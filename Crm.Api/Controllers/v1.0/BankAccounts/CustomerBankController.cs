using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using Crm.Application.Customers;
using Crm.Api.Banks;
using Crm.Application.BankAccounts;
using Crm.Application.BankAccounts.GetCustomerBankAccounts;
using Crm.Application.BankAccounts.RegisterCustomerBankAccount;
using Crm.Application.BankAccounts.ChangeCustomerBankAccount;
using Crm.Application.BankAccounts.RemoveCustomerBankAccount;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerBankController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerBankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get customer babjs.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns>List of customer banks</returns>
        [Route("{customerId}/bankAccounts")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BankAccountDto>>> GetCustomerBanksAsync([FromRoute] Guid customerId)
        {
            var customers = await _mediator.Send(new GetCustomerBankAccountsQuery(customerId));
            if (!customers.Any())
                return NotFound();

            return Ok(customers);
        }

        /// <summary>
        /// Add customer bank.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="request">new Bank</param>
        [Route("{customerId}/bankAccounts")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> RegisterCustomerBankAsync([FromRoute] Guid customerId, [FromBody] RegisterCustomerBankRequest request)
        {

            var customer = await _mediator.Send(new RegisterCustomerBankAccountCommand(customerId, request.Currency, request.Description, request.BeneficiaryName, request.BankName, request.BankBrankName, request.BankAddress, request.BankSwiftBic, request.BankAccountNumber, request.Iban, request.IntermediaryBankName, request.IntermediaryBankSwiftBic, request.IntermediaryBankAddress));
            return Created(string.Empty, customer);
        }


        /// <summary>
        /// update a customer by id
        /// </summary>
        /// <param name="customerId">customer id </param>
        /// <param name="bankId">bank id</param>
        /// <param name="request">change bank details</param>
        [Route("{customerId}/bankAccounts/{bankId}")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeCustomerBankAsync([FromRoute]Guid customerId, [FromRoute] Guid bankId,[FromBody] RegisterCustomerBankRequest request)
        {
            await _mediator.Send(new ChangeCustomerBankAccountCommand(customerId, bankId, request.Currency, request.Description, request.BeneficiaryName, request.BankName, request.BankBrankName, request.BankAddress, request.BankSwiftBic, request.BankAccountNumber, request.Iban, request.IntermediaryBankName, request.IntermediaryBankSwiftBic, request.IntermediaryBankAddress));

            return Ok();
        }

        /// <summary>
        /// delete a customer bank by bank id
        /// </summary>
        /// <param name="customerId">customer id</param>
        ///  <param name="bankId">bank id</param>
        [Route("{customerId}/bankAccounts/{bankAccounts}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCustomerBankAsync([FromRoute] Guid customerId, [FromRoute] Guid bankId)
        {
            await _mediator.Send(new RemoveCustomerBankAccountCommand(customerId, bankId));

            return Ok();
        }
    }
}
