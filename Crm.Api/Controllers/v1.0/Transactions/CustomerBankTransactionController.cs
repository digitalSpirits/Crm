using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using Crm.Api.Banks;
using Crm.Application.Transactions.GetBankAccountTransacions;
using Crm.Application.Transactions;
using Crm.Application.Transactions.RegisterBankAccountTransaction;
using Crm.Domain.Customers.BankAccounts.Transactions;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerBankTransactionController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerBankTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get bank account transactions.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns>List of customer bank transactions</returns>
        [Route("{customerId}/bankAccount/transactions")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransactionDetailsDto>>> GetCustomerBankTransactionsAsync([FromRoute] Guid customerId)
        {
            var transactions = await _mediator.Send(new GetBankAccountTransactionsQuery(customerId));
            if (!transactions.Any())
                return NotFound();

            return Ok(transactions);
        }

        /// <summary>
        /// Add transaction to a customer bank.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="bankAccountId">bank id</param>
        /// <param name="request">new Bank</param>
        [Route("{customerId}/bankAccounts/{bankAccountId}/transactions")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDto>> RegisterCustomerBankTransactionAsync([FromRoute] Guid customerId, [FromRoute] Guid bankAccountId, [FromBody] RegisterCustomerBankTransactionRequest request)
        {
            var transaction = await _mediator.Send(new RegisterBankAccountTransactionCommand(bankAccountId, customerId, request.Currency, request.Amount, request.BeginDate, request.EndDate, request.ExpectedYield, request.ActualYield, (TransactionStatus)request.Status));
            return Created(string.Empty, transaction);
        }


        /// <summary>
        /// update transaction
        /// </summary>
        /// <param name="customerId">customer id </param>
        /// <param name="bankAccountId">bank id</param>
        /// <param name="request">change bank details</param>
        [Route("{customerId}/bankAccounts/{bankAccountId}/transactions")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeCustomerBankTransactionAsync([FromRoute]Guid customerId, [FromRoute] Guid bankAccountId, [FromBody] RegisterCustomerBankRequest request)
        {
          //  await _mediator.Send(new ChangeCustomerBankAccountCommand(customerId, bankId, request.Currency, request.Description, request.BeneficiaryName, request.BankName, request.BankBrankName, request.BankAddress, request.BankSwiftBic, request.BankAccountNumber, request.Iban, request.IntermediaryBankName, request.IntermediaryBankSwiftBic, request.IntermediaryBankAddress));

            return Ok();
        }

        /// <summary>
        /// delete a customer bank by bank id
        /// </summary>
        /// <param name="customerId">customer id</param>
        ///  <param name="bankAccountId">bank id</param>
        [Route("{customerId}/bankAccounts/{bankAccountId}/transaction")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCustomerBankTransactionAsync([FromRoute] Guid customerId, [FromRoute] Guid bankAccountId)
        {
            await _mediator.Send(new RemoveBankAccountTransactionCommand(customerId, bankAccountId));

            return Ok();
        }
    }
}
