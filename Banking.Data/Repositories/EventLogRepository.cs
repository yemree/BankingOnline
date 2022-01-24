using Banking.Data.Abstractions;
using Banking.Data.Abstranctions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Settings;
using System.Threading.Tasks;

namespace Banking.Data.Repositories
{
    public class EventLogRepository : IEventLogRepository
    {
        private readonly IBankingContext _bankingContext;
        public EventLogRepository(IMongoDbSettings mongoDbSettings)
        {
            _bankingContext = new BankingContext(mongoDbSettings);
        }

        public async Task CreateEventLogAsync(EventLog eventLog)
        {
            await _bankingContext.EventLogs.InsertOneAsync(eventLog);
        }
    }
}
