using Crm.Domain.SeedWork;

namespace Crm.Domain.Roles
{
    public class RoleRegisteredEvent : DomainEventBase
    {
        public RoleId RoleId { get; }

        public RoleRegisteredEvent(RoleId roleId)
        {
            RoleId = roleId;
        }
    }
}
