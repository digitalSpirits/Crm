using Crm.Domain.SeedWork;

namespace Crm.Domain.Prospects
{
    public class ProspectRegisteredEvent : DomainEventBase
    {
        public ProspectId ProspectId { get; }

        public ProspectRegisteredEvent(ProspectId prospectId)
        {
            ProspectId = prospectId;
        }
    }
}
