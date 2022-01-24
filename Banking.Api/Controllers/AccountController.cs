using Banking.Infrastructure.Account.Queries;
using Banking.Infrastructure.Models;
using Banking.Infrastructure.Models.Account;
using Banking.Infrastructure.Queries.Account;
using Banking.Manager.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Banking.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/accounts")]
    [ProducesResponseType(typeof(HttpServiceResponseBase), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(HttpServiceResponseBase), (int)HttpStatusCode.InternalServerError)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates account
        /// </summary>
        /// <param name="" balance="" customerId="" ></param>
        [HttpPost]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AccountCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, new HttpServiceResponseBase<string> { Data = result, Code = HttpStatusCode.Created });
        }

        /// <summary>
        /// Gets account information
        /// </summary>
        /// <param  accountId="" ></param>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Get([FromQuery] GetAccountQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(new HttpServiceResponseBase<AccountDto> { Data = result, Code = HttpStatusCode.OK });
        }

        /// <summary>
        /// Creates account transaction information - (TransactionType; add, withdraw)
        /// </summary>
        /// <param  accountId=""  amount ="" transactionType =""></param>
        [HttpPost("accountTransaction")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AccountTransactionCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, new HttpServiceResponseBase<string> { Data = result, Code = HttpStatusCode.Created });
        }

        /// <summary>
        /// Gets account transaction information
        /// </summary>
        /// <param  accountId="" ></param>
        [HttpGet("{accountId}/accountTransaction")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Get([FromQuery] GetAccountTransactionQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(new HttpServiceResponseBase<AccountTransactionDto> { Data = result, Code = HttpStatusCode.OK });
        }
        /// <summary>
        /// Gets customer account's transaction information period of time
        /// </summary>
        /// <param  accountId="" startDate="" endDate ="" ></param>
        [HttpGet("{customerId}/{startDate}/{endDate}/customerTransaction")]
        [ProducesResponseType(typeof(HttpServiceResponseBase<string>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Get([FromQuery] GetCustomerAccountTransactionQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(new HttpServiceResponseBase<AccountTransactionDto> { Data = result, Code = HttpStatusCode.OK });
        }

    }
}
