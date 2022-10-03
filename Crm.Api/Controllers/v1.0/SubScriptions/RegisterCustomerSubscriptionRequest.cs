using System;
using System.ComponentModel.DataAnnotations;


namespace Crm.Api.Customers
{
    public class RegisterCustomerSubscriptionRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Rev { get; set; }

        [Required]
        public decimal SetupFee { get; set; }

        [Required]
        public decimal MontlhyFee { get; set; }

        [Required]
        public decimal TransactionFee { get; set; }

    }
}
