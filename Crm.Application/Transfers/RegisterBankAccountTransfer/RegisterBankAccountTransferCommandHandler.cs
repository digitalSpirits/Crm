using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Domain.Customers;
using Crm.Domain.Customers.BankAccounts;
using MediatR;
using System;

namespace Crm.Application.Transfers.RegisterBankAccountTransfer
{
    public class RegisterBankAccountTransferCommandHandler : IRequestHandler<RegisterBankAccountTransferCommand, TransferDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        private readonly ICustomerLiquidyChecker _customerLiquidyChecker;
        public RegisterBankAccountTransferCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity, ICustomerLiquidyChecker  customerLiquidyChecker)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _authenticateSecurity = authenticateSecurity;
            _customerLiquidyChecker = customerLiquidyChecker;
    }
        public async Task<TransferDto> Handle(RegisterBankAccountTransferCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var publicId = _authenticateSecurity.GetPublicId();

            var transferId = customer.CreateBankAccountTransfer(new BankAccountId(request.BankAccountId), publicId, request.Currency, request.Side, request.Amount, request.Status, request.Type, DateTime.UtcNow, _customerLiquidyChecker);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new TransferDto { Id = transferId.Value };
        }
    }
}