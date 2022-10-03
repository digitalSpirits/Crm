using Crm.Domain.SeedWork;

namespace Crm.Domain.Prospects.EmailConfirmations.Events
{
    public class EmailConfirmationRegisteredEvent : DomainEventBase
    {
        public ProspectId ProspectId { get; }

        public EmailConfirmationId EmailConfirmationId { get; }

        public EmailConfirmationRegisteredEvent(ProspectId prospectId, EmailConfirmationId emailConfirmationId)
        {
            ProspectId = prospectId;
            EmailConfirmationId = emailConfirmationId;
        }
    }
}
