using Crm.Application.Configuration.Data;
using Crm.Application.Configuration.Queries;
using Dapper;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Application.Prospects.GetProspectDetails
{
    public class GetProspectDetailsQueryHandler : IQueryHandler<GetProspectDetailsQuery, ProspectDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetProspectDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<ProspectDetailsDto> Handle(GetProspectDetailsQuery request, CancellationToken cancellationToken)
        {
            const string prospectSql = "SELECT [Id],[Email],[Password],[Country],[CreationDate] FROM [05_Crm].[Web].[Prospect] WHERE [Prospect].[Id] = @prospectId ";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var prospect = await connection.QuerySingleOrDefaultAsync<ProspectDetailsDto>(prospectSql, new { request.ProspectId });
       
            return prospect;
        }
    }
}
