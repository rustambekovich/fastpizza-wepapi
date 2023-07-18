

using FastPizza.DataAccess.Utils;

namespace AgileShop.DataAccess.Common.Interfaces;

public interface IGetAll<TEntity>
{
    public Task<IList<TEntity>> GetAllAsync(PaginationParams @params);
}
