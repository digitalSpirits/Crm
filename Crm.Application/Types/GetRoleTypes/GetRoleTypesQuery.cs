using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Types
{
    public class GetRoleTypesQuery : IRequest<List<CategoryRoleTypesDto>>
    {
        public GetRoleTypesQuery()
        {
        }
    }
}
