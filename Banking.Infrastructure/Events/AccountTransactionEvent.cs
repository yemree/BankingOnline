using Banking.Infrastructure.Enums;
using MediatR;

namespace Banking.Infrastructure.Events
{
    public class AccountTransactionEvent : INotification
    {
        public string AccountTransactionId { get; set; }
    }
}
