
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Commands;
using Crm.Domain.SeedWork;
using Crm.Domain.Prospects;
using MediatR;

namespace Crm.Application.Prospects.RegisterProspectConfirmations
{
    public class RegisterProspectConfirmationCommandHandler : IRequestHandler<RegisterProspectConfirmationCommand, ProspectDto>
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterProspectConfirmationCommandHandler(IProspectRepository prospectRepository, IUnitOfWork unitOfWork)
        {
            _prospectRepository = prospectRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProspectDto> Handle(RegisterProspectConfirmationCommand request, CancellationToken cancellationToken)
        {
            var prospect = await _prospectRepository.GetByIdAsync(new ProspectId(request.ProspectId));

            if (prospect.IsTokenValid(request.Token))
            {
                prospect.MarkProspectEmailAsConfirmed(request.Token);
            }
            // return expired token

            await _unitOfWork.CommitAsync(cancellationToken);

            return new ProspectDto { Id = prospect.Id.Value };
        }
    }
}
