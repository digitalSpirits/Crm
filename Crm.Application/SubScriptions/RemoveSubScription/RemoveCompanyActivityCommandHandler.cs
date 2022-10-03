
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using MediatR;
using Crm.Application.SubScriptions;
using Crm.Domain.Customers.SubScriptions;

namespace Crm.Application.Activities
{
    public class RemoveCustomerSubScriptionCommandHandler : IRequestHandler<RemoveCustomerSubScriptionCommand, Unit>
    {
        private readonly ICustomerRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCustomerSubScriptionCommandHandler(ICustomerRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCustomerSubScriptionCommand request, CancellationToken cancellationToken)
        {
            var customer = await _companyRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            customer.RemoveSubScription(new SubScriptionId(request.SubScriptionId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
