
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using MediatR;
using Crm.Domain.Customers;
using Crm.Domain.Customers.ApiKeys;

namespace Crm.Application.ApiKeys.RemoveCustomerApiKey
{
    public class RemoveCustomerApiKeyCommandHandler : ICommandHandler<RemoveCustomerApiKeyCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCustomerApiKeyCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCustomerApiKeyCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.RemoveApiKey(new ApiKeyId(request.ApiKeyId));

            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}
