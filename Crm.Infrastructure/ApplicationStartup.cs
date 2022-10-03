using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Crm.Application.Configuration;
using Crm.Application.Configuration.Emails;
using Quartz;
using Quartz.Impl;
using Crm.Infrastructure.Database;
using Crm.Infrastructure.Processing;
using Crm.Infrastructure.Processing.InternalCommands;
using Crm.Infrastructure.Logging;
using Crm.Infrastructure.Quartz;
using Crm.Infrastructure.Emails;
using Crm.Infrastructure.Caching;
using Crm.Infrastructure.Domain;
using Crm.Infrastructure.Processing.Outbox;
using Crm.Infrastructure.SeedWork;
using Serilog;
using Crm.Application.Configuration.Gateways;
using Autofac.Core;

namespace Crm.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(IServiceCollection services, string connectionString, string jwtToken, ICacheStore cacheStore, IEmailSender emailSender,
            EmailsSettings emailsSettings, IBankApiGateWay bankApiGateWay, ILogger logger, IExecutionContextAccessor executionContextAccessor, bool runQuartz = true)
        {
            if (runQuartz)
            {
                StartQuartz(connectionString, emailsSettings, logger, executionContextAccessor);
            }

            services.AddSingleton(bankApiGateWay);
            services.AddSingleton(cacheStore);

            return RegisterAutofacServiceProvider(services, connectionString, jwtToken, emailSender, emailsSettings, logger, executionContextAccessor);
        }

        private static IServiceProvider RegisterAutofacServiceProvider(IServiceCollection services, string connectionString, string jwtToken, IEmailSender emailSender, EmailsSettings emailsSettings, ILogger logger, IExecutionContextAccessor executionContextAccessor)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new DomainModule(jwtToken));

            if (emailSender != null)
            {
                containerBuilder.RegisterModule(new EmailModule(emailSender, emailsSettings));
            }
            else
            {
                containerBuilder.RegisterModule(new EmailModule(emailsSettings));
            }

            containerBuilder.RegisterModule(new ProcessingModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            var containerBuilded = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(containerBuilded));

            var autofacServiceProvider = new AutofacServiceProvider(containerBuilded);

            CompositionRoot.SetContainer(containerBuilded);

            return autofacServiceProvider;
        }

        private static void StartQuartz(string connectionString,EmailsSettings emailsSettings,ILogger logger,IExecutionContextAccessor executionContextAccessor)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();

            container.RegisterModule(new LoggingModule(logger));
            container.RegisterModule(new QuartzModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new EmailModule(emailsSettings));
            container.RegisterModule(new ProcessingModule());

            container.RegisterInstance(executionContextAccessor);
            container.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<CrmContext>();
                dbContextOptionsBuilder.UseSqlServer(connectionString);

                dbContextOptionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new CrmContext(dbContextOptionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            scheduler.JobFactory = new JobFactory(container.Build());

            scheduler.Start().GetAwaiter().GetResult();

            //jobs
            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }
    }

}
