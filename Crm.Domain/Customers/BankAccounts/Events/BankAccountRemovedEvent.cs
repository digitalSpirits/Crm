using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.BankAccounts.Events
{
    public class BankAccountRemovedEvent : DomainEventBase
    {
        public BankAccountId BankId { get; }

        public BankAccountRemovedEvent(BankAccountId bankId)
        {
            BankId = bankId;
        }
    }
}
