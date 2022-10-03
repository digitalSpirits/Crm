using Crm.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace Crm.Application.ApiKeys.ChangeCustomerApiKey
{
    public class ChangeCustomerApiKeyCommand : CommandBase<ApiKeyDto>
    {
        public Guid CustomerId { get; set; }

        public Guid ApiKeyId { get; set; }
        public string Name { get; set; }
        public List<Guid> RoleIds { get; set; }

        public ChangeCustomerApiKeyCommand(Guid customerId, Guid apiKeyId, string name, List<Guid> roleIds)
        {
            CustomerId = customerId;
            ApiKeyId = apiKeyId;
            Name = name;
            RoleIds = roleIds;
        }
    }
}
