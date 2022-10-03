using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Prospects;
using MediatR;

namespace Crm.Application.Prospects.IntegrationHandlers
{
    public class MarkProspectAsConfirmedCommandHandler : IRequestHandler<CreateProspectCommand, Unit>
    {
        private readonly IProspectRepository _prospectRepository;

        public MarkProspectAsConfirmedCommandHandler(IProspectRepository prospectRepository)
        {
            _prospectRepository = prospectRepository;
        }

        public async Task<Unit> Handle(CreateProspectCommand command, CancellationToken cancellationToken)
        {
            var prospect = await _prospectRepository.GetByIdAsync(command.ProspectId);

            prospect.MarkAsConfirmedByEmail();

            return Unit.Value;
        }
    }
}