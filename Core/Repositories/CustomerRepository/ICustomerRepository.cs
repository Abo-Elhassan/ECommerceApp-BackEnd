using Core.Entities;
using Core.Repositories.GenericRepository;

namespace Core.Repositories.CustomerRepository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetCustomerByFirstName(string firstName);
        Task<Customer> GetCustomerByLastName(string lastName);
        Task<Customer> GetCustomerByEmail(string sddress);
    }
}