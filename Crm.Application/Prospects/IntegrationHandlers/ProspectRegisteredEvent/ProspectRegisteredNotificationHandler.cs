using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Processing;
using MediatR;

namespace Crm.Application.Prospects.IntegrationHandlers
{
    public class ProspectRegisteredNotificationHandler : INotificationHandler<ProspectRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public ProspectRegisteredNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(ProspectRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new CreateProspectCommand(Guid.NewGuid(),notification.ProspectId));
        }
    }
}