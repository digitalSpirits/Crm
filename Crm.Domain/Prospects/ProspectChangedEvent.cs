using Crm.Domain.SeedWork;

namespace Crm.Domain.Prospects
{
    public class ProspectChangedEvent : DomainEventBase
    {
        public ProspectId ProspectId { get; }

        public ProspectChangedEvent(ProspectId prospectId)
        {
            ProspectId = prospectId;
        }
    }
}
