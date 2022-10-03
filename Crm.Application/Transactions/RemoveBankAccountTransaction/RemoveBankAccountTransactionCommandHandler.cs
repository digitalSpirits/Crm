
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using MediatR;
using Crm.Domain.Customers.BankAccounts.Transactions;

namespace Crm.Application.Transactions
{
    public class RemoveBankAccountTransactionCommandHandler : IRequestHandler<RemoveBankAccountTransactionCommand, Unit>
    {
        private readonly ICustomerRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveBankAccountTransactionCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveBankAccountTransactionCommand request, CancellationToken cancellationToken)
        {
            var customer = await _companyRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.RemoveBankAccountTransaction(new TransactionId(request.TransactionId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
