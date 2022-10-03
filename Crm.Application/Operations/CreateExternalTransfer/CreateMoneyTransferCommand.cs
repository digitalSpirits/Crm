
using Crm.Application.Configuration.Commands;

namespace Crm.Application.Operations.RegisterMoneyTransfer
{
    public class CreateMoneyTransferCommand : CommandBase<string>
    {
        public string AccountId { get; set; }
        public string Body { get; set; }
        public CreateMoneyTransferCommand(string accountId, string body)
        {
            AccountId = accountId;
            Body = body;
        }
    }
}
