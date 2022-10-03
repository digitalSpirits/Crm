
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.ApiKeys.GetCustomerApikeys
{
    public class GetCustomerApiKeyQuery : IRequest<List<ApiKeyDto>>
    {
        public Guid CustomerId { get; set; }
        public GetCustomerApiKeyQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
