using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using System.Linq;
using Crm.Domain.Roles;
using MediatR;

namespace Crm.Application.ApiKeys.RegisterCustomerApiKey
{
    public class RegisterCustomerApiKeyCommanddHandler : IRequestHandler<RegisterCustomerApiKeyCommand, ApiKeyDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        public RegisterCustomerApiKeyCommanddHandler(ICustomerRepository customerRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity)
        {
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _authenticateSecurity = authenticateSecurity;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiKeyDto> Handle(RegisterCustomerApiKeyCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var listOfRoleIds = request.RoleIds.Select(x => new RoleId(x)).ToList();

            var roles = await _roleRepository.GetAsync(listOfRoleIds);

            var roleNames = roles.Select(x => x.Name).ToArray();

            var token = _authenticateSecurity.GetApiToken(request.Id, roleNames);

            var role = customer.CreateApiKey(request.Name, token, listOfRoleIds);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new ApiKeyDto { Id = customer.Id.Value };
        }
    }
}
