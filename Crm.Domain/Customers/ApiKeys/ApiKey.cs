using Crm.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Crm.Domain.Customers.ApiKeys
{
    public class ApiKey
    {
        internal ApiKeyId Id;

        private string _name;

        private string _key;

        private readonly List<KeyRole>? _roles;

        private bool _isRemoved;

        public ApiKey(string name, string key)
        {
            Id = new ApiKeyId(Guid.NewGuid());
            _name = name;
            _key = key;
            _roles = new List<KeyRole>(); ;
        }

        private ApiKey()
        {
            _isRemoved = false;
        }

        internal static ApiKey CreateNew(string name,string key)
        {
            return new ApiKey(name, key);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }

        // CREATE ROLES
        public RoleId AddRole(RoleId roleId)
        {
            var role = KeyRole.AddRole(roleId);

            _roles.Add(role);

            return role.RoleId;
        }

        public void Change(string name, string key, List<RoleId> roleIds)
        {
            _name = name;
            _key = key;

            var rolesToDelete = _roles.Where(x => !roleIds.Contains(x.RoleId));
            foreach (var roleToDelete in rolesToDelete.ToList())
            {
                _roles.Remove(roleToDelete);
            }

            foreach (var roleId in roleIds)
            {
                var role = _roles.Find(x => x.RoleId == roleId);
                if(role == null)
                    _roles.Add(new KeyRole(roleId));
            }
 
        }

        // REMOVE ROLE
        public void RemoveRoles()
        {
            if (_roles.Count > 0)
               _roles.RemoveRange(0, _roles.Count -1);
        }
    }
}

