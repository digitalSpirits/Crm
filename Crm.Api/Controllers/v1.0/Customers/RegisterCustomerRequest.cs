using System;
using System.ComponentModel.DataAnnotations;


namespace Crm.Api.Customers
{
    public class RegisterCustomerRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Token { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string ProfileRisk { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
