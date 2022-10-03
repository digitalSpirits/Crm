using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Prospects.GetProspectsFiltered
{
    public class GetProspectsFilteredQueryHandler : IRequestHandler<GetProspectsFilteredQuery, List<ProspectDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetProspectsFilteredQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ProspectDetailsDto>> Handle(GetProspectsFilteredQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string prospectFilteredSql = "SELECT [Id],[Email],[Password],[Country],[CreationDate] FROM [Web].[Prospect] WHERE [email]  like '% @Filter &' OR [country] like '% @Filter &'";

            var prospects = await connection.QueryAsync<ProspectDetailsDto>(prospectFilteredSql, new { request.Filter });

            return prospects.AsList();
        }
    }
}
