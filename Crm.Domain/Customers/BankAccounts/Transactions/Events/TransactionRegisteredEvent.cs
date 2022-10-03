using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.BankAccounts.Transactions.Events
{
    public class TransactionRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }
        public BankAccountId BankAccountId { get; }
        public TransactionId TransactionId { get; }

        public TransactionRegisteredEvent(CustomerId customerId, BankAccountId bankAccountId, TransactionId transactionId)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            TransactionId = transactionId;
        }
    }
}
