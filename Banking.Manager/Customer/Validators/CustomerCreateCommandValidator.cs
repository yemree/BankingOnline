using Banking.Manager.Customer.Commands;
using FluentValidation;

namespace Banking.Manager.Customer.Validators
{
    public class CustomerCreateCommandValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Customer Name is required.");
            RuleFor(x => x.Email).NotNull().WithMessage("Customer Email is required.").EmailAddress().WithMessage("Customer Email is not valid!"); ;
            RuleFor(x => x.Address).NotNull().WithMessage("Customer Address is required"); 
            RuleFor(x => x.Phone).NotNull().WithMessage("Customer Phone is required");
        }
    }
}
