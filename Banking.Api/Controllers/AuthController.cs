using Banking.Infrastructure.Models;
using Banking.Manager.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Banking.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Bearer token Authentication
        /// </summary>
        /// <param ></param>
        [HttpPost("token")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(TokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, new HttpServiceResponseBase<string> { Data = result, Code = HttpStatusCode.Created });
        }
    }
}
