using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Customers.BankAccounts.Transfers
{
    public class Transfer : Entity
    {
        internal TransferId Id;

        internal string PublicId;

        private string _currency;

        private string _side;

        private decimal _amount;

        private TransferStatus _status;

        private TransferType _type;

        private DateTime _date;

        private bool _isRemoved;

        public Transfer(string publicId, string currency, string side, decimal amount, TransferStatus status, TransferType type, DateTime date)
        {
            Id = new TransferId(Guid.NewGuid());
            PublicId = publicId;
            _currency = currency;
            _side = side;
            _amount = amount;
            _status = status;
            _type = type;
            _date = date;
            _isRemoved = false;

            // add domian event
        }

        internal static Transfer CreateNew(string publicId, string currency, string side, decimal amount, TransferStatus status, TransferType type, DateTime date)
        {
            return new Transfer(publicId, currency, side, amount, status, type, date);
        }
        internal void ChangeStatus(TransferStatus status)
        {
            _status = status;
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}