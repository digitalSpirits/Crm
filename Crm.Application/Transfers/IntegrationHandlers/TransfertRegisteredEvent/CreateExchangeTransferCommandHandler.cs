using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Data;
using Crm.Application.Configuration.Gateways;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Crm.Domain.SeedWork;
using Dapper;
using MediatR;
using RestSharp;

namespace Crm.Application.Transfers.IntegrationHandlers
{
    public class CreateExchangeTransferCommandHandler : IRequestHandler<CreateExchangeTransferCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IBankApiGateWay _bankApiGateWay;
        public CreateExchangeTransferCommandHandler(ISqlConnectionFactory sqlConnectionFactory, ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IBankApiGateWay bankApiGateWay)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _bankApiGateWay = bankApiGateWay;
        }

        public async Task<Unit> Handle(CreateExchangeTransferCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);

            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string transferSql = "SELECT [TransferId],[BankAccountId],[PublicId],[Iban],[Side],[Currency],[Amount],[Status],[Type],[Date] FROM [Crm].[CustomerBanksTransfersVw] WHERE [TransferId] = @transferId";

            // checks if an external transfer is required
            var transfer = await connection.QuerySingleAsync<CustomerBankTransferDetailsDto>(transferSql, new { transferId = command.TransferId.Value });
            if ((TransferType)transfer.Type != TransferType.Allocated)
                return Unit.Value;

            // send money to exchange

            var externalTransfer = new ExternalTransfer {
                BeneficiarId = "123", // exchange iban
                DebitIban = transfer.Iban, 
                Currency = transfer.Currency,
                Amount = transfer.Amount.ToString(),
                Reference = transfer.PublicId
            };

            var createExternalTransfer = new CreateExternalTransfer
            {
                ExternalTransfer = externalTransfer,
            };

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(externalTransfer);

            var endPointPath = _bankApiGateWay.EndPoints.Find(x => x.Key == BankAccountEndPoints.CREATE_EXTERNAL_TRANSFER).Value.Replace("{accountId}", command.CustomerId.Value.ToString());
            var client = new RestClient(string.Concat(_bankApiGateWay, endPointPath));
            var restRequest = new RestRequest("", Method.Post).AddStringBody(body, DataFormat.Json);

            var headers = _bankApiGateWay.Headers;
            headers.Add(new KeyValuePair<string, string>("X-Qonto-Idempotency-Key", transfer.TransferId.ToString()));

            restRequest.AddHeaders(headers);

            // var response = await client.ExecuteAsync(restRequest);

            // update transaction status
            HttpStatusCode response = HttpStatusCode.OK;

            if (response == HttpStatusCode.OK)
                customer.ChangeBankAccountTransfer( new BankAccountId(transfer.BankAccountId), new TransferId(transfer.TransferId), TransferStatus.InProgress);

            return Unit.Value;
        }
    }
}