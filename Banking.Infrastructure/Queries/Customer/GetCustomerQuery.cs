using Banking.Infrastructure.Models.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Infrastructure.Customer.Querries
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }
    }
}
