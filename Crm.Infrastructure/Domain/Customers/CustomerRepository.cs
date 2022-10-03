using Crm.Domain.Customers;
using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Crm.Infrastructure.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CrmContext _context;
        public CustomerRepository(CrmContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Customer user)
        {
            await _context.Customers.AddAsync(user);
        }

        public async Task<Customer> GetByIdAsync(CustomerId id)
        {
            var  customer = await _context.Customers
                .Include(CustomerEntityTypeConfiguration.BankAccountsList)
                .Include(CustomerEntityTypeConfiguration.SubScriptionsList)
                .Include(CustomerEntityTypeConfiguration.DocumentsList)
                .Include(CustomerEntityTypeConfiguration.ApiKeysList)
                 .SingleOrDefaultAsync(x => x.Id == id);
         
            return customer;
        }

        public void Remove(Customer user)
        {
            _context.Remove(user);
        }
    }
}
