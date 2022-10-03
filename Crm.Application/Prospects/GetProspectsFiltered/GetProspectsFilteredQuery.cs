
using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Prospects.GetProspectsFiltered
{
    public class GetProspectsFilteredQuery : IRequest<List<ProspectDetailsDto>>
    {
        public string Filter { get; set; }
        public GetProspectsFilteredQuery(string filter)
        {
            Filter = filter;
        }
    }
}
