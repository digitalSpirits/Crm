using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Customers.GetCustomerDetails;

namespace Crm.Application.Customers.GetCustomersFiltered
{
    public class GetCustomersFilteredQueryHandler : IRequestHandler<GetCustomersFilteredQuery, List<CustomerDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCustomersFilteredQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<CustomerDetailsDto>> Handle(GetCustomersFilteredQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string companiesFilteredSql = "SELECT [Id], [SubScriber], [Name] ,[Address], [Cap] ,[City] ,[Province] ,[FiscalCode],[Piva] ,[Bank],[Iban],[UniqueCode],[ContractType],[SubscriptionType],[SubscriptionDate],[CreationDate] FROM [AG_Crm].[Web].[Companies] WHERE [name] like '% @Filter &' OR [City] like '% @Filter &' OR [Province]  like '% @Filter &' OR [FiscalCode] like '% @Filter &' OR [PIva] like '% @Filter &' OR [SubScriptionType] like '% @Filter &'";

            var customers = await connection.QueryAsync<CustomerDetailsDto>(companiesFilteredSql, new { request.Filter });

            return customers.AsList();
        }
    }
}
