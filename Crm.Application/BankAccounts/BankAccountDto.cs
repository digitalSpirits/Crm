using Crm.Application.Transactions;
using Crm.Application.Transfers.GetBankAccountTransfers;
using System;
using System.Collections.Generic;

namespace Crm.Application.BankAccounts
{
    public class BankAccountDto
    {
        public Guid Id { get; set; }

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

        public List<TransactionDetailsDto> Transactions { get; set; }

        public List<TransferDto> Transfers { get; set; }
    }
}
