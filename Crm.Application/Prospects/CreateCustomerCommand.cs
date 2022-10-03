using System;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Prospects;
using MediatR;
using Newtonsoft.Json;

namespace Crm.Application.Prospects
{
    public class CreateCustomerCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public CreateCustomerCommand(Guid id, ProspectId prospectId) : base(id)
        {
            ProspectId = prospectId;
        }

        public ProspectId ProspectId { get; }
    }
}