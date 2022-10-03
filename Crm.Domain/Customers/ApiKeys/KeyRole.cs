using Crm.Domain.Roles;
using Crm.Domain.SeedWork;
using System.Collections.Generic;

namespace Crm.Domain.Customers.ApiKeys
{
    public class KeyRole : Entity
    {
        public RoleId RoleId { get; private set; }


        public KeyRole()
        {
        }

        public KeyRole(RoleId roleId)
        {
            RoleId = roleId;
        }

        internal static KeyRole AddRole(RoleId roleId)
        {
            return new KeyRole(roleId);
        }
    }
}

