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
using Crm.Application.ApiKeys.ChangeCustomerApiKey;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerApiKeysController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerApiKeysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// create api key
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="request"></param>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Route("{customerId}/apiKeys")]
        [HttpPost]
        public async Task<IActionResult> RegisterApiKey([FromRoute] Guid customerId, [FromBody] RegisterApiKeysRequest request)
        {
            var apiKey = await _mediator.Send(new RegisterCustomerApiKeyCommand(customerId, request.Name, request.RoleIds));

            return Ok(apiKey);
        }

        /// <summary>
        /// create api key
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="apiKeyId">api Key</param>
        /// <param name="request"></param>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Route("{customerId}/apiKeys/{apiKeyId}")]
        [HttpPut]
        public async Task<IActionResult> ChangeApiKey([FromRoute] Guid customerId, [FromRoute] Guid apiKeyId, [FromBody] ChangeApiKeysRequest request)
        {
            var apiKey = await _mediator.Send(new ChangeCustomerApiKeyCommand(customerId, apiKeyId, request.Name, request.RoleIds));

            return Ok(apiKey);
        }

        /// <summary>
        /// delete a customer apikey by apikey id
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="apiKeyId">apiKey id</param>
        [Route("{customerId}/apiKeys/{apiKeyId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCustomerApiKeyAsync([FromRoute] Guid customerId, [FromRoute] Guid apiKeyId)
        {
            await _mediator.Send(new RemoveCustomerApiKeyCommand(customerId, apiKeyId));

            return Ok();
        }
    }
}