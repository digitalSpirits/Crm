using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Commands;
using Crm.Domain.SeedWork;
using Crm.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Crm.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult> where T : ICommand<TResult>
    {
        private readonly IRequestHandler<T, TResult> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly CrmContext _crmContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(IRequestHandler<T, TResult> decorated,   IUnitOfWork unitOfWork, CrmContext crmContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _crmContext = crmContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                var internalCommand = await _crmContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}