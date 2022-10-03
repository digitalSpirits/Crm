using Crm.Application.Configuration.Data;
using Crm.Domain.Roles;
using Dapper;

namespace Crm.Application.ApiRoles.DomainServices
{
    public class RoleNameUniquenessChecker : IRoleNameUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public RoleNameUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(string name)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1 FROM [Crm].[Roles] WHERE [Name] = @Name";
            var roleNames= connection.QuerySingleOrDefault<int?>(sql, new{ Name = name });

            return !roleNames.HasValue;
        }
    }
}