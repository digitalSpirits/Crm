using Crm.Domain.Roles;
using Crm.Domain.SeedWork;

namespace Crm.Domain.Prospects.Roles.Rules
{
    public class  RoleNamelMustBeUniqueRule : IBusinessRule
    {
        private readonly IRoleNameUniquenessChecker _roleNameUniquenessChecker;

        private readonly string _name;

        public RoleNamelMustBeUniqueRule(IRoleNameUniquenessChecker roleNameUniquenessChecker, string name)
        {
            _roleNameUniquenessChecker = roleNameUniquenessChecker;
            _name = name;
        }

        public bool IsBroken() => !_roleNameUniquenessChecker.IsUnique(_name);

        public string Message => "Role with this name already exists.";
    }
}