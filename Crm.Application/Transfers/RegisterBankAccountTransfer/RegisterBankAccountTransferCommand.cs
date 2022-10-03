using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers.BankAccounts.Transfers;
using System;

namespace Crm.Application.Transfers.RegisterBankAccountTransfer
{
    public class RegisterBankAccountTransferCommand : CommandBase<TransferDto>
    {
        public RegisterBankAccountTransferCommand(Guid bankAccountId, Guid customerId, string side, string currency, decimal amount, TransferType type, TransferStatus status)
        {
            BankAccountId = bankAccountId;
            CustomerId = customerId;
            Currency = currency;
            Side = side;
            Amount = amount;
            Type = type;
            Status = status;
        }

        public Guid BankAccountId { get; set; }

        public Guid CustomerId { get; set; }

        public string Currency { get; set; }

        public string Side { get; set; }

        public decimal Amount { get; set; }

        public TransferType Type { get; set; }

        public TransferStatus Status { get; set; }
    }
}
