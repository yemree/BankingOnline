using Banking.Data.Abstranctions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Settings;
using MongoDB.Driver;
using System;

namespace Banking.Data
{
    public class BankingContext : IBankingContext
    {
        public IMongoClient MongoClient { get; }

        private readonly string _databaseName;
        public BankingContext(IMongoDbSettings mongoDbSettings)
        {
            MongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            _databaseName = mongoDbSettings.DatabaseName;
        }

        IMongoCollection<Customer> IBankingContext.Customers => MongoClient.GetDatabase(_databaseName).GetCollection<Customer>("Customers");

        IMongoCollection<Account> IBankingContext.Accounts => MongoClient.GetDatabase(_databaseName).GetCollection<Account>("Accounts");

        IMongoCollection<AccountTransaction> IBankingContext.AccountTransactions => MongoClient.GetDatabase(_databaseName).GetCollection<AccountTransaction>("AccountTransactions");
        IMongoCollection<CustomerAccount> IBankingContext.CustomerAccounts => MongoClient.GetDatabase(_databaseName).GetCollection<CustomerAccount>("CustomerAccounts");

        IMongoCollection<EventLog> IBankingContext.EventLogs => MongoClient.GetDatabase(_databaseName).GetCollection<EventLog>("EventLogs");
    }
}
