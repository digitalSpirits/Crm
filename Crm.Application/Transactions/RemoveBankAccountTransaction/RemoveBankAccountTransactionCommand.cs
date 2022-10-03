using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Transactions
{
    public class RemoveBankAccountTransactionCommand : CommandBase<Unit>
    {
       
        public Guid CustomerId { get; set; }

        public Guid TransactionId { get; set; }

        public RemoveBankAccountTransactionCommand(Guid customerId, Guid transactionId)
        {
            CustomerId = customerId;
            TransactionId = transactionId;
        }
    }
}
