using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.BankAccounts.Events
{
    public class BankAccountChangeEvent : DomainEventBase
    {
        public BankAccountId BankId { get; }

        public BankAccountChangeEvent(BankAccountId bankId)
        {
            BankId = bankId;
        }
    }
}
