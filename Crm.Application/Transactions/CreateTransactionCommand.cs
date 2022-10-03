using System;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.BankAccounts.Transactions;
using MediatR;
using Newtonsoft.Json;

namespace Crm.Application.Transactions
{
    public class CreateTransactionCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public CreateTransactionCommand(Guid id, CustomerId customerId, BankAccountId bankAccountId, TransactionId transactionId) : base(id)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            TransactionId = transactionId;
        }

        public CustomerId CustomerId { get; }
        public BankAccountId BankAccountId { get; }
        public TransactionId TransactionId { get; }
    }
}