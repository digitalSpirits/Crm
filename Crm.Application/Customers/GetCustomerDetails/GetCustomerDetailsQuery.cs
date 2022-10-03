using Crm.Application.Configuration.Queries;
using System;

namespace Crm.Application.Customers.GetCustomerDetails
{
    public class GetCustomerDetailsQuery : IQuery<CustomerDetailsDto>
    {
        public Guid CustomerId { get; set; }
        public GetCustomerDetailsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
