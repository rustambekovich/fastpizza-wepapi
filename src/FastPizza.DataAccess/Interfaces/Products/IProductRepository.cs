using AgileShop.DataAccess.Common.Interfaces;
using FastPizza.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product, Product>,
        IGetAll<Product>, ISearchable<Product>
    {
    }
}
