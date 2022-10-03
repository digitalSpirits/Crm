using System.Threading.Tasks;

namespace Crm.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);

         Task<Customer> GetByIdAsync(CustomerId id);

        void Remove (Customer customer);
    }
}
