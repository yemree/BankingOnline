using Banking.Data.Abstractions;
using Banking.Data.Abstranctions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Exceptions;
using Banking.Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Data.Repositories
{

    public class AccountRepository : IAccountRepository
    {
        private readonly IBankingContext _bankingContext;
        public AccountRepository(IMongoDbSettings mongoDbSettings)
        {
            _bankingContext = new BankingContext(mongoDbSettings);
        }
        public async Task<Account> GetAccountsAsync(string accountId)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Id, accountId);
            return await _bankingContext.Accounts.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<AccountTransaction>> GetAccountTransactionsAsync(string accountId)
        {
            var filter = Builders<AccountTransaction>.Filter.Eq(x => x.AccountId, accountId);
            return await _bankingContext.AccountTransactions.Find(filter).ToListAsync();
        }
        public async Task<List<AccountTransaction>> GetCustomerTransactionsAsync(string customerId, DateTime startDate, DateTime endDate)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.CustomerId, customerId);
            var account = await _bankingContext.Accounts.Find(filter).FirstOrDefaultAsync();

            if (account == null)
            {
                throw new ApiException("Customer Account has not found", System.Net.HttpStatusCode.BadRequest);
            }
            var accountfilter =  Builders<AccountTransaction>.Filter.Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate && w.AccountId == account.Id);

            var transactions = await _bankingContext.AccountTransactions.Find(accountfilter).ToListAsync();

            return transactions;
        }
        public async Task<string> CreateAccountAsync(Account account)
        {
            await _bankingContext.Accounts.InsertOneAsync(account);
            return account.Id.ToString();
        }
        public async Task<UpdateResult> UpdateAccountBalanceAsync(Account account)
        {           
            var filter = Builders<Account>.Filter.Eq(x => x.Id, account.Id);
            var updatedAcc = Builders<Account>.Update.Set("Balance", account.Balance);
           var result =  await _bankingContext.Accounts.UpdateOneAsync(filter, updatedAcc);
            return result;
        }
        public async Task<string> CreateAccountTransactionAsync(AccountTransaction accountTransaction)
        {
            var account = await CheckAccountAsync(accountTransaction.AccountId);

            if (account == null)
            {
                throw new ApiException("Customer Account has not found", System.Net.HttpStatusCode.BadRequest);
            }

            await _bankingContext.AccountTransactions.InsertOneAsync(accountTransaction);

            return accountTransaction.Id.ToString();
        }
        private async Task<Account> CheckAccountAsync(string accountId)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Id, accountId);
            var account = await _bankingContext.Accounts.Find(filter).FirstOrDefaultAsync();
            return account;
        }

    }
}
