using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.ApiKeys.GetCustomerApikeys
{
    public class GetCustomerApiKeyQueryHandler : IRequestHandler<GetCustomerApiKeyQuery, List<ApiKeyDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCustomerApiKeyQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ApiKeyDto>> Handle(GetCustomerApiKeyQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string apiKeysSql = "SELECT [Id], [Name], [Key] FROM [Crm].[ApiKeys] WHERE [ApiKeys].[CustomerId] = @customerId And [IsRemoved] = 'false'";

            var apikeys = await connection.QueryAsync<ApiKeyDto>(apiKeysSql, new { request.CustomerId });

            return apikeys.AsList();
        }
    }
}
