using Crm.Domain.Customers.BankAccounts.Transactions.Events;
using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Customers.BankAccounts.Transactions
{
    public class Transaction : Entity
    {
        internal TransactionId Id;

        private string _currency;

        private decimal _amount;

        private DateTime _beginDate;

        private DateTime? _endDate;

        private decimal? _expectedYield;

        private decimal? _actualYield;

        private TransactionStatus _status;

        private bool _isRemoved;

        public Transaction(string currency, decimal amount, DateTime beginDate, DateTime? endDate, decimal? expectedYield, decimal? actualYield, TransactionStatus status)
        {
            Id = new TransactionId(Guid.NewGuid());
            _currency = currency;
            _amount = amount;
            _beginDate = beginDate;
            _endDate = endDate;
            _expectedYield = expectedYield;
            _actualYield = actualYield;
            _status = status;
            _isRemoved = false;
        }

        internal static Transaction CreateNew(string currency, decimal amount, DateTime beginDate, DateTime? endDate, decimal? expectedYield, decimal? actualYield, TransactionStatus status)
        {
            return new Transaction(currency, amount, beginDate, endDate, expectedYield, actualYield, status);
         }

        internal void Change(string currency, decimal amount, DateTime beginDate, DateTime? endDate, decimal? expectedYield, decimal? actualYield, TransactionStatus status)
        {
            _currency = currency;
            _amount = amount;
            _beginDate = beginDate;
            _endDate = endDate;
            _expectedYield = expectedYield;
            _actualYield = actualYield;
            _status = status;
        }

        internal void ChangeStatus(TransactionStatus status)
        {
            _status = status;
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
