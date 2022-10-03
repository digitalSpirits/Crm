using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Transfers.GetBankAccountTransfers
{
    public class GetBankAccountTransfersQueryHandler : IRequestHandler<GetBankAccountTransfersQuery, List<TransferDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetBankAccountTransfersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<TransferDto>> Handle(GetBankAccountTransfersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string transfersSql = "SELECT [PublicId],[BankAccountId],[Side],[Currency],[Amount],[Status],[Type],[Date] FROM [Crm].[CustomerBanksTransfersVw] WHERE [CustomerId] = @CustomerId AND [IsRemoved] = 'false'";
           
            var transfers = await connection.QueryAsync<TransferDto>(transfersSql, new {CustomerId = request.CustomerId});

            return transfers.AsList();
        }
    }
}
