using MediatR;

namespace Banking.Infrastructure.Events
{
    public class CustomerCreatedEvent : INotification
    {
        public string CustomerId { get; set; }
    }
}
