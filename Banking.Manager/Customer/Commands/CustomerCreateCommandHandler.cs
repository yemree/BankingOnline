using Banking.Data.Abstranctions;
using Banking.Infrastructure.Events;
using Banking.Infrastructure.Exceptions;
using Banking.Manager.Customer.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Customer.Commands
{
    public class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand,string>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;

        public CustomerCreateCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<string> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new CustomerCreateCommandValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            var customer = new Banking.Infrastructure.Documents.Customer
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone
            };

            var customerId = await _customerRepository.CreateCustomerAsync(customer);

            _ = _mediator.Publish(new CustomerCreatedEvent
            {
                CustomerId = customerId
            }, cancellationToken);

            return customerId;
        }
    }
}
