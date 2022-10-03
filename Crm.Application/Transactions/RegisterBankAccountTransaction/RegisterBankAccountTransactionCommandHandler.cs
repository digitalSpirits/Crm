using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using MediatR;

namespace Crm.Application.Transactions.RegisterBankAccountTransaction
{
    public class RegisterBankAccountTransactionCommandHandler : IRequestHandler<RegisterBankAccountTransactionCommand, TransactionDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterBankAccountTransactionCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<TransactionDto> Handle(RegisterBankAccountTransactionCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var transaction = customer.CreateBankAccountTransaction(new BankAccountId(request.BankAccountId), request.Currency, request.Amount, request.BeginDate, request.EndDate, request.ExpectedYield, request.ActualYield, request.Status);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new TransactionDto { Id = transaction.Value };
        }
    }
}