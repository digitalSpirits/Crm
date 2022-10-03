using System;

namespace Crm.Api
{
    public class RegisterCustomerBankTransferRequest
    {

        public Guid BankAccountId { get; set; }

        public string Side { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public byte Status { get; set; }

        public byte Type { get; set; }

    }
}
