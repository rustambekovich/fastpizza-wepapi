using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Entities.Products;
using FastPizza.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Interfaces.Useries
{
    public interface IUser : IRepository<User, User>,
        IGetAll<User>, ISearchable<User>
    {
        public Task<User?> GetByPhoneAsync(string phone);
    }
}
