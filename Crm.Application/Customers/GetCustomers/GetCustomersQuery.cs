using Crm.Application.Customers.GetCustomerDetails;
using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Customers.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<CustomerDetailsDto>>
    {
        public GetCustomersQuery()
        {

        }
    }
}
