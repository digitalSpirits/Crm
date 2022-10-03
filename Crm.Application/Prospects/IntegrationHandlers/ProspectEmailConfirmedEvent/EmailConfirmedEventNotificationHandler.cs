using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Processing;
using MediatR;

namespace Crm.Application.Prospects.IntegrationHandlers
{
    public class EmailConfirmedEventNotificationHandler : INotificationHandler<EmailConfirmedEventNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public EmailConfirmedEventNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(EmailConfirmedEventNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new CreateCustomerCommand( Guid.NewGuid(), notification.ProspectId));
        }
    }
}