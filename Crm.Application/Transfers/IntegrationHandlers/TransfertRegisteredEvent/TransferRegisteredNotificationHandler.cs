using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Processing;
using MediatR;

namespace Crm.Application.Transfers.IntegrationHandlers
{
    public class TransferRegisteredNotificationHandler : INotificationHandler<TransferRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;
        public TransferRegisteredNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }
        public async Task Handle(TransferRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new CreateExchangeTransferCommand(Guid.NewGuid(),notification.CustomerId, notification.BankAccountId, notification.TransferId));
        }
    }
}