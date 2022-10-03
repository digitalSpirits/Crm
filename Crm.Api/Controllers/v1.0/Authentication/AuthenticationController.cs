using Crm.Api.Authentication;
using Crm.Api.Services;
using Crm.Application.Authentication.ChangeActiveToken;
using Crm.Application.Authentication.GetAuthenticationToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Authentication)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(TokenDto), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthenticationRequest auth)
        {
            var authentication = await _mediator.Send(new GetAuthenticatedQuery(auth.UserName, auth.Password));

            if (authentication != null)
                 await _mediator.Send(new ChangeActiveTokenCommand(authentication.Id));

            return Ok(authentication);
        }
    }
}
