using Banking.Infrastructure.Queries.Account;
using FluentValidation;

namespace Banking.Manager.Account.Validators
{
    public class GetAccountTransactionQueryValidator : AbstractValidator<GetAccountTransactionQuery>
    {
        public GetAccountTransactionQueryValidator()
        {
            RuleFor(x => x.AccountId).NotNull().WithMessage("Account Id is required.");
        }
    }
}
