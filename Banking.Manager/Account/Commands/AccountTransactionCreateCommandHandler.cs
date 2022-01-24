using Banking.Data.Abstractions;
using Banking.Infrastructure.Enums;
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
    public class AccountTransactionCreateCommandHandler : IRequestHandler<AccountTransactionCreateCommand, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _mediator;
        public AccountTransactionCreateCommandHandler(IAccountRepository accountRepository,
            IMediator mediator)
        {
            _accountRepository = accountRepository;
            _mediator = mediator;
        }
        public async Task<string> Handle(AccountTransactionCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new AccountTransactionCreateCommandValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            var accountTransaction = new Banking.Infrastructure.Documents.AccountTransaction
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                TransactionType = request.TransactionType
            };
            var accountTransactionId = await _accountRepository.CreateAccountTransactionAsync(accountTransaction);

            await UpdateBalanceProcessAsync(request, cancellationToken);

            _ = _mediator.Publish(new AccountTransactionEvent
            {
                AccountTransactionId = accountTransactionId
            }, cancellationToken);

            return accountTransactionId;
        }
        private async Task UpdateBalanceProcessAsync(AccountTransactionCreateCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountsAsync(request.AccountId);
            if (request.TransactionType == "add")
            {
                account.Balance += request.Amount;
            }
            else
            {
                account.Balance -= request.Amount;
            }
            await _accountRepository.UpdateAccountBalanceAsync(account);

            _ = _mediator.Publish(new AccountBalanceUpdatedEvent
            {
                AccountId = account.Id,
                Balance = account.Balance
            }, cancellationToken);
        }
    }
}
