
using Crm.Application.Configuration.DomainEvents;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.BankAccounts.Transactions;
using Crm.Domain.Customers.BankAccounts.Transactions.Events;
using Newtonsoft.Json;

namespace Crm.Application.Transactions.IntegrationHandlers
{
    public class TransactionRegisteredNotification : DomainNotificationBase<TransactionRegisteredEvent>
    {
        public CustomerId CustomerId { get; }
        public BankAccountId BankAccountId { get; }
        public TransactionId TransactionId { get; }

        public TransactionRegisteredNotification(TransactionRegisteredEvent domainEvent) : base(domainEvent)
        {
            CustomerId = domainEvent.CustomerId;    
            BankAccountId = domainEvent.BankAccountId;  
            TransactionId = domainEvent.TransactionId;
        }

        [JsonConstructor]
        public TransactionRegisteredNotification(CustomerId customerId, BankAccountId bankAccountId, TransactionId transactionId) : base(null)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            TransactionId = transactionId;
        }
    }
}