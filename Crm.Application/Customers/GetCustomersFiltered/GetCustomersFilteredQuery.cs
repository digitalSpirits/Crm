using Crm.Application.Customers.GetCustomerDetails;
using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Customers.GetCustomersFiltered
{
    public class GetCustomersFilteredQuery : IRequest<List<CustomerDetailsDto>>
    {
        public string Filter { get; set; }
        public GetCustomersFilteredQuery(string filter)
        {
            Filter = filter;
        }
    }
}
