
using Crm.Application.Configuration.DomainEvents;
using Crm.Domain.Prospects;
using Newtonsoft.Json;

namespace Crm.Application.Prospects.IntegrationHandlers
{
    public class ProspectRegisteredNotification : DomainNotificationBase<ProspectRegisteredEvent>
    {
        public ProspectId ProspectId { get; }

        public ProspectRegisteredNotification(ProspectRegisteredEvent domainEvent) : base(domainEvent)
        {
            ProspectId = domainEvent.ProspectId;
        }

        [JsonConstructor]
        public ProspectRegisteredNotification(ProspectId prospectId) : base(null)
        {
            ProspectId = prospectId;
        }
    }
}