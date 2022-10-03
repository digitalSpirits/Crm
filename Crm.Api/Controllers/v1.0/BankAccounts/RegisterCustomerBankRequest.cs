using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Api.Banks
{
    public class RegisterCustomerBankRequest
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Currency is required")]
        public string Currency { get; set; }

        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Beneficiary name is required")]
        public string BeneficiaryName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Bank name is required")]
        public string BankName { get; set; }

        public string BankBrankName { get; set; }

        public string BankAddress { get; set; }

        public string BankSwiftBic { get; set; }

        public string BankAccountNumber { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Iban is required")]
        public string Iban { get; set; }

        public string IntermediaryBankName { get; set; }

        public string IntermediaryBankSwiftBic { get; set; }

        public string IntermediaryBankAddress { get; set; }
    }
}
