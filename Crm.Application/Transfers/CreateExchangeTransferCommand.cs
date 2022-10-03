using System;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.BankAccounts.Transfers;
using MediatR;
using Newtonsoft.Json;

namespace Crm.Application.Transfers
{
    public class CreateExchangeTransferCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public CreateExchangeTransferCommand(Guid id, CustomerId customerId, BankAccountId bankAccountId, TransferId transferId) : base(id)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            TransferId = transferId;
        }

        public CustomerId CustomerId { get; }
        public BankAccountId BankAccountId { get; }
        public TransferId TransferId { get; }
    }
}