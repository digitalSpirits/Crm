using System;

namespace Crm.Application.Transactions
{
    public class TransactionDetailsDto
    {
        public Guid Id { get; set; }

        public Guid BankAccountId { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? ExpectedYield { get; set; }

        public decimal? ActualYield { get; set; }

        public byte Status { get; set; }

    }
}
