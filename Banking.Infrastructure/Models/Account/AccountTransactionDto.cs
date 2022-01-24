using Banking.Infrastructure.Enums;
using System.Collections.Generic;

namespace Banking.Infrastructure.Models.Account
{
    public class AccountTransactionDto 
    {
        public List<TransactionDto> Transactions { get; set; }
    }

    public class TransactionDto
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }
    }
}
