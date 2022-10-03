
using Crm.Application.Configuration.DomainEvents;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Crm.Domain.Customers.BankAccounts.Transfers.Events;
using Newtonsoft.Json;

namespace Crm.Application.Transfers.IntegrationHandlers
{
    public class TransferRegisteredNotification : DomainNotificationBase<TransferRegisteredEvent>
    {
        public CustomerId CustomerId { get; }
        public BankAccountId BankAccountId { get; }
        public TransferId TransferId { get; }

        public TransferRegisteredNotification(TransferRegisteredEvent domainEvent) : base(domainEvent)
        {
            CustomerId = domainEvent.CustomerId;    
            BankAccountId = domainEvent.BankAccountId;  
            TransferId = domainEvent.TransferId;
        }

        [JsonConstructor]
        public TransferRegisteredNotification(CustomerId customerId, BankAccountId bankAccountId, TransferId transferId) : base(null)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            TransferId = transferId;
        }
    }
}