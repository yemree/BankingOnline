namespace Banking.Infrastructure.Documents
{
    public class Account : Document
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string CustomerId { get; set; }
    }
}
