using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using System.Linq;
using Crm.Domain.Roles;
using Crm.Domain.Customers.ApiKeys;
using MediatR;

namespace Crm.Application.ApiKeys.ChangeCustomerApiKey
{
    public class ChangeCustomerApiKeyCommanddHandler : IRequestHandler<ChangeCustomerApiKeyCommand, ApiKeyDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        public ChangeCustomerApiKeyCommanddHandler(ICustomerRepository customerRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity)
        {
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _authenticateSecurity = authenticateSecurity;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiKeyDto> Handle(ChangeCustomerApiKeyCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var listOfRoleIds = request.RoleIds.Select(x => new RoleId(x)).ToList();

            var roles = await _roleRepository.GetAsync(listOfRoleIds);

            var roleNames = roles.Select(x => x.Name).ToArray();

            var token = _authenticateSecurity.GetApiToken(request.Id, roleNames);

            customer.ChangeApiKey(new ApiKeyId(request.ApiKeyId), request.Name, token, listOfRoleIds);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new ApiKeyDto { Id = customer.Id.Value };
        }
    }
}
