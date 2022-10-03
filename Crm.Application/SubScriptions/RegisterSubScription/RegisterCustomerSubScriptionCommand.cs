using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.SubScriptions
{
    public class RegisterCustomerSubScriptionCommand : CommandBase<SubScriptionDto>
    {
        public RegisterCustomerSubScriptionCommand(Guid companyId, string name, string rev, decimal setupFee, decimal monthlyFee, decimal transactionFee, DateTime? updateDate)
        {
            CompanyId = companyId;
            Name = name;
            Rev = rev;
            SetupFee = setupFee;
            MonthlyFee = monthlyFee;
            TransactionFee = transactionFee;
            UpdateDate = updateDate;
        }

        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public string Rev { get; set; }

        public decimal SetupFee { get; set; }

        public decimal MonthlyFee { get; set; }

        public decimal TransactionFee { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
