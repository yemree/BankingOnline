namespace Banking.Infrastructure.Models.Account
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string CustomerId { get; set; }
    }
}
