using System;
using System.Text.Json.Serialization;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Prospects;
using MediatR;

namespace Crm.Application.Prospects
{
    public class MarkProspectEmailAsConfirmedCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public MarkProspectEmailAsConfirmedCommand(Guid id, ProspectId prospectId) : base(id)
        {
            ProspectId = prospectId;
        }

        public ProspectId ProspectId { get; }
    }
}