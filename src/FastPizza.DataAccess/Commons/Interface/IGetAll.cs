

using FastPizza.DataAccess.Utils;

public interface IGetAll<TEntity>
{
    public Task<IList<TEntity>> GetAllAsync(PaginationParams @params);
}
