using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Crm.Api.Customers;
using Crm.Application.SubScriptions;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerSubScriptionController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerSubScriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add customer subscription
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="request">new subscription</param>
        [Route("{customerId}/subscription")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubScriptionDto>> RegisterCustomerSubscriptionAsync([FromRoute] Guid customerId,[FromBody] RegisterCustomerSubscriptionRequest request)
        {
            var subscription = await _mediator.Send(new RegisterCustomerSubScriptionCommand(customerId,request.Name,request.Rev, request.SetupFee, request.MontlhyFee, request.TransactionFee, DateTime.UtcNow));

            return Created(string.Empty, subscription);
        }
    }
}
