using Crm.Application.ApiKeys;
using Crm.Application.ApiRoles;
using Crm.Application.BankAccounts;
using Crm.Application.Configuration.Data;
using Crm.Application.Configuration.Queries;
using Crm.Application.Customers.GetCustomerDetails;
using Crm.Application.Documents;
using Crm.Application.SubScriptions;
using Crm.Application.Transactions;
using Crm.Application.Transfers.GetBankAccountTransfers;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Application.Customers.GetCompanyDetails
{
    public class GetCustomerDetailsQueryHandler : IQueryHandler<GetCustomerDetailsQuery, CustomerDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCustomerDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<CustomerDetailsDto> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            const string companySql = "SELECT [Id],[ActivationDate],[UpdateDate],[Name],[Surname],[Email],[Type],[Country],[City],[Address],[Phone],[ProfileRisk] FROM [05_Crm].[Crm].[Customers] WHERE [Id] = @customerId ";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var customer = await connection.QuerySingleOrDefaultAsync<CustomerDetailsDto>(companySql, new { request.CustomerId });

            // load subscriptions
            const string subscriptionSql = "SELECT [Id],[CustomerId],[Name],[Rev],[SetupFee],[MonthlyFee],[TransactionFee] FROM [Crm].[Subscriptions] WHERE [CustomerId] = @customerId AND [IsRemoved] = 'false'";
            var subscriptions = await connection.QueryAsync<SubScriptionDto>(subscriptionSql, new { request.CustomerId });

            // load bank accounts
            const string bankSql = "SELECT [Id],[CustomerId],[Currency],[Description],[BeneficiaryName],[BankName],[BankBranchName],[BankAddress],[BankSwiftBic],[BankAccountNumber],[Iban],[IntermediaryBankName],[IntermediaryBankSwiftBic],[IntermediaryBankAddress] FROM [Crm].[BankAccounts] WHERE [BankAccounts].[CustomerId] = @customerId And [IsRemoved] = 'false'";
            var bankAccounts = await connection.QueryAsync<BankAccountDto>(bankSql, new { request.CustomerId });

            // load bank account transactions
            const string transactionsSql = "SELECT [Id],[BankAccountId],[Currency],[Amount],[BeginDate],[EndDate],[ExpectedYield],[ActualYield],[Status] FROM [Crm].[Transactions] WHERE [IsRemoved] = 'false'";
            var transactions = await connection.QueryAsync<TransactionDetailsDto>(transactionsSql);

            // load bank account transfers
            const string transfersql = "SELECT [PublicId],[BankAccountId],[Side],[Currency],[Amount],[Status],[Type],[Date] FROM [Crm].[Transfers] WHERE [IsRemoved] = 'false'";
            var transfers = await connection.QueryAsync<TransferDto>(transfersql);

            // load documents
            const string documentSql = "SELECT [Id],[DocumentType],[DocumentStatus],[AuditedBy],[AuditedDate] FROM [Crm].[Documents] WHERE [CustomerId] = @customerId  AND [IsRemoved] = 'false'";
            var documents = await connection.QueryAsync<DocumentDetailsDto>(documentSql, new { request.CustomerId });

            // load api keys
            const string apikeysSql = "SELECT [Id], [Name] FROM [Crm].[ApiKeys] WHERE [CustomerId] = @customerId";
            var apiKeys = await connection.QueryAsync<ApiKeyRolesDto>(apikeysSql, new { request.CustomerId });

            // load all bank accounts
            const string apiRolesSql = "SELECT [KeyId], [RoleId], [Category], [Name] FROM [Crm].[CustomerRolesVw] WHERE [CustomerId] = @customerId";
            var apiRoles = await connection.QueryAsync<ApiKeyRoleDto>(apiRolesSql, new { request.CustomerId });

            foreach (var key in apiKeys)
            {
                var roles = apiRoles.Where(x => x.KeyId == key.Id).AsList();

                var customerRoles = new List<ApiRoleDto>();
                foreach (var role in roles)
                {
                    var customerRole = new ApiRoleDto
                    {
                        Id = role.RoleId,
                        Category = role.Category,
                        Name = role.Name
                    };

                    customerRoles.Add(customerRole);
                }

                key.Roles = customerRoles;
            }

            customer.SubScriptions = subscriptions.AsList();
            customer.BankAccounts = bankAccounts.AsList();

            decimal cash = 0;
            decimal stacked = 0;
            foreach (var bank in bankAccounts)
            {
                var bankAccountTransactions = transactions.Where(x => x.BankAccountId == bank.Id).AsList();
                bank.Transactions = bankAccountTransactions;

                var bankAccountTransfers = transfers.Where(x => x.BankAccountId == bank.Id).OrderByDescending(x => x.Date).AsList();
                bank.Transfers = bankAccountTransfers;

                stacked = bankAccountTransactions.Sum(x => x.Amount);
                cash = bankAccountTransfers.Sum(x => x.Amount);
            }

            var currentPosition = new CurrentPositionDto
            {
                Currency = "Eur",
                Cash = cash,
                Staked = stacked
            };

            customer.CurrentPosition = currentPosition;
            customer.Documents = documents.AsList();
            customer.ApiKeysRoles = apiKeys.AsList();
            
            return customer;
        }
    }
}
