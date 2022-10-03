using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Crm.Application.Prospects;
using System;
using Crm.Application.Prospects.RegisterProspectConfirmations;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Confirm)]
    [ApiController]
    public class ConfirmController : Controller
    {
        private readonly IMediator _mediator;

        public ConfirmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Confirm prospect by id and token
        /// </summary>
        /// <param name="prospectId"></param>
        /// <param name="tk"></param>
        [Route("{prospectId}/{tk}")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProspectDetailsDto>> RegisterProspectConfirmationAsync([FromRoute] Guid prospectId, [FromRoute] string tk)
        {
            if (tk == null) return NotFound();

            var confirm = await _mediator.Send(new RegisterProspectConfirmationCommand(prospectId, tk));
            if (confirm == null)
                return NotFound();

            return Ok(confirm);
        }
    }
}
