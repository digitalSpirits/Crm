using Crm.Api.Services;
using Crm.Application.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Types)]
    [ApiController]
    public class TypesController : Controller
    {
        private readonly IMediator _mediator;

        public TypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// get Roles
        /// </summary>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Route("Roles")]
        [HttpGet]

        public async Task<IActionResult> GetRoleTypes()
        {
            var rolesTypes = await _mediator.Send(new GetRoleTypesQuery());

            return Ok(rolesTypes);
        }
    }
}
