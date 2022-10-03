using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using Crm.Application.SubScriptions;
using MediatR;

namespace Crm.Application.Activities
{
    public class RegisterCustomerSubScriptionCommandHandler : IRequestHandler<RegisterCustomerSubScriptionCommand, SubScriptionDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCustomerSubScriptionCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<SubScriptionDto> Handle(RegisterCustomerSubScriptionCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CompanyId));

            var subScription = customer.CreateSubScription(request.Name, request.Rev, request.SetupFee, request.MonthlyFee, request.TransactionFee, request.UpdateDate);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SubScriptionDto { Id = customer.Id.Value };
        }
    }
}