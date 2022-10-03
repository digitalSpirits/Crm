using System;

namespace Crm.Application.Prospects
{
    public class ProspectDetailsDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Country { get; set; }

    }
}
