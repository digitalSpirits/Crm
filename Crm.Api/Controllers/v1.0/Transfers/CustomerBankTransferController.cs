using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using Crm.Application.Transactions;
using Crm.Application.Transfers.GetBankAccountTransfers;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Crm.Application.Transfers.RegisterBankAccountTransfer;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    public class CustomerBankTransferController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerBankTransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get bank account transactions.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns>List of customer bank transfer</returns>
        [Route("{customerId}/bankAccounts/transfers")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransactionDetailsDto>>> GetCustomerBankTransfersAsync([FromRoute] Guid customerId)
        {
            var transfers = await _mediator.Send(new GetBankAccountTransfersQuery(customerId));
            if (!transfers.Any())
                return NotFound();

            return Ok(transfers);
        }

        /// <summary>
        /// Add transaction to a customer bank.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="bankAccountId">bank id</param>
        /// <param name="request">new Bank</param>
        [Route("{customerId}/bankAccounts/{bankAccountId}/transfers")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDto>> RegisterCustomerBankTransferAsync([FromRoute] Guid customerId, [FromRoute] Guid bankAccountId, [FromBody] RegisterCustomerBankTransferRequest request)
        {
            var amount = ((TransferType)request.Type == TransferType.Withdrawal ? -request.Amount : request.Amount);

            var transaction = await _mediator.Send(new RegisterBankAccountTransferCommand(bankAccountId, customerId, request.Side, request.Currency, amount, (TransferType)request.Type, (TransferStatus)request.Status));
            return Created(string.Empty, transaction);
        }


        /// <summary>
        /// delete a customer bank by bank id
        /// </summary>
        /// <param name="customerId">customer id</param>
        ///  <param name="bankAccountId">bank id</param>
        [Route("{customerId}/bankAccounts/{bankAccountId}/transfers/{transferId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCustomerBankTransferAsync([FromRoute] Guid customerId, [FromRoute] Guid bankAccountId)
        {
            await _mediator.Send(new RemoveBankAccountTransactionCommand(customerId, bankAccountId));

            return Ok();
        }
    }
}
