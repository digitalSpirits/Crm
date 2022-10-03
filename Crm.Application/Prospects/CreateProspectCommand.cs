using System;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Prospects;
using MediatR;
using Newtonsoft.Json;

namespace Crm.Application.Prospects
{
    public class CreateProspectCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public CreateProspectCommand(Guid id, ProspectId prospectId) : base(id)
        {
            ProspectId = prospectId;
        }

        public ProspectId ProspectId { get; }
    }
}