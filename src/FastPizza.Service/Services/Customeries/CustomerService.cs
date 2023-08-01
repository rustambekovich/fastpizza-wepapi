using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Exceptions.Customers;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Interfaces.Common;
using FastPizza.Service.Interfaces.Customeries;

namespace FastPizza.Service.Services.Customeries
{
    public class CustomerService : ICustomer
    {
        private readonly IPaginator _paginator;
        private readonly ICustomerRepository _repositoryCustomer;

        public CustomerService(ICustomerRepository repository,
            IPaginator paginator)
        {
            this._paginator = paginator;
            this._repositoryCustomer = repository;
        }
        public async Task<long> CountAsync()
        {
            var resalt = await _repositoryCustomer.CountAsync();
            return resalt;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var dbresult = await _repositoryCustomer.GetByIdAsync(id);
            if (dbresult is null)
            {
                throw new CustomerNotFoundException();
            }
            else
            {
                var result = await _repositoryCustomer.DeleteAsync(id);
                return result > 0 ? true : false;
            }
        }

        public async Task<List<Customer>> GetAllAsync(PaginationParams PaginationParams)
        {
            var dbresult = await _repositoryCustomer.GetAllAsync(PaginationParams);
            if (dbresult is null)
            {
                throw new CustomerNotFoundException();
            }
            else
            {
                var count = await _repositoryCustomer.CountAsync();
                _paginator.Paginate(count, PaginationParams);
                return dbresult.ToList();
            }
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            var result = await _repositoryCustomer.GetByIdAsync(id);
            if (result is null)
            {
                throw new CustomerNotFoundException();
            }
            else
            {
                return result;
            }
        }

        public async Task<bool> UpdateAsync(long id, RegistrDto dto)
        {
            var resil = await _repositoryCustomer.GetByIdAsync(id);
            if (resil is null)
            {
                throw new CustomerNotFoundException();
            }
            else
            {
                Customer customer = new Customer();
                customer.FullName = dto.FullName;
                customer.PhoneNumber = dto.PhoneNumber;
                customer.Email = dto.Email;
                customer.ImagePathCustomer = "";
                customer.UpdatedAt = TimeHelper.GetDateTime();
                var resltdb = await _repositoryCustomer.UpdateAsync(id, customer);
                return resltdb > 0 ? true : false;
            }
        }
    }
}
