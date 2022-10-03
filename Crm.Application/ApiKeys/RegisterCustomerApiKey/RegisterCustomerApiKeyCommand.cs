using Crm.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace Crm.Application.ApiKeys.RegisterCustomerApiKey
{
    public class RegisterCustomerApiKeyCommand : CommandBase<ApiKeyDto>
    {
        public Guid CustomerId { get; set; }

        public string Name { get; set; }
        public List<Guid> RoleIds { get; set; }

        public RegisterCustomerApiKeyCommand(Guid customerId, string name, List<Guid> roleIds)
        {
            CustomerId = customerId;
            Name = name;
            RoleIds = roleIds;
        }
    }
}
