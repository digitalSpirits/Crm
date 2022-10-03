using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.SubScriptions
{
    public class RemoveCustomerSubScriptionCommand : CommandBase<Unit>
    {
       
        public Guid CustomerId { get; set; }

        public Guid SubScriptionId { get; set; }

        public RemoveCustomerSubScriptionCommand(Guid customerId, Guid subScriptionId)
        {
            CustomerId = customerId;
            SubScriptionId = subScriptionId;
        }
    }
}
