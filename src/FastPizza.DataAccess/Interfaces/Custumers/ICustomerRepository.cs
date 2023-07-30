
using FastPizza.DataAccess.ViewModels.Users;
using FastPizza.Domain.Entities.Customers;

namespace FastPizza.DataAccess.Interfaces.Custumers
{
    public interface ICustomerRepository : IRepository<Customer, Customer>,
        IGetAll<Customer>, ISearchable<Customer>
    {
        public Task<Customer?> GetByEmailAsync(string email);
        public Task<Customer?> GetByPhoneAsync(string phone);
    }
}
