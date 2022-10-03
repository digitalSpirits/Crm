using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Processing;
using MediatR;

namespace Crm.Application.Transactions.IntegrationHandlers
{
    public class TransactionRegisteredNotificationHandler : INotificationHandler<TransactionRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;
        public TransactionRegisteredNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }
        public async Task Handle(TransactionRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new CreateTransactionCommand(Guid.NewGuid(),notification.CustomerId, notification.BankAccountId, notification.TransactionId));
        }
    }
}