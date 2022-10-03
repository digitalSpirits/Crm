using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Prospects.RemoveProspect
{
    public class RemoveProspectCommand : CommandBase<Unit>
    {
        public Guid ProspectId { get; set; }

        public RemoveProspectCommand(Guid prospectId)
        {
            ProspectId = prospectId;
        }
    }
}
