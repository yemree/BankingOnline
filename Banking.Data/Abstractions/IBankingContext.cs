using Banking.Infrastructure.Documents;
using MongoDB.Driver;

namespace Banking.Data.Abstranctions
{
    public interface IBankingContext
    {
        IMongoClient MongoClient { get; }
        IMongoCollection<Customer> Customers { get; }
        IMongoCollection<Account> Accounts { get; }
        IMongoCollection<AccountTransaction> AccountTransactions { get; }
        IMongoCollection<CustomerAccount> CustomerAccounts { get; }
        IMongoCollection<EventLog> EventLogs { get; }
    }
}
