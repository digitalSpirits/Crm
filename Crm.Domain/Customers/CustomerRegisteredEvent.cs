using Crm.Domain.SeedWork;
using Crm.Domain.Customers;

namespace Crm.Domain.Customers
{
    public class CustomerRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public CustomerRegisteredEvent(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}
