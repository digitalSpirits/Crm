using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Authentication.ChangeActiveToken
{
    public class ChangeActiveTokenCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; }

        public ChangeActiveTokenCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
