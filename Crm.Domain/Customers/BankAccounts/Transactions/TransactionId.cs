using System;
using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.BankAccounts.Transactions
{
    public class TransactionId : TypedIdValueBase
    {
        public TransactionId(Guid value) : base(value)
        {

        }
    }
}
