using Crm.Application.Configuration.Data;
using Crm.Domain.Customers;
using Dapper;
using System;

namespace Crm.Application.Transfers.DomainServices
{
    public class CustomerLiquidyChecker :ICustomerLiquidyChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CustomerLiquidyChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsMjor(Guid bankAccountId, decimal amount)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Liquidity] FROM [Crm].[CustomerBanksLiquidityVw] WHERE [BankAccountId] = @BankAccountId";
            var liquidity = connection.QuerySingleOrDefault<int?>(sql, new { BankAccountId = bankAccountId });

            return liquidity > amount;
        }
    }
}