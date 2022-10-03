﻿
using Crm.Application.Configuration.Gateways;
using Crm.Domain.Customers.BankAccounts;

using MediatR;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Application.Operations.RegisterVirtualIBan
{
    public class RegisterVirtualIbanCommandHandler : IRequestHandler<RegisterVirtualIbanCommand, string>
    {
        private readonly IBankApiGateWay _bankApiGateWay;
        public RegisterVirtualIbanCommandHandler(IBankApiGateWay bankApiGateWay)
        {
            _bankApiGateWay = bankApiGateWay;
        }
        public async Task<string> Handle(RegisterVirtualIbanCommand request, CancellationToken cancellationToken)
        {
            var endPointPath = _bankApiGateWay.EndPoints.Find(x => x.Key == BankAccountEndPoints.VIRTUAL_IBAN).Value.Replace("{accountId}", request.AccountId);
            var client = new RestClient(string.Concat(_bankApiGateWay, endPointPath));
            var restRequest = new RestRequest("", Method.Post).AddStringBody(request.AccountId, DataFormat.Json);

            restRequest.AddHeaders(_bankApiGateWay.Headers);

            var response = await client.ExecuteAsync(restRequest);

            return response.Content;
        }
    }
}
