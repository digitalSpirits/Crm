using MediatR;
using Crm.Application.Configuration.Commands;

namespace Crm.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}