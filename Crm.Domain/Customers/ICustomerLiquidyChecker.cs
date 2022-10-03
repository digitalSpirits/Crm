using System;

namespace Crm.Domain.Customers
{
    public interface ICustomerLiquidyChecker
    {
        bool IsMjor(Guid bankAccountId, decimal amount);
    }
}