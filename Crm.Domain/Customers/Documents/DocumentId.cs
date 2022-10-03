using System;
using Crm.Domain.SeedWork;

namespace Crm.Domain.Customers.Documents
{
    public class DocumentId : TypedIdValueBase
    {
        public DocumentId(Guid value) : base(value)
        {

        }
    }
}
