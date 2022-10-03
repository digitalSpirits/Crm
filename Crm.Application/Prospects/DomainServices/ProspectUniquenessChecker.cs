using Crm.Application.Configuration.Data;
using Crm.Domain.Prospects;
using Dapper;

namespace Crm.Application.Prospects.DomainServices
{
    public class ProspectEmailUniquenessChecker : IProspectUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProspectEmailUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(string email)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1 FROM [Web].[Prospect] WHERE [Email] = @Email";
            var prospectsNumber = connection.QuerySingleOrDefault<int?>(sql, new{ Email = email});

            return !prospectsNumber.HasValue;
        }
    }
}