
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using MediatR;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;

namespace Crm.Application.BankAccounts.ChangeCustomerBankAccount
{
    public class ChangeCustomerBankAccountCommandHandler : IRequestHandler<ChangeCustomerBankAccountCommand, Unit>
    {
        private readonly ICustomerRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeCustomerBankAccountCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangeCustomerBankAccountCommand request, CancellationToken cancellationToken) 
        {

            var customer = await _companyRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.ChangeBankAccount(new BankAccountId(request.BankId), request.Currency, request.Description, request.BeneficiaryName, request.BankName, request.BankBrankName, request.BankAddress, request.BankSwiftBic, request.BankAccountNumber, request.Iban, request.IntermediaryBankName, request.IntermediaryBankSwiftBic, request.IntermediaryBankAddress);

            await _unitOfWork.CommitAsync();
   
            return Unit.Value;
        }
    }
}
