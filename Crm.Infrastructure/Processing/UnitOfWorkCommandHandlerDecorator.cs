using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Crm.Domain.SeedWork;
using Crm.Infrastructure.Database;
using Crm.Application.Configuration.Commands;

namespace Crm.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerDecorator<T> : IRequestHandler<T> where T:ICommand
    {
        private readonly IRequestHandler<T> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly CrmContext _crmContext;

        public UnitOfWorkCommandHandlerDecorator(IRequestHandler<T> decorated, IUnitOfWork unitOfWork, CrmContext crmContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _crmContext = crmContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand = await _crmContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}