
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using MediatR;
using Crm.Domain.Customers;

namespace Crm.Application.Customers.RemoveCustomer
{
    public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCustomerCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            var company = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            company.Remove();

            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}
