using System;

namespace Crm.Application.Customers
{
    public class CurrentPositionDto
    {
        public string Currency { get; set; }

        public decimal Cash { get; set; }

        public decimal Staked { get; set; }
    }
}
