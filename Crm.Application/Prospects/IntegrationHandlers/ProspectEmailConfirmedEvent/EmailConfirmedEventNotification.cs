
using Crm.Application.Configuration.DomainEvents;
using Crm.Domain.Prospects;
using Crm.Domain.Prospects.EmailConfirmations.Events;
using Newtonsoft.Json;

namespace Crm.Application.Prospects.IntegrationHandlers
{
    public class EmailConfirmedEventNotification : DomainNotificationBase<EmailConfirmedEvent>
    {
        public ProspectId ProspectId { get; }

        public EmailConfirmedEventNotification(EmailConfirmedEvent domainEvent) : base(domainEvent)
        {
            ProspectId = domainEvent.ProspectId;
        }

        [JsonConstructor]
        public EmailConfirmedEventNotification(ProspectId prospectId) : base(null)
        {
            ProspectId = prospectId;
        }
    }
}