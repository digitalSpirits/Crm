
using Crm.Application.Configuration.Gateways;
using Crm.Application.Configuration.Queries;
using Crm.Domain.Customers.BankAccounts;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Application.Operations.GetCashAccountBalance
{
    public class GetCashAccountBalanceQueryHandler : IQueryHandler<GetCashAccountBalanaceQuery, string>
    {

        private readonly IBankApiGateWay _bankApiGateWay;
        public GetCashAccountBalanceQueryHandler(IBankApiGateWay bankApiGateWay)
        {
            _bankApiGateWay = bankApiGateWay;
        }
        public async Task<string> Handle(GetCashAccountBalanaceQuery request, CancellationToken cancellationToken)
        {
            var endPointPath = _bankApiGateWay.EndPoints.Find(x => x.Key == BankAccountEndPoints.CASH_ACCOUNT_BALANCE).Value.Replace("{accountId}", request.AccountId);
            var client = new RestClient(string.Concat(_bankApiGateWay, endPointPath));
            var restRequest = new RestRequest("", Method.Get);

            restRequest.AddHeaders(_bankApiGateWay.Headers);

            var response = await client.ExecuteAsync(restRequest);

            return response.Content;
        }
    }
}
