using Banking.Data.Abstractions;
using Banking.Infrastructure.Exceptions;
using Banking.Infrastructure.Models.Account;
using Banking.Infrastructure.Queries.Account;
using Banking.Manager.Account.Validators;
using MediatR;
using MongoDB.Bson;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Account.Queries
{
    public class GetAccountTransactionHandler : IRequestHandler<GetAccountTransactionQuery, AccountTransactionDto>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountTransactionHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountTransactionDto> Handle(GetAccountTransactionQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new GetAccountTransactionQueryValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            if (!ObjectId.TryParse(request.AccountId, out ObjectId accountrObjectId))
            {
                throw new ApiException("Missing format", HttpStatusCode.BadRequest);
            }

            var accountTransactions = await _accountRepository.GetAccountTransactionsAsync(accountrObjectId.ToString());
            if (accountTransactions == null)
            {
                throw new ApiException("Customer does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            return new AccountTransactionDto
            {
                Transactions = accountTransactions.Select(s => new TransactionDto { Amount = s.Amount, Id = s.Id, TransactionType = s.TransactionType }).ToList()
            };

        }
    }
}
