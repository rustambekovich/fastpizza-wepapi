using AgileShop.DataAccess.Common.Interfaces;
using FastPizza.DataAccess.ViewModels.Users;
using FastPizza.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Interfaces.Custumers
{
    public interface ICustomerRepository : IRepository<Customer, CostumerViewModel>,
        IGetAll<CostumerViewModel>, ISearchable<CostumerViewModel>
    {
        public Task<Customer?> GetByEmailAsync(string email);
    }
}
