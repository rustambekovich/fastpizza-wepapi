using FastPizza.DataAccess.Utils;

namespace FastPizza.Service.Common.Helpers;


public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);
}
