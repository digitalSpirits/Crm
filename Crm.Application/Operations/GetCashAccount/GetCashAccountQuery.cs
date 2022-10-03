using Crm.Application.Configuration.Queries;
using System;

namespace Crm.Application.Operations.GetCashAccount
{
    public class GetCashAccountQuery : IQuery<string>
    {
        public string AccountId { get; set; }
        public GetCashAccountQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}
