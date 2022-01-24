using Banking.Infrastructure.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Banking.Infrastructure.Queries.Account
{
    public class GetCustomerAccountTransactionQuery : IRequest<AccountTransactionDto>
    {
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }

        [FromRoute(Name = "startDate")]
        public DateTime StartDate { get; set; }
        [FromRoute(Name = "endDate")]
        public DateTime EndDate { get; set; }
    }
}
