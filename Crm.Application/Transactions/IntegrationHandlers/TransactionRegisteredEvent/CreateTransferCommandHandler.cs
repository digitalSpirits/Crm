using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Data;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts.Transactions;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Dapper;
using MediatR;

namespace Crm.Application.Transactions.IntegrationHandlers
{
    public class CreateTransferCommandHandler : IRequestHandler<CreateTransactionCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        private readonly ICustomerLiquidyChecker _customerLiquidyChecker;
        public CreateTransferCommandHandler(ICustomerRepository customerRepository, ISqlConnectionFactory sqlConnectionFactory, IAuthenticateSecurity authenticateSecurity, ICustomerLiquidyChecker customerLiquidyChecker)
        {
            _customerRepository = customerRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
            _authenticateSecurity = authenticateSecurity;
            _customerLiquidyChecker = customerLiquidyChecker;
        }

        public async Task<Unit> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);

            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string transactionsSql = "SELECT [Id],[BankAccountId],[Currency],[Amount],[BeginDate],[EndDate],[ExpectedYield],[ActualYield],[Status] FROM [Crm].[Transactions] WHERE [Transactions].[Id] = @transactionId AND [IsRemoved] = 'false'";
      
            var transaction = await connection.QuerySingleAsync<TransactionDetailsDto>(transactionsSql, new { transactionId = command.TransactionId.Value });
           
            var publicId = _authenticateSecurity.GetPublicId();

            customer.CreateBankAccountTransfer(command.BankAccountId, publicId, transaction.Currency, "Debit", - transaction.Amount,  TransferStatus.Scheduled, TransferType.Allocated, DateTime.UtcNow, _customerLiquidyChecker);

            customer.ChangeTransactionStatus(command.BankAccountId, command.TransactionId, (TransactionStatus)transaction.Status);

            return Unit.Value;
        }
    }
}