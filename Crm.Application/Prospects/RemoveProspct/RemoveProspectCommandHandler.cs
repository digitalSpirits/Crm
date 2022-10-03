
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using MediatR;
using Crm.Domain.Prospects;

namespace Crm.Application.Prospects.RemoveProspect
{
    public class RemoveProspectCommandHandler : IRequestHandler<RemoveProspectCommand, Unit>
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveProspectCommandHandler(IProspectRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _prospectRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveProspectCommand request, CancellationToken cancellationToken)
        {
            var prospect = await _prospectRepository.GetByIdAsync(new ProspectId(request.ProspectId));

            prospect.Remove();

            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}
