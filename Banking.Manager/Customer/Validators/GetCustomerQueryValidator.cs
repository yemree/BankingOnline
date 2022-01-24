using Banking.Infrastructure.Customer.Querries;
using Banking.Manager.Customer.Querries;
using FluentValidation;

namespace Banking.Manager.Customer.Validators
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id is required");
        }
    }
}
