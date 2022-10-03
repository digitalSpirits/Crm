using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Crm.Api.Customers;
using System.Net;
using Crm.Application.ApiKeys.RegisterCustomerApiKey;
using System.Net.Mime;
using System;
using Crm.Application.ApiKeys.RemoveCustomerApiKey;
using Crm.Application.ApiKeys.RegisterCustomerApiRole;
using System.Collections.Generic;
using Crm.Application.ApiRoles;
using Crm.Application.ApiKeys.GetCustomerApiRoles;
using System.Linq;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerApiRolesController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerApiRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get api role 
        /// </summary>
        /// /// <param name="request"></param>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ApiRoleDto>>> GetCustomersAsync([FromBody] ApiRolesRequest request)
        {
            var apiRoles = await _mediator.Send(new GetCustomerApiRoleQuery(request.CustomerId));
            if (!apiRoles.Any())
                return NotFound();

            return Ok(apiRoles);
        }

        /// <summary>
        /// create api role 
        /// </summary>
        /// <param name="request"></param>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Route("{customerId}/apiRole")]
        [HttpPost]
        public async Task<IActionResult> RegisterApiRole([FromBody] RegisterApiRolesRequest request)
        {
            var apiKey = await _mediator.Send(new RegisterCustomerApiRoleCommand(request.CustomerId, request.Roles));

            return Ok(apiKey);
        }
    }
}