using Autofac;
using Crm.Application.ApiRoles.DomainServices;
using Crm.Application.Authentication.DomainServices;
using Crm.Application.Prospects.DomainServices;
using Crm.Application.Transfers.DomainServices;
using Crm.Domain.Customers;
using Crm.Domain.Prospects;
using Crm.Domain.Roles;

namespace Crm.Infrastructure.Domain
{
    public class DomainModule : Autofac.Module
    {
        private readonly string _jwtSecret;
        public DomainModule(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<ProspectEmailUniquenessChecker>()
                .As<IProspectUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleNameUniquenessChecker>()
                .As<IRoleNameUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerLiquidyChecker>()
                .As<ICustomerLiquidyChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthenticationSecurity>()
                .As<IAuthenticateSecurity>()
                .WithParameter("jwtSecret", _jwtSecret)
                .InstancePerLifetimeScope();
        }
    }
}
