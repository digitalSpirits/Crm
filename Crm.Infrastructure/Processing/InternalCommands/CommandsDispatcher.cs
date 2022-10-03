using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Crm.Infrastructure.Database;
using Crm.Application.Customers.GetCustomers;
using Newtonsoft.Json;

namespace Crm.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly CrmContext _context;
        public CommandsDispatcher(IMediator mediator, CrmContext companiesContext)
        {
            _mediator = mediator;
            _context = companiesContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var internalCommand = await _context.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(GetCustomersQuery)).GetType(internalCommand.Type);
            dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await _mediator.Send(command);
        }
    }
}