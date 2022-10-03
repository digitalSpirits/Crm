using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crm.Api.Customers
{
    public class RegisterApiRolesRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public List<KeyValuePair<string, string>> Roles { get; set; }
    }
}
