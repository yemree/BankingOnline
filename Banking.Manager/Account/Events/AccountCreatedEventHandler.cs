using Banking.Data.Abstractions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Events;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Account.Events
{
    public class AccountCreatedEventHandler : INotificationHandler<AccountCreatedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;
        public AccountCreatedEventHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }
        public async Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventLog = new EventLog
            {
                Message = $"{notification.AccountId} account is created.",
                Data = JsonConvert.SerializeObject(notification.AccountId)
            };

            await _eventLogRepository.CreateEventLogAsync(eventLog);
        }
    }
}
