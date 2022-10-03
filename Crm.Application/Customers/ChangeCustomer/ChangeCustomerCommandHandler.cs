
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;

using MediatR;
using Crm.Domain.Customers;

namespace Crm.Application.Customers.ChangeCustomer
{
    public class ChangeCustomerCommandHandler : IRequestHandler<ChangeCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeCustomerCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangeCustomerCommand request, CancellationToken cancellationToken) 
        {

            var customer = await _companyRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.ChangeCustomer( request.Name, request.Surname, request.Email, request.Type, request.Country, request.City, request.Address, request.Phone, request.ProfileRisk,  request.UpdateDate);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
