using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.Documents.Events
{
    public class DocumentRemovedEvent : DomainEventBase
    {
        public DocumentId Documentid { get; }

        public DocumentRemovedEvent(DocumentId documentid)
        {
            Documentid = documentid;
        }
    }
}
