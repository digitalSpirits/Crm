using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.Documents.Events
{
    public class DocumentRegisterEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public DocumentId DocumentId { get; }

        public DocumentRegisterEvent(CustomerId customerId, DocumentId documentId)
        {
            CustomerId = customerId;
            DocumentId = documentId;
        }
    }
}
