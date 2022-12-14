using System;

namespace Crm.Application.Transfers.GetBankAccountTransfers
{
    public class TransferDto
    {
        public string PublicId { get; set; }

        public Guid BankAccountId { get; set; }

        public string Side { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
        
        public byte Status { get; set; }

        public byte Type { get; set; }

        public DateTime Date { get; set; }
    }
}
