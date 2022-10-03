using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.ApiKeys.RemoveCustomerApiKey
{
    public class RemoveCustomerApiKeyCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; set; }

        public Guid ApiKeyId { get; set; }

        public RemoveCustomerApiKeyCommand(Guid customerId, Guid apiKeyId)
        {
            CustomerId = customerId;
            ApiKeyId = apiKeyId;
        }
    }
}
