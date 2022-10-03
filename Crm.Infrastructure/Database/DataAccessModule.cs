using Autofac;
using Crm.Application.Configuration.Data;
using Crm.Domain.SeedWork;
using Crm.Infrastructure.Domain;
using Crm.Infrastructure.Domain.Customers;
using Crm.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using Crm.Domain.Customers;
using Crm.Infrastructure.Domain.Prospects;
using Crm.Domain.Prospects;
using Crm.Infrastructure.Domain.Roles;
using Crm.Domain.Roles;

namespace Crm.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string connectionString)
        {
            _databaseConnectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
             .As<ISqlConnectionFactory>()
             .WithParameter("connectionString", _databaseConnectionString)
             .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
             .As<IUnitOfWork>()
             .InstancePerLifetimeScope();

            builder.RegisterType<ProspectRepository>()
             .As<IProspectRepository>()
             .InstancePerLifetimeScope();

            builder.RegisterType<CustomerRepository>()
            .As<ICustomerRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<RoleRepository>()
             .As<IRoleRepository>()
             .InstancePerLifetimeScope();

            builder.RegisterType<StronglyTypedIdValueConverterSelector>()
            .As<IValueConverterSelector>()
            .InstancePerLifetimeScope();

            builder
             .Register(c =>
             {
                 var dbContextOptionsBuilder = new DbContextOptionsBuilder<CrmContext>();
                 dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                 .LogTo(Console.WriteLine, LogLevel.Information);
                 dbContextOptionsBuilder
                     .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                 return new CrmContext(dbContextOptionsBuilder.Options);
             })
             .AsSelf()
             .As<DbContext>()
             .InstancePerLifetimeScope();

        }
    }
}
