using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Crm.Application.Customers;
using Crm.Api.Customers;
using Crm.Application.Documents.RegisterCustomerDocuments;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerDocumentController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add customer document.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="request">form</param>
        [Route("{customerId}/documents")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> RegisterCustomerDocumentAsync([FromRoute] Guid customerId, [FromForm] RegisterCustomerDocumentRequest request)
        {
            byte documentState = 0;
            var customer = await _mediator.Send(new RegisterCustomerDocumentCommand(customerId, request.DocumentType, request.DocumentData, documentState, request.AuditedBy, request.AuditedDate));

            return Created(string.Empty, customer);
        }
    }
}
