using AgileShop.DataAccess.Common.Interfaces;
using FastPizza.Domain.Entities.Categories;

namespace FastPizza.DataAccess.Interfaces.Categories
{
    public interface ICategoryRepository : IRepository<Category, Category>,
        IGetAll<Category>
    {
    }
}
