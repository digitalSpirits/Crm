using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.ApiKeys.Events
{
    public class ApiKeyChangedEvent : DomainEventBase
    {
        public ApiKeyId ApiKeyId { get; }

        public ApiKeyChangedEvent( ApiKeyId apiKeyId)
        {
            ApiKeyId = apiKeyId;
        }
    }
}
