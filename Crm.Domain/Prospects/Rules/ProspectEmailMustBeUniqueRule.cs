
using Crm.Domain.SeedWork;

namespace Crm.Domain.Prospects.Rules
{
    public class ProspectEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly IProspectUniquenessChecker _prospectUniquenessChecker;

        private readonly string _email;

        public ProspectEmailMustBeUniqueRule(IProspectUniquenessChecker prospectUniquenessChecker, string email)
        {
            _prospectUniquenessChecker = prospectUniquenessChecker;
            _email = email;
        }

        public bool IsBroken() => !_prospectUniquenessChecker.IsUnique(_email);

        public string Message => "Prospect with this email already exists.";
    }
}