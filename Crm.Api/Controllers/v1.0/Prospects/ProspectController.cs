using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using Crm.Application.Prospects.GetProspects;
using Crm.Application.Prospects;
using Crm.Application.Prospects.GetProspectDetails;
using Crm.Application.Prospects.RegisterProspect;
using Crm.Application.Prospects.GetProspectsFiltered;
using Crm.Application.Prospects.RemoveProspect;
using Crm.Api.Prospects;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Prospect)]
    [ApiController]
    public class ProspectController : Controller
    {
        private readonly IMediator _mediator;

        public ProspectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get prospect 
        /// </summary>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProspectDetailsDto>>> GetProspectsAsync()
        {
            var prospects = await _mediator.Send(new GetProspectQuery());
            if (!prospects.Any())    
                return NotFound();

            return Ok(prospects);
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// /// <param name="prospectId"></param>
        [Route("{prospectId}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProspectDetailsDto>> GetProspectByIdAsync([FromRoute] Guid prospectId)
        {
            var prospect = await _mediator.Send(new GetProspectDetailsQuery(prospectId));
            if (prospect == null)
                return NotFound();

            return Ok(prospect);
        }

        /// <summary>
        /// Create a new prospect
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProspectDto>> RegisterProspectAsync([FromBody] RegisterProspectRequest request)
        {

            var prospect = await _mediator.Send(new RegisterProspectCommand(request.Email, request.Password, request.Country));

            return Created(string.Empty, prospect);
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
        public async Task<ActionResult<ProspectDto>> FilterProspectAsync([FromBody] FilterProspectsRequest request)
        {
            var prospect = await _mediator.Send(new GetProspectsFilteredQuery(request.Filter));
            if (prospect == null)
                return NotFound();

            return Ok(prospect);
        }

        /// <summary>
        /// delete a prospect by id
        /// </summary>
        /// <param name="prospectId"></param>
        [Route("{prospectId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveProspectAsync([FromRoute] Guid prospectId)
        {
            await _mediator.Send(new RemoveProspectCommand(prospectId));

            return Ok();
        }

    }
}
