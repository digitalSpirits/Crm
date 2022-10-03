
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Transfers.GetBankAccountTransfers
{
    public class GetBankAccountTransfersQuery : IRequest<List<TransferDto>>
    {
        public Guid CustomerId { get; set; }
        public GetBankAccountTransfersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
