using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crm.Api.Customers
{
    public class ChangeApiKeysRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public List<Guid> RoleIds { get; set; }
    }
}
