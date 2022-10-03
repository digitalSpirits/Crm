
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using MediatR;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;

namespace Crm.Application.BankAccounts.RemoveCustomerBankAccount
{
    public class RemoveCustomerBankAccountCommandHandler : IRequestHandler<RemoveCustomerBankAccountCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCustomerBankAccountCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCustomerBankAccountCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.RemoveBank(new BankAccountId(request.BankId));

            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}
