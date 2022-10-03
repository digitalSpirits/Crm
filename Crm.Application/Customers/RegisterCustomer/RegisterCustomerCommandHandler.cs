using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Domain.Customers;
using MediatR;

namespace Crm.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCustomerCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = CustomerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {

            var customer = Customer.CreateRegistered(request.Username, request.Password, request.Token, request.Name, request.Surname, request.Email, request.Type, request.Country, request.City, request.Address, request.Phone, request.ProfileRisk, request.ActivationDate, request.CloseDate, request.UpdateDate);

            await _customerRepository.CreateAsync(customer);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new CustomerDto { Id = customer.Id.Value };
        }
    }
}
