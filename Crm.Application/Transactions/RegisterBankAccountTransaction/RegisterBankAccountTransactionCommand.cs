using Crm.Application.Configuration.Commands;
using Crm.Domain.Customers.BankAccounts.Transactions;
using System;

namespace Crm.Application.Transactions.RegisterBankAccountTransaction
{
    public class RegisterBankAccountTransactionCommand : CommandBase<TransactionDto>
    {
        public RegisterBankAccountTransactionCommand(Guid bankAccountId, Guid customerId, string currency, decimal amount, DateTime beginDate, DateTime? endDate, byte? expectedYield, byte? actualYield, TransactionStatus status)
        {
            BankAccountId = bankAccountId;
            CustomerId = customerId;
            Currency = currency;
            Amount = amount;
            BeginDate = beginDate;
            EndDate = endDate;
            ExpectedYield = expectedYield;
            ActualYield = actualYield;
            Status = status;
        }

        public Guid BankAccountId { get; set; }

        public Guid CustomerId { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? ExpectedYield { get; set; }

        public byte? ActualYield { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
