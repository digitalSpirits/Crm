
using Crm.Domain.Prospects.Roles.Rules;
using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Roles
{
    public class Role: Entity, IAggregateRoot
    {
        public RoleId Id { get; private set; }

        private string _cateogory;

        public string Name { get; private set; }

        private bool _isRemoved;

        private Role()
        {

            _isRemoved = false;
        }

        public Role(string category, string name)
        {
            Id = new RoleId(Guid.NewGuid());
            _cateogory = category;
            Name = name;

          //  AddDomainEvent(new ProspectRegisteredEvent(Id));
        }

        public static Role CreateRegistered(string category, string name, IRoleNameUniquenessChecker roleNameUniquenessChecker)
        {
            // check here some logic , unique email...

            CheckRule(new RoleNamelMustBeUniqueRule(roleNameUniquenessChecker, name));

            return new Role(category, name);

            // create token
        }

        public void Remove()
        {
            _isRemoved = true;
        }
    }
}
