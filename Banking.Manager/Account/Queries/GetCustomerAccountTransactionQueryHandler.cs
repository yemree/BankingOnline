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
    public class GetCustomerAccountTransactionQueryHandler : IRequestHandler<GetCustomerAccountTransactionQuery, AccountTransactionDto>
    {
        private readonly IAccountRepository _accountRepository;
        public GetCustomerAccountTransactionQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountTransactionDto> Handle(GetCustomerAccountTransactionQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new GetCustomerAccountTransactionValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                throw new ApiException(message, HttpStatusCode.BadRequest);
            }

            if (!ObjectId.TryParse(request.CustomerId, out ObjectId customerObjectId))
            {
                throw new ApiException("Missing format", HttpStatusCode.BadRequest);
            }

            var customerTransactions = await _accountRepository.GetCustomerTransactionsAsync(customerObjectId.ToString(), request.StartDate, request.EndDate);
            if (customerTransactions == null)
            {
                throw new ApiException("Customer data not found", System.Net.HttpStatusCode.BadRequest);
            }

            return new AccountTransactionDto
            {
                Transactions = customerTransactions.Select(s => new TransactionDto { Amount = s.Amount, Id = s.Id, TransactionType = s.TransactionType }).ToList()
            };

        }
    }
}
