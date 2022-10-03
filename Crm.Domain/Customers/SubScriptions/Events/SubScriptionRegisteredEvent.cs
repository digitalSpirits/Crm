using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.SubScriptions.Events
{
    public class SubScriptionRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public SubScriptionId SubScriptionId { get; }

        public SubScriptionRegisteredEvent(CustomerId customerId, SubScriptionId subScriptionId)
        {
            CustomerId = customerId;
            SubScriptionId = subScriptionId;
        }
    }
}
