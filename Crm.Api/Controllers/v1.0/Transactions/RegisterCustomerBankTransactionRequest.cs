using System;

namespace Crm.Api
{
    public class RegisterCustomerBankTransactionRequest
    {

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? ExpectedYield { get; set; }

        public byte? ActualYield { get; set; }

        public byte Status { get; set; }
    }
}
