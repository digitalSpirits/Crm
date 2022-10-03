using System;
using System.Text.Json.Serialization;

namespace Crm.Application.Authentication.GetAuthenticationToken
{
    public class TokenDto
    {
        public Guid Id { get; set; }

        public string ActiveToken { get; set; }

        public long ExpireDate { get; set; }

    }
}
