using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.ApiRoles;

namespace Crm.Application.ApiKeys.GetCustomerApiRoles
{
    public class GetCustomerApiRoleQueryHandler : IRequestHandler<GetCustomerApiRoleQuery, List<ApiRoleDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCustomerApiRoleQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ApiRoleDto>> Handle(GetCustomerApiRoleQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string customerApiRolesSql = "SELECT [Id], [Category], [Name] FROM [Crm].[CustomerRolesVw] WHERE [CustomerRolesVw].[CustomerId] = @customerId";

            var apiRoles = await connection.QueryAsync<ApiRoleDto>(customerApiRolesSql, new { request.CustomerId });

            return apiRoles.AsList();
        }
    }
}
