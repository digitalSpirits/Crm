using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Customers.GetCustomerDetails;
using Crm.Application.SubScriptions;
using Crm.Application.BankAccounts;

namespace Crm.Application.Customers.GetCustomers
{
    public class GetCustomerBanksQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCustomerBanksQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<CustomerDetailsDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string customersSql = "SELECT [Id],[Username],[Password],[Name],[Surname],[Email],[Type],[Country],[City],[Address],[Phone],[ProfileRisk],[ActivationDate],[CloseDate],[UpdateDate] FROM [Crm].[Customers] WHERE [IsRemoved] = 'false'";
            var customers = await connection.QueryAsync<CustomerDetailsDto>(customersSql);

            // load all subscriptions
            const string subscriptionSql = "SELECT  [Id],[CustomerId],[Name],[Rev],[SetupFee],[MonthlyFee],[TransactionFee] FROM [Crm].[Subscriptions] WHERE [IsRemoved] = 'false'";
            var subscriptions = await connection.QueryAsync<SubScriptionDto>(subscriptionSql);

            // load all bank accounts
            const string bankSql = "SELECT [Id],[CustomerId],[Currency],[Description],[BeneficiaryName],[BankName],[BankBranchName],[BankAddress],[BankSwiftBic],[BankAccountNumber],[Iban],[IntermediaryBankName],[IntermediaryBankSwiftBic],[IntermediaryBankAddress] FROM [Crm].[BankAccounts] WHERE [IsRemoved] = 'false'";
            var banks = await connection.QueryAsync<BankAccountDto>(bankSql);

            foreach (var customer in customers)
            {
                customer.SubScriptions = subscriptions.AsList().FindAll(x => x.CustomerId == customer.Id);
                customer.BankAccounts = banks.AsList().FindAll(x => x.CustomerId != customer.Id);    
            }

            return customers.AsList();
        }
    }
}
