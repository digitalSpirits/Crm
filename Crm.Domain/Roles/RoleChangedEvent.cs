using Crm.Domain.SeedWork;

namespace Crm.Domain.Roles
{
    public class RoleChangedEvent : DomainEventBase
    {
        public RoleId RoleId { get; }

        public RoleChangedEvent(RoleId roleId)
        {
            RoleId = roleId;
        }
    }
}
