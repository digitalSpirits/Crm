using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.ApiKeys.Events
{
    public class ApiKeyRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public ApiKeyId ApiKeyId { get; }

        public ApiKeyRegisteredEvent(CustomerId customerId, ApiKeyId apiKeyId)
        {
            CustomerId = customerId;
            ApiKeyId = apiKeyId;
        }
    }
}
