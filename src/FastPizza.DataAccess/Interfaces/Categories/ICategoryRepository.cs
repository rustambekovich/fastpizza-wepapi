using AgileShop.DataAccess.Common.Interfaces;
using FastPizza.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Interfaces.Categories
{
    public interface ICategoryRepository : IRepository<Category, Category>, 
        IGetAll<Category>
    {
    }
}
