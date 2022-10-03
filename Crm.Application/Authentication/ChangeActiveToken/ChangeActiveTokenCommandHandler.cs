
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using Crm.Domain.SeedWork;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Application.Authentication.ChangeActiveToken
{
    internal class ChangeActiveTokenCommandHandler : IRequestHandler<ChangeActiveTokenCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        private readonly ICustomerRepository _customerRepository;
        public ChangeActiveTokenCommandHandler(IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _authenticateSecurity = authenticateSecurity;
            _customerRepository = customerRepository;
        }
        public async Task<Unit> Handle(ChangeActiveTokenCommand request, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));
            KeyValuePair<string, long> token = _authenticateSecurity.GetAuthToken(request.CustomerId);

            customer.RefreshToken(token.Key);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

