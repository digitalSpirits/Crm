using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.SubScriptions.Events
{
    public class SubScriptionRemovedEvent : DomainEventBase
    {
        public SubScriptionId SubScriptionId { get; }

        public SubScriptionRemovedEvent(SubScriptionId subScriptionId)
        {
            SubScriptionId = subScriptionId;
        }
    }
}
