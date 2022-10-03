using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Customers.RemoveCustomer
{
    public class RemoveCustomerCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; set; }

        public RemoveCustomerCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
