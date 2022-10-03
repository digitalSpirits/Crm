using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using Crm.Application.Customers.GetCustomerDetails;
using Crm.Application.Customers.GetCustomers;
using Crm.Application.Customers.GetCustomersFiltered;
using Crm.Application.Customers.RemoveCustomer;
using Crm.Application.Customers.ChangeCustomer;
using Crm.Application.Customers.RegisterCustomer;
using Crm.Application.Customers;
using Crm.Api.Customers;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get customers 
        /// </summary>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CustomerDetailsDto>>> GetCustomersAsync()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());
            if (!customers.Any())
                return NotFound();

            return Ok(customers);
        }

        /// <summary>
        /// Get customer details
        /// </summary>
        /// <param name="customerId"></param>
        //[HttpGet]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<CustomerDetailsDto>> GetCustomerDetailsAsync(Guid customerId)
        //{
        //    var customer = await _mediator.Send(new GetCustomerDetailsQuery(customerId));
        //    if (customer == null)
        //        return NotFound();

        //    return Ok(customer);
        //}

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="customerId"></param>
        [Route("{customerId}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomerByIdAsync([FromRoute] Guid customerId)
        {
            var customer = await _mediator.Send(new GetCustomerDetailsQuery(customerId));
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> RegisterCustomerAsync([FromBody] RegisterCustomerRequest request)
        {

            var customer = await _mediator.Send(new RegisterCustomerCommand(request.Username, request.Password, request.Token, request.Name, request.Surname, request.Email, request.Type, request.Country, request.City, request.Address, request.Phone, request.ProfileRisk, request.ActivationDate, request.CloseDate, request.UpdateDate));

            return Created(string.Empty, customer);
        }

        /// <summary>
        /// filter customer
        /// </summary>
        /// <param name="request"></param>
        [Route("filter")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> FilterCompanyAsync([FromBody] FilterCustomersRequest request)
        {
            var customer = await _mediator.Send(new GetCustomersFilteredQuery(request.Filter));
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// update a customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        [Route("{customerId}")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeCustomerAsync([FromRoute]Guid customerId, [FromBody] ChangeCustomerRequest request)
        {
            await _mediator.Send(new ChangeCustomerCommand(customerId, request.Name, request.Surname, request.Email, request.Type, request.Country, request.City, request.Address, request.Phone, request.ProfileRisk, DateTime.UtcNow));

            return Ok();
        }

        /// <summary>
        /// delete a customer by id
        /// </summary>
        /// <param name="customerId"></param>
        [Route("{customerId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCustomerAsync([FromRoute] Guid customerId)
        {
            await _mediator.Send(new RemoveCustomerCommand(customerId));

            return Ok();
        }

    }
}
