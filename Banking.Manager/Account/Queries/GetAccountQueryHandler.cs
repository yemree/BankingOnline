using Banking.Data.Abstractions;
using Banking.Infrastructure.Account.Queries;
using Banking.Infrastructure.Exceptions;
using Banking.Infrastructure.Models.Account;
using Banking.Manager.Account.Validators;
using MediatR;
using MongoDB.Bson;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Account.Queries
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new GetAccountQueryValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            if (!ObjectId.TryParse(request.AccountId, out ObjectId accountrObjectId))
            {
                throw new ApiException("Missing format", HttpStatusCode.BadRequest);
            }

            var account = await _accountRepository.GetAccountsAsync(accountrObjectId.ToString());
            if (account == null)
            {
                throw new ApiException("Account does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            return new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Balance = account.Balance,
                CustomerId = account.CustomerId
            };
        }
    }
}
