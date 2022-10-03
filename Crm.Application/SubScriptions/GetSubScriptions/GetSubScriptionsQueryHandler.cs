using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.SubScriptions.GetSubScriptions
{
    public class GetSubScriptionsQueryHandler : IRequestHandler<GetSubScriptionsQuery, List<SubScriptionDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetSubScriptionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<SubScriptionDto>> Handle(GetSubScriptionsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string subscriptionSql = "SELECT  [Id],[CustomerId],[Name],[Rev],[SetupFee],[MonthlyFee],[TransactionFee] FROM [Crm].[Subscriptions] WHERE [IsRemoved] = 'false'";
           
            var subScriptions = await connection.QueryAsync<SubScriptionDto>(subscriptionSql);

            return subScriptions.AsList();
        }
    }
}
