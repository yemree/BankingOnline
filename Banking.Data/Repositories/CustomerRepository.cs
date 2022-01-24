using Banking.Data.Abstranctions;
using Banking.Infrastructure.Documents;
using Banking.Infrastructure.Exceptions;
using Banking.Infrastructure.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Banking.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IBankingContext _bankingContext;
        public CustomerRepository(IMongoDbSettings mongoDbSettings)
        {
            _bankingContext = new BankingContext(mongoDbSettings);
        }
        public async Task<string> CreateCustomerAsync(Customer customer)
        {           
            var registeredCustomer = await CheckCustomerAsync(customer.Email, customer.Phone);
            if (registeredCustomer != null)
            {
                throw new ApiException("You have already account", System.Net.HttpStatusCode.BadRequest);
            }

            await _bankingContext.Customers.InsertOneAsync(customer);
            return customer.Id.ToString();
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var customer = await _bankingContext.Customers.Find(filter).FirstOrDefaultAsync();
            return customer;
        }
        private async Task<Customer> CheckCustomerAsync(string email, string phone)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Email, email);
            filter |= Builders<Customer>.Filter.Eq(x => x.Phone, phone);

            var customer = await _bankingContext.Customers.Find(filter).FirstOrDefaultAsync();
            return customer;
        }
    }
}
