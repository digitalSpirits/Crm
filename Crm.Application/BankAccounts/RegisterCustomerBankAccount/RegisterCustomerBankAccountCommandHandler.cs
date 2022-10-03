using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using MediatR;

namespace Crm.Application.BankAccounts.RegisterCustomerBankAccount
{
    public class RegisterCustomerBankAccountCommandHandler : IRequestHandler<RegisterCustomerBankAccountCommand, BankAccountDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCustomerBankAccountCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = CustomerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BankAccountDto> Handle(RegisterCustomerBankAccountCommand request, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.CreateBankAccount(request.Currency, request.Description, request.BeneficiaryName, request.BankName, request.BankBrankName, request.BankAddress, request.BankSwiftBic, request.BankAccountNumber, request.Iban, request.IntermediaryBankName, request.IntermediaryBankSwiftBic, request.IntermediaryBankAddress);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new BankAccountDto { Id = customer.Id.Value };
        }
    }
}
