using AgileShop.DataAccess.Common.Interfaces;
using FastPizza.Domain.Entities.Products;

namespace FastPizza.DataAccess.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product, Product>,
        IGetAll<Product>, ISearchable<Product>
    {
    }
}
