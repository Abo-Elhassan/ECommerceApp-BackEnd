using Core.Context;
using Core.Entities;
using Core.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;


namespace Core.Repositories.CustomerRepository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly StoreContext _storecontext;

        public CustomerRepository(StoreContext storecontext) : base(storecontext)
        {
            _storecontext = storecontext;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _storecontext.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Customer> GetCustomerByFirstName(string firstName)
        {
            return await _storecontext.Customers.FirstOrDefaultAsync(x => x.FirstName == firstName);

        }

        public async Task<Customer> GetCustomerByLastName(string lastName)
        {
            return await _storecontext.Customers.FirstOrDefaultAsync(x => x.LastName == lastName);

        }
    }


}
