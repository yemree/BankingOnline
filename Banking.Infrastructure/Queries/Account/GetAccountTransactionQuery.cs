using Banking.Infrastructure.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Infrastructure.Queries.Account
{
    public class GetAccountTransactionQuery : IRequest<AccountTransactionDto>
    {
        [FromRoute(Name = "accountId")]
        public string AccountId { get; set; }
    }
}
