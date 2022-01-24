using Banking.Infrastructure.Enums;
using Banking.Manager.Account.Commands;
using FluentValidation;

namespace Banking.Manager.Account.Validators
{
    public class AccountTransactionCreateCommandValidator : AbstractValidator<AccountTransactionCreateCommand>
    {
        public AccountTransactionCreateCommandValidator()
        {
            RuleFor(x => x.AccountId).NotNull().WithMessage("Account Id is required.");
            RuleFor(x => x.Amount).NotNull().WithMessage("Amount is required.");
            RuleFor(x => x.TransactionType).NotNull().WithMessage("TransactionType is required.").Must(BeAValidParameter).WithMessage("Transation Type must be valid parameter 'add' or 'withdraw'");
        }
        private static bool BeAValidParameter(string arg)
        {
            return arg.Equals("add") || arg.Equals("withdraw");
        }

    }
}
