using Banking.Infrastructure.Account.Queries;
using FluentValidation;

namespace Banking.Manager.Account.Validators
{
    public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidator()
        {
            RuleFor(x => x.AccountId).NotNull().WithMessage("Account Id is required.");
        }
    }
}
