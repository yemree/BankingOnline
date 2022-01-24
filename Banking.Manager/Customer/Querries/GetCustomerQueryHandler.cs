using Banking.Data.Abstranctions;
using Banking.Infrastructure.Customer.Querries;
using Banking.Infrastructure.Exceptions;
using Banking.Infrastructure.Models.Customer;
using Banking.Manager.Customer.Validators;
using MediatR;
using MongoDB.Bson;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Customer.Querries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new GetCustomerQueryValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            if (!ObjectId.TryParse(request.CustomerId, out ObjectId customerObjectId))
            {
                throw new ApiException("Missing format", HttpStatusCode.BadRequest);
            }

            var customer = await _customerRepository.GetCustomerAsync(customerObjectId.ToString());
            if (customer == null)
            {
                throw new ApiException("Customer does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            return new CustomerDto
            {
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone
            };
        }
    }
}
