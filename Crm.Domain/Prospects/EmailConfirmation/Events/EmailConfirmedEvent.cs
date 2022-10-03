using Crm.Domain.SeedWork;

namespace Crm.Domain.Prospects.EmailConfirmations.Events
{
    public class EmailConfirmedEvent : DomainEventBase
    {
        public ProspectId ProspectId { get; }

        public EmailConfirmedEvent(ProspectId prospectId)
        {
            ProspectId = prospectId;
        }
    }
}
