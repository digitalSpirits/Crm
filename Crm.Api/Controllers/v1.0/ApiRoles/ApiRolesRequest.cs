using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Api.Customers
{
    public class ApiRolesRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

    }
}
