using MediatR;

namespace Banking.Infrastructure.Events
{
    public class AccountCreatedEvent : INotification
    {
        public string AccountId { get; set; }
    }
}
