using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using System.Linq;
using Crm.Application.ApiRoles;
using MediatR;

namespace Crm.Application.ApiKeys.RegisterCustomerApiRole
{
    public class RegisterCustomerApiRoleCommandHandler : IRequestHandler<RegisterCustomerApiRoleCommand, ApiRoleDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        public RegisterCustomerApiRoleCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity)
        {
            _customerRepository = CustomerRepository;
            _authenticateSecurity = authenticateSecurity;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiRoleDto> Handle(RegisterCustomerApiRoleCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var roles = request.Roles.Select(x => x.Value);
            var token = _authenticateSecurity.GetApiToken(request.Id, roles);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new ApiRoleDto { Id = customer.Id.Value };
        }
    }
}
