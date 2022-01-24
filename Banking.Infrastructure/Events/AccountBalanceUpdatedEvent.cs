using MediatR;

namespace Banking.Infrastructure.Events
{
    public class AccountBalanceUpdatedEvent : INotification
    {
        public string AccountId { get; set; }

        public decimal Balance { get; set; }
    }
}
