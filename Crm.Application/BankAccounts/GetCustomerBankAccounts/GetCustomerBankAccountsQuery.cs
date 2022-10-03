
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.BankAccounts.GetCustomerBankAccounts
{
    public class GetCustomerBankAccountsQuery : IRequest<List<BankAccountDto>>
    {
        public Guid CustomerId { get; set; }
        public GetCustomerBankAccountsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
