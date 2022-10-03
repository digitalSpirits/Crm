using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.BankAccounts.GetCustomerBankAccounts
{
    public class GetCustomerBankAccountsQueryHandler : IRequestHandler<GetCustomerBankAccountsQuery, List<BankAccountDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCustomerBankAccountsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<BankAccountDto>> Handle(GetCustomerBankAccountsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string bankSql = "SELECT [Id],[CustomerId],[Currency],[Description],[BeneficiaryName],[BankName],[BankBranchName],[BankAddress],[BankSwiftBic],[BankAccountNumber],[Iban],[IntermediaryBankName],[IntermediaryBankSwiftBic],[IntermediaryBankAddress] FROM [Crm].[BankAccounts] WHERE [BankAccounts].[CustomerId] = @customerId And [IsRemoved] = 'false'";

            var banks = await connection.QueryAsync<BankAccountDto>(bankSql, new { request.CustomerId });

            return banks.AsList();
        }
    }
}
