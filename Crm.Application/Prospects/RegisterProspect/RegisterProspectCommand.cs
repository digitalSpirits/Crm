using Crm.Application.Configuration.Commands;

namespace Crm.Application.Prospects.RegisterProspect
{
    public class RegisterProspectCommand : CommandBase<ProspectDto>
    {
        public RegisterProspectCommand(string email, string password, string country)
        {
            Email = email;
            Password = password;
            Country = country;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Country { get; set; }
    }
}
