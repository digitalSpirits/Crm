using Crm.Application.ApiRoles;
using Crm.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace Crm.Application.ApiKeys.RegisterCustomerApiRole
{
    public class RegisterCustomerApiRoleCommand : CommandBase<ApiRoleDto>
    {
        public Guid CustomerId { get; set; }
        public List<KeyValuePair<string, string>> Roles { get; set; }

        public RegisterCustomerApiRoleCommand(Guid customerId, List<KeyValuePair<string, string>> roles)
        {
            CustomerId = customerId;
            Roles = roles;
        }
    }
}
