using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Configuration.Queries;

namespace Crm.Application.Transactions.GetBankAccountTransacions
{
    public class GetBankAccountTransacionsQueryHandler : IQueryHandler<GetBankAccountTransactionsQuery, List<TransactionDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetBankAccountTransacionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<TransactionDetailsDto>> Handle(GetBankAccountTransactionsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string transactionsSql = "SELECT [Id],[BankAccountId],[Currency],[Amount],[BeginDate],[EndDate],[ExpectedYield],[ActualYield],[Status] FROM [Crm].[CustomerBanksTransactionsVw]  WHERE [CustomerId] = @CustomerId AND [IsRemoved] = 'false'";
           
            var transactions = await connection.QueryAsync<TransactionDetailsDto>(transactionsSql, new { CustomerId = request.CustomerId });

            return transactions.AsList();
        }
    }
}
