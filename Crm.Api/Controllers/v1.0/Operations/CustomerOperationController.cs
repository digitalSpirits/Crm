using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Crm.Api.Customers;
using Microsoft.AspNetCore.Authorization;
using Crm.Application.Operations.GetCashAccount;
using Crm.Application.Operations.GetCashAccountBalance;
using Crm.Application.Operations.RegisterVirtualIBan;
using Newtonsoft.Json;
using Crm.Application.Operations.GetVirtualIbans;
using Crm.Application.Operations.RegisterMoneyTransfer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Crm.Api.Controllers
{
    [Route(ApiBase.Customers)]
    [ApiController]
    [Authorize]
    public class CustomerOperationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerOperationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// get account balances
        /// </summary>
        /// <param name="accountId"></param>
        [Route("{customerId}/banks/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetCashAccountByAccountId([FromRoute] string accountId)
        {
            var result = await _mediator.Send(new GetCashAccountQuery(accountId));

            return Ok(result);
        }

        /// <summary>
        /// get account balances
        /// </summary>
        /// <param name="accountId"></param>
        [Route("{customerId}/banks/{accountId}/balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetCashAccountBalanceByAccountId([FromRoute] string accountId)
        {
            var result = await _mediator.Send(new GetCashAccountBalanaceQuery(accountId));

            return Ok(result);
        }

        /// <summary>
        /// create virtual iban 
        /// </summary>
        /// <param name="accountId"></param>
        [Route("{customerId}/banks/{accountId}/virtual-iban")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateVirtualIBan([FromRoute] string accountId, [FromBody] RegisterVirtualIbanRequest request)
        {

            var result = await _mediator.Send(new RegisterVirtualIbanCommand(accountId, request.Description));

            return Ok(result);
        }

        /// <summary>
        /// create virtual iban 
        /// </summary>
        /// <param name="accountId"></param>
        [Route("{customerId}/banks/{accountId}/virtual-ibans")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetVirtualIBanByAccountId([FromRoute] string accountId)
        {
            var result = await _mediator.Send(new GetVirtualIbansQuery(accountId));

            return Ok(result);
        }

        /// <summary>
        /// create money transfer from virtual iban
        /// </summary>
        /// <param name="accountId"></param>
        [Route("{customerId}/banks/{accountId}/payments/money-transfers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateMoneyTransfer([FromRoute] string accountId, [FromBody] CreateMoneyTransferRequest request)
        {
            var result = await _mediator.Send(new CreateMoneyTransferCommand(accountId, JsonConvert.SerializeObject(request)));

            return Ok(result);
        }
    }
}
