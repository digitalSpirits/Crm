using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Prospects;
using Crm.Domain.Customers;
using System;
using MediatR;

namespace Crm.Application.Prospects.RegisterProspect
{
    public class RegisterProspectCommandHandler : IRequestHandler<RegisterProspectCommand, ProspectDto>
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        private readonly IProspectUniquenessChecker _prospectUniquenessChecker;
        public RegisterProspectCommandHandler(IProspectRepository prospectRepository, IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity, IProspectUniquenessChecker prospectUniquenessChecker)
        {
            _prospectRepository = prospectRepository;
            _unitOfWork = unitOfWork;
            _authenticateSecurity = authenticateSecurity;
            _prospectUniquenessChecker = prospectUniquenessChecker;
        }
        public async Task<ProspectDto> Handle(RegisterProspectCommand request, CancellationToken cancellationToken)
        {
            var encryptedPassword = _authenticateSecurity.CreateHashedSaltedPassword(request.Email, request.Password);

            var prospect = Prospect.CreateRegistered(request.Email, encryptedPassword, request.Country, _prospectUniquenessChecker);

            var token = _authenticateSecurity.GetAuthToken(Guid.NewGuid());

            var expireDate = DateTime.UtcNow.AddDays(1).Ticks;

            prospect.CreateEmailConfirmation(token.Key, expireDate);

            await _prospectRepository.CreateAsync(prospect);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new ProspectDto { Id = prospect.Id.Value };
        }
    }
}
