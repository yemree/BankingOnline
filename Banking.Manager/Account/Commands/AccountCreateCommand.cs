using MediatR;

namespace Banking.Manager.Account.Commands
{
    public class AccountCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string CustomerId { get; set; }
    }
}
