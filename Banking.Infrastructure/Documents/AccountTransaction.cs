using Banking.Infrastructure.Enums;

namespace Banking.Infrastructure.Documents
{
    public class AccountTransaction : Document
    {
        public string AccountId { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }
    }
}
