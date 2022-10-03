using Crm.Application.Configuration.Queries;
using System;

namespace Crm.Application.Operations.GetCashAccountBalance
{
    public class GetCashAccountBalanaceQuery : IQuery<string>
    {
        public string AccountId { get; set; }
        public GetCashAccountBalanaceQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}
