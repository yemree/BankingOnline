using Banking.Infrastructure.Customer.Querries;
using Banking.Infrastructure.Models;
using Banking.Infrastructure.Models.Customer;
using Banking.Manager.Customer.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Banking.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    [ProducesResponseType(typeof(HttpServiceResponseBase), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(HttpServiceResponseBase), (int)HttpStatusCode.InternalServerError)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Creates Customer
        /// </summary>
        /// <param name="" Address="" Email="" Phone=""  ></param>
        [HttpPost]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] CustomerCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, new HttpServiceResponseBase<string> { Data = result, Code = HttpStatusCode.Created });
        }
        /// <summary>
        /// Gets Customer Information
        /// </summary>
        /// <param customerId=""  ></param>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetCustomerQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(new HttpServiceResponseBase<CustomerDto> { Data = result, Code = HttpStatusCode.OK });
        }
    }
}
