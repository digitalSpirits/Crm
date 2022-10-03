﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Crm.Application.Configuration.Commands;
using Crm.Application.Configuration.Data;
using Crm.Application.Configuration.DomainEvents;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace Crm.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : IRequestHandler<ProcessOutboxCommand, Unit>
    {
        private readonly IMediator _mediator;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProcessOutboxCommandHandler(IMediator mediator, ISqlConnectionFactory sqlConnectionFactory)
        {
            _mediator = mediator;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand command, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT [OutboxMessage].[Id],[OutboxMessage].[Type], [OutboxMessage].[Data] FROM [Crm].[OutboxMessages] AS [OutboxMessage] WHERE [OutboxMessage].[ProcessedDate] IS NULL";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            var messagesList = messages.AsList();

            const string sqlUpdateProcessedDate = "UPDATE [Crm].[OutboxMessages] SET [ProcessedDate] = @Date WHERE [Id] = @Id";
            if (messagesList.Count > 0)
            {
                foreach (var message in messagesList)
                {
                    Type type = Assemblies.Application.GetType(message.Type);
                    var request = JsonConvert.DeserializeObject(message.Data, type) as IDomainEventNotification;

                    using (LogContext.Push(new OutboxMessageContextEnricher(request)))
                    {
                        await _mediator.Publish(request, cancellationToken);

                        await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                        {
                            Date = DateTime.UtcNow,
                            message.Id
                        });
                    }
                }
            }

            return Unit.Value;
        }

        private class OutboxMessageContextEnricher : ILogEventEnricher
        {
            private readonly IDomainEventNotification _notification;

            public OutboxMessageContextEnricher(IDomainEventNotification notification)
            {
                _notification = notification;
            }
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"OutboxMessage:{_notification.Id.ToString()}")));
            }
        }
    }
}