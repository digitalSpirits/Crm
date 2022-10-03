using System;

namespace Crm.Application.Documents
{
    public class DocumentDetailsDto
    {
        public Guid Id { get; set; }

        public string DocumentType { get; set; }

        public byte DocumentStatus { get; set; }

        public string AuditedBy { get; set; }

        public DateTime? AuditedDate { get; set; }

    }
}
