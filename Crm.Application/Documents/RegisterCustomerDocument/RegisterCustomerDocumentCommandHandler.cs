using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers;
using System.IO;
using MediatR;

namespace Crm.Application.Documents.RegisterCustomerDocuments
{
    public class RegisterCustomerDocumentCommandHandler : IRequestHandler<RegisterCustomerDocumentCommand, DocumentDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCustomerDocumentCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = CustomerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<DocumentDto> Handle(RegisterCustomerDocumentCommand request, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            await using var memoryStream = new MemoryStream();
            await request.DocumentData.CopyToAsync(memoryStream);

            var documentId = customer.CreateDocument(request.DocumentType, memoryStream.ToArray(), request.DocumentStatus, request.AuditedBy, request.AuditedDate);
     
            await _unitOfWork.CommitAsync(cancellationToken);

            return new DocumentDto { Id = documentId.Value };
        }
    }
}
