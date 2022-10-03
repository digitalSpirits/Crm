
using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Prospects.GetProspects
{
    public class GetProspectQuery : IRequest<List<ProspectDetailsDto>>
    {
        public GetProspectQuery()
        {

        }
    }
}
