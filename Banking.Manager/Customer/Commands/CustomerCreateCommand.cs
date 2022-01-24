using MediatR;

namespace Banking.Manager.Customer.Commands
{
    public class CustomerCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
