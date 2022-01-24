using Banking.Manager.Account.Commands;
using FluentValidation;

namespace Banking.Manager.Account.Validators
{
    public class AccountCreateCommandValidator : AbstractValidator<AccountCreateCommand>
    {
        public AccountCreateCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Account Name is required.");
            RuleFor(x => x.Balance).NotNull().WithMessage("Account Balance is required.");
        }
    }
}
