using Banking.Infrastructure.Documents;
using System.Threading.Tasks;

namespace Banking.Data.Abstractions
{
    public interface IEventLogRepository
    {
        /// <summary>
        /// Creates a event log
        /// </summary>
        /// <param name="eventLog"></param>
        /// <returns></returns>
        Task CreateEventLogAsync(EventLog eventLog);
    }
}
