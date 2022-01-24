using Banking.Data.Abstractions;
using Banking.Infrastructure.Events;
using Banking.Infrastructure.Exceptions;
using Banking.Manager.Account.Validators;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Account.Commands
{
    public class AccountCreateCommandHandler : IRequestHandler<AccountCreateCommand, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _mediator;

        public AccountCreateCommandHandler(IAccountRepository accountRepository,
            IMediator mediator)
        {
            _accountRepository = accountRepository;
            _mediator = mediator;
        }
        public async Task<string> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new AccountCreateCommandValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            var account = new Banking.Infrastructure.Documents.Account
            {
                Name = request.Name,
                Balance = request.Balance,
                CustomerId = request.CustomerId
            };

            var customerId = await _accountRepository.CreateAccountAsync(account);

            _ = _mediator.Publish(new CustomerCreatedEvent
            {
                CustomerId = customerId
            }, cancellationToken);

            return customerId;
        }
    }
}
