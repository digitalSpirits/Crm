using Crm.Application.Configuration.Queries;
using System;

namespace Crm.Application.Operations.GetVirtualIbans
{
    public class GetVirtualIbansQuery : IQuery<string>
    {
        public string AccountId { get; set; }
        public GetVirtualIbansQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}
