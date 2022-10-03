
using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Prospects.RegisterProspectConfirmations
{
    public class RegisterProspectConfirmationCommand : CommandBase<ProspectDto>
    {
        public Guid ProspectId { get; set; }
        public string Token { get; set; }
        public RegisterProspectConfirmationCommand(Guid prospectId, string token)
        {
            ProspectId = prospectId;
            Token = token;
        }
    }
}
