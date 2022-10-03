using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Prospects.GetProspects
{
    public class GetProspectsQueryHandler : IRequestHandler<GetProspectQuery, List<ProspectDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetProspectsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ProspectDetailsDto>> Handle(GetProspectQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string prospectSql = "SELECT [Id],[Email],[Password],[Country],[CreationDate] FROM [Web].[Prospect] WHERE [IsRemoved] = 'false'";

            var prospects = await connection.QueryAsync<ProspectDetailsDto>(prospectSql);

            return prospects.AsList();
        }
    }
}
