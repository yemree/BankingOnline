using Banking.Data.Abstractions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Events;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Customer.Events
{
    public class CustomerCreateHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;
        public CustomerCreateHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }
        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventLog = new EventLog
            {
                Message = $"{notification.CustomerId} customer is created.",
                Data = JsonConvert.SerializeObject(notification.CustomerId)
            };

            await _eventLogRepository.CreateEventLogAsync(eventLog);
        }
    }
}
