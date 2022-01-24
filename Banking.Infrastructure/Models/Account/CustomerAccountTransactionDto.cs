using System.Collections.Generic;

namespace Banking.Infrastructure.Models.Account
{
    public class CustomerAccountTransactionDto
    {
        public string AccountId { get; set; }
        public List<AccountTransactionDto> AccountTransactions { get; set; }
    }
}
