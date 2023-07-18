

using FastPizza.DataAccess.Utils;

namespace AgileShop.DataAccess.Common.Interfaces;

public interface ISearchable<TEntity>
{
    public Task<(int ItemsCount, IList<TEntity>)> SearchAsync(string search, 
        PaginationParams @params);
}
