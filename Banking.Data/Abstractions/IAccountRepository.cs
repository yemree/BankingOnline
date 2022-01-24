using Banking.Infrastructure.Documents;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Data.Abstractions
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountsAsync(string accountId);
        Task<List<AccountTransaction>> GetAccountTransactionsAsync(string accountId);
        Task<List<AccountTransaction>> GetCustomerTransactionsAsync(string customerId, DateTime startDate, DateTime endDate);
        Task<string> CreateAccountAsync(Account account);
        Task<string> CreateAccountTransactionAsync(AccountTransaction accountTransaction);
        Task<UpdateResult> UpdateAccountBalanceAsync(Account account);

    }
}
