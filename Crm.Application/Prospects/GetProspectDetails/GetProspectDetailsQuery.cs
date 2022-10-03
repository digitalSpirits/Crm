using Crm.Application.Configuration.Queries;
using System;

namespace Crm.Application.Prospects.GetProspectDetails
{
    public class GetProspectDetailsQuery : IQuery<ProspectDetailsDto>
    {
        public Guid ProspectId { get; set; }
        public GetProspectDetailsQuery(Guid prospectId)
        {
            ProspectId = prospectId;
        }
    }
}
