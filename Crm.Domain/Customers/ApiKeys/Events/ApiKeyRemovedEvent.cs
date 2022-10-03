using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.ApiKeys.Events
{
    public class ApiKeyRemovedEvent : DomainEventBase
    {
        public ApiKeyId ApiKeyId { get; }

        public ApiKeyRemovedEvent(ApiKeyId apiKeyId)
        {
            ApiKeyId = apiKeyId;
        }
    }
}
