using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.BankAccounts.RegisterCustomerBankAccount
{
    public class RegisterCustomerBankAccountCommand : CommandBase<BankAccountDto>
    {
        public RegisterCustomerBankAccountCommand(Guid customerId,string currency, string description, string beneficiaryName, string bankName, string bankBrankName, string bankAddress, string bankSwiftBic, string bankAccountNumber, string iban, string intermediaryBankName, string intermediaryBankSwiftBic, string intermediaryBankAddress)
        {
            CustomerId = customerId;
            Currency = currency;
            Description = description;
            BeneficiaryName = beneficiaryName;
            BankName = bankName;
            BankBrankName = bankBrankName;
            BankAddress = bankAddress;
            BankSwiftBic = bankSwiftBic;
            BankAccountNumber = bankAccountNumber;
            Iban = iban;
            IntermediaryBankName = intermediaryBankName;
            IntermediaryBankSwiftBic = intermediaryBankSwiftBic;
            IntermediaryBankAddress = intermediaryBankAddress;
        }

        public Guid CustomerId { get; set; }
        public string Currency { get; set; }

        public string Description { get; set; }

        public string BeneficiaryName { get; set; }

        public string BankName { get; set; }

        public string BankBrankName { get; set; }

        public string BankAddress { get; set; }

        public string BankSwiftBic { get; set; }

        public string BankAccountNumber { get; set; }


        public string Iban { get; set; }

        public string IntermediaryBankName { get; set; }

        public string IntermediaryBankSwiftBic { get; set; }

        public string IntermediaryBankAddress { get; set; }

        
    }
}
