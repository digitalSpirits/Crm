using System;

namespace Crm.Domain.Customers.Documents
{
    public class Document
    {
        internal DocumentId Id;

        private string _documentType;

        private byte[] _documentData;

        private byte _documentStatus;

        private string _auditedBy;

        private DateTime? _auditedDate;

        private bool _isRemoved;

        public Document(string documentType, byte[] documentData, byte documentStatus, string auditedBy, DateTime? auditedDate)
        {
            Id = new DocumentId(Guid.NewGuid());
            _documentType = documentType;
            _documentData = documentData;
            _documentStatus = documentStatus;
            _auditedBy = auditedBy;
            _auditedDate = auditedDate;
        }

        private Document()
        {
            _isRemoved = false;
        }

        internal static Document CreateNew(string documentType, byte[] documentData, byte documentStatus, string auditedBy, DateTime? auditedDate)
        {
            return new Document(documentType, documentData, documentStatus, auditedBy, auditedDate);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }

    }
}
