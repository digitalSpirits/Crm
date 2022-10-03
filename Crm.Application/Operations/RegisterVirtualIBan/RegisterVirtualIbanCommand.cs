
using Crm.Application.Configuration.Commands;

namespace Crm.Application.Operations.RegisterVirtualIBan
{
    public class RegisterVirtualIbanCommand : CommandBase<string>
    {
        public string AccountId { get; set; }

        public string Description { get; set; }
        public RegisterVirtualIbanCommand(string accountId, string description)
        {
            AccountId = accountId;

            Description = description;
        }
    }
}
