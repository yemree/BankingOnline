using Banking.Infrastructure.Documents;
using System.Threading.Tasks;

namespace Banking.Data.Abstranctions
{
    public interface ICustomerRepository
    {
        Task<string> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerAsync(string id);
    }
}
