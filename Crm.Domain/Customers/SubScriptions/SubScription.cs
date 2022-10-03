using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Customers.SubScriptions
{
    public class SubScription : Entity
    {
        internal SubScriptionId Id;

        private string _name;

        private string _rev;

        private decimal _setupFee;

        private decimal _monthlyFee;

        private decimal _transactionFee;

        private bool _isRemoved;

        private DateTime? _updateDate;

        public SubScription(string name, string rev, decimal setupFee, decimal monthlyFee, decimal transactionFee, DateTime? updateDate)
        {
            Id = new SubScriptionId(Guid.NewGuid());
            _name = name;
            _rev = rev;
            _setupFee = setupFee;
            _monthlyFee = monthlyFee;
            _transactionFee = transactionFee;
            _updateDate = updateDate;
            _isRemoved = false;
        }

        private SubScription() {
            _isRemoved = false;
        }

        internal static SubScription CreateNew(string name, string rev, decimal setupFee, decimal monthlyFee, decimal transactionFee, DateTime? updateDate)
        {
            return new SubScription(name, rev, setupFee, monthlyFee, transactionFee, updateDate);
        }

        internal void ChangeSubScription(string name, string rev, decimal setupFee, decimal monthlyFee, decimal transactionFee, DateTime? updateDate)
        {
            _name = name;
            _rev = rev;
            _setupFee = setupFee;
            _monthlyFee = monthlyFee;
            _transactionFee = transactionFee;
            _updateDate = updateDate;
    }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
