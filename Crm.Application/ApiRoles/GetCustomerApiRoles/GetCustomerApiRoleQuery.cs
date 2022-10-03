using Crm.Application.ApiRoles;
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.ApiKeys.GetCustomerApiRoles
{
    public class GetCustomerApiRoleQuery : IRequest<List<ApiRoleDto>>
    {
        public Guid CustomerId { get; set; }
        public GetCustomerApiRoleQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
