using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers
{
    public class CustomerChangedEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public CustomerChangedEvent(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}
