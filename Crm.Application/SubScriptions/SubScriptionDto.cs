using System;

namespace Crm.Application.SubScriptions
{
    public class SubScriptionDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Name { get; set; }

        public int Rev { get; set; }

        public double SetupFee { get; set; }

        public double MontlyFee { get; set; }

        public double TransactionFee { get; set; }
    }
}
