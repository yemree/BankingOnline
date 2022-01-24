using Banking.Data.Abstractions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Events;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Manager.Account.Events
{
    public class AccountBalanceUpdatedEventHandler : INotificationHandler<AccountBalanceUpdatedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;
        public AccountBalanceUpdatedEventHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }
        public async Task Handle(AccountBalanceUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var eventLog = new EventLog
            {
                Message = $"{notification.AccountId}/{notification.Balance} account/balance is updated.",
                Data = JsonConvert.SerializeObject(notification.AccountId)
            };

            await _eventLogRepository.CreateEventLogAsync(eventLog);
        }
    }
}
