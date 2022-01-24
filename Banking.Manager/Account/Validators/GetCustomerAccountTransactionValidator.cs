using Banking.Infrastructure.Queries.Account;
using FluentValidation;
using System;

namespace Banking.Manager.Account.Validators
{
    public class GetCustomerAccountTransactionValidator : AbstractValidator<GetCustomerAccountTransactionQuery>
    {
        public GetCustomerAccountTransactionValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id is required.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("Start Date is required.").Must(date => date != default(DateTime)).WithMessage("Start Date must be a valid date");
            RuleFor(x => x.EndDate).NotNull().WithMessage("End Date is required.").Must(date => date != default(DateTime)).WithMessage("End Date must be a valid date");
        }
    }
}
