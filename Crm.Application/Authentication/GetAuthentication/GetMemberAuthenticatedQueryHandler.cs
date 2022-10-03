using Crm.Application.Authentication.GetAuthenticationToken;
using Crm.Application.Configuration.Data;
using Crm.Application.Configuration.Queries;
using Crm.Application.Customers;
using Crm.Domain.Customers;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Application.Authentication.GetMemberAuthentications
{
    public class GetAuthenticatedQueryHandler : IQueryHandler<GetAuthenticatedQuery, TokenDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        public GetAuthenticatedQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAuthenticateSecurity authenticateSecurity)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _authenticateSecurity = authenticateSecurity;
        }
        public async Task<TokenDto> Handle(GetAuthenticatedQuery request, CancellationToken cancellationToken)
        {

            var passwordHashedSalted = _authenticateSecurity.CreateHashedSaltedPassword(request.UserName, request.Password);
            
            const string userIdSql = "SELECT Id FROM [05_Crm].[Crm].[Customers] WHERE [UserName] = @UserName And [Password] = @Password AND [IsRemoved] = 'false'";

           var connection = _sqlConnectionFactory.GetOpenConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@UserName", request.UserName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@Password", passwordHashedSalted, System.Data.DbType.String, System.Data.ParameterDirection.Input);
          
            var customer = await connection.QuerySingleAsync<CustomerDto>(userIdSql, parameters);

            KeyValuePair<string, long> token = _authenticateSecurity.GetAuthToken(customer.Id);

            TokenDto authenticationDto = new TokenDto
            {
                Id = customer.Id,
                ActiveToken = token.Key,
                ExpireDate = token.Value,
            };

            return authenticationDto;
        }
    }
}
