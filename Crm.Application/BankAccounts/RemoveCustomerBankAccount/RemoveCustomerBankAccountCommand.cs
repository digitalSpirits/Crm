using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.BankAccounts.RemoveCustomerBankAccount
{
    public class RemoveCustomerBankAccountCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; set; }

        public Guid BankId { get; set; }

        public RemoveCustomerBankAccountCommand(Guid customerId, Guid bankId)
        {
            CustomerId = customerId;
            BankId = bankId;
        }
    }
}
