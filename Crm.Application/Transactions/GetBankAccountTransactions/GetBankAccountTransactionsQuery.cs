
using Crm.Application.Configuration.Queries;
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Transactions.GetBankAccountTransacions
{
    public class GetBankAccountTransactionsQuery : IQuery<List<TransactionDetailsDto>>
    {
        public Guid CustomerId { get; set; }
        public GetBankAccountTransactionsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
