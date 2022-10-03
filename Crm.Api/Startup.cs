using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Crm.Api.Configuration;
using Crm.Application.Configuration;
using Microsoft.AspNetCore.Http;
using Crm.Infrastructure;
using System;
using Crm.Api.Services;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Crm.Infrastructure.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Crm.Application.Configuration.Emails;
using Serilog;
using Serilog.Formatting.Compact;
using Hellang.Middleware.ProblemDetails;
using Crm.Application.Configuration.Validation;
using Crm.Domain.SeedWork;
using Crm.Api.SeedWork;
using System.Collections.Generic;
using Crm.Application.Configuration.Gateways;
using Crm.Infrastructure.GateWays;

namespace Crm.Api
{
    public class Startup
    {
        public IConfigurationRoot _configuration { get; private set; }

        private const string _connectionString = "CrmConnectionStrings";
        private const string allowedOrigins = "AllowedOrigins";
        private const string _jwtSettings = "JwtSettings";
        private const string _emailSettings = "EmailsSettings";
        private const string _bankSettings = "BankSettings";

        private static ILogger _logger;

        // in core 3 use to register .env variables
        public Startup(IWebHostEnvironment env)
        {
            _logger = ConfigureLogger();
            _logger.Information("Logger configured");

            _configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
            .AddJsonFile("appsettings.json")
            .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddMemoryCache();

            services.AddSwaggerDocumentation();

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });


            var apiJwtSettings = _configuration.GetSection(_jwtSettings);
            services.Configure<JwtSettings>(apiJwtSettings);

            var jwtSecret = apiJwtSettings.Get<JwtSettings>().Secret;
            var secret = Encoding.ASCII.GetBytes(jwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(allowedOrigins, builder =>
                {
                    builder.WithOrigins(_configuration.GetSection(allowedOrigins).Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition");
                });
            });

            services.AddHttpContextAccessor();
            var serviceProvider = services.BuildServiceProvider();

            IExecutionContextAccessor executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetService<IHttpContextAccessor>());

            var children = _configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            var emailsSettings = _configuration.GetSection(_emailSettings).Get<EmailsSettings>();
            var bankSettings = _configuration.GetSection(_bankSettings).Get<BankApiGateWaySettings>();

            List<KeyValuePair<string, string>> endPoints = new List<KeyValuePair<string, string>>();
            foreach (var endPoint in bankSettings.EndPoints)
            {
                endPoints.Add(new KeyValuePair<string, string>(endPoint.Name, endPoint.Path));
            }

            // load fabrick endpoints
            List<KeyValuePair<string, string>> domains = new List<KeyValuePair<string, string>>();
            foreach (var domain in bankSettings.Domains)
            {
                domains.Add(new KeyValuePair<string, string>(domain.Name, domain.Path));
            }

            var bankApiGateway = new BankApiGateway(domains, bankSettings.Login, bankSettings.Secret, bankSettings.Iban, endPoints);

            return ApplicationStartup.Initialize(
               services,
               _configuration[_connectionString], 
               jwtSecret,
               new MemoryCacheStore(memoryCache, cachingConfiguration), 
               null,
               emailsSettings,
               bankApiGateway,
                _logger,
               executionContextAccessor);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorrelationMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            app.UseCors(allowedOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }

        private static ILogger ConfigureLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();
        }
    }
   
}
