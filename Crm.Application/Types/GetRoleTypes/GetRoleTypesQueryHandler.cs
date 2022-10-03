using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Crm.Application.Types
{
    public class GetRoleTypesQueryHandler : IRequestHandler<GetRoleTypesQuery, List<CategoryRoleTypesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetRoleTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<CategoryRoleTypesDto>> Handle(GetRoleTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string apiRoleTypesSql = "SELECT [Id], [Category], [Name] FROM [Crm].[Roles] WHERE [Roles].[IsRemoved] = 'false'";

            var rolesTypes = await connection.QueryAsync<RolesTypeDto>(apiRoleTypesSql);

            var categories = rolesTypes.GroupBy(x => x.Category).Select(x => x.Key);
            var categoriesRoleTypes = new List<CategoryRoleTypesDto>();
            foreach (var category in categories)
            {
                var roles = rolesTypes.Where(x => x.Category == category).Select(y => new RoleNameDto { Id = y.Id, Name = y.Name }).ToList();
                categoriesRoleTypes.Add(new CategoryRoleTypesDto { Category = category, Roles = roles });
            }

            return categoriesRoleTypes;
        }
    }
}
