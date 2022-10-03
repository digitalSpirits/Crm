using Crm.Application.Configuration.Commands;
using Microsoft.AspNetCore.Http;
using System;

namespace Crm.Application.Documents.RegisterCustomerDocuments
{
    public class RegisterCustomerDocumentCommand : CommandBase<DocumentDto>
    {
        public RegisterCustomerDocumentCommand(Guid customerId, string documentType, IFormFile documentData, byte documentStatus, string auditedBy, DateTime? auditedDate)
        {
            CustomerId = customerId;
            DocumentType = documentType;
            DocumentData = documentData;
            DocumentStatus = documentStatus;
            AuditedBy = auditedBy;
            AuditedDate = auditedDate;
        }

        public Guid CustomerId { get; set; }

        public string DocumentType { get; set; }

        public IFormFile DocumentData { get; set; }

        public byte DocumentStatus { get; set; }

        public string AuditedBy { get; set; }

        public DateTime? AuditedDate { get; set; }
    }
}
