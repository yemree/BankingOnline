using Banking.Infrastructure.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Infrastructure.Account.Queries
{
    public class GetAccountQuery : IRequest<AccountDto>
    {
        [FromRoute(Name = "accountId")]
        public string AccountId { get; set; }
    }
}
