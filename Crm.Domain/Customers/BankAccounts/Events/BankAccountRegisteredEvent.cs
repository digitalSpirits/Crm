using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.BankAccounts.Events
{
    public class BankAccountRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public BankAccountId BankId { get; }

        public BankAccountRegisteredEvent(CustomerId customerId, BankAccountId bankId)
        {
            CustomerId = customerId;
            BankId = bankId;
        }
    }
}
