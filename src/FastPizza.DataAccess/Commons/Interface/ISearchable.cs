

using FastPizza.DataAccess.Utils;

public interface ISearchable<TEntity>
{
    public Task<(int ItemsCount, IList<TEntity>)> SearchAsync(string search,
        PaginationParams @params);
}
