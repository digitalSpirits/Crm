
using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Customers.Rules
{
    public class CustomerBankAccountMustHaveLiquidityRule : IBusinessRule
    {
        private readonly ICustomerLiquidyChecker _customerLiquidyChecker;

        private readonly Guid _bankAccountId;

        private readonly decimal _amount;

        public CustomerBankAccountMustHaveLiquidityRule(ICustomerLiquidyChecker customerLiquidyChecker, Guid bankAccountId, decimal amount)
        {
            _customerLiquidyChecker = customerLiquidyChecker;
            _bankAccountId = bankAccountId; 
            _amount = amount;
        }

        public bool IsBroken()
        {
            return !_customerLiquidyChecker.IsMjor(_bankAccountId, _amount);
        } 

        public string Message => "Do not have enough liquidity.";
    }
}