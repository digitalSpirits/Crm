using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.BankAccounts.Transfers.Events
{
    public class TransferRegisteredEvent : DomainEventBase
    {

        public CustomerId CustomerId { get; }

        public BankAccountId BankAccountId { get; }

        public TransferId TransferId { get; }

        public TransferRegisteredEvent(CustomerId customerId, BankAccountId bankAccountId, TransferId transferId)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            TransferId = transferId;
        }
    }
}
