
using Crm.Application.SubScriptions;
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.SubScriptions.GetSubScriptions
{
    public class GetSubScriptionsQuery : IRequest<List<SubScriptionDto>>
    {
        public GetSubScriptionsQuery()
        {
          
        }
    }
}
