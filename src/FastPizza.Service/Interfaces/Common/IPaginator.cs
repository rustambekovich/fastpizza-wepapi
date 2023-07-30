using FastPizza.DataAccess.Utils;

namespace FastPizza.Service.Interfaces.Common;


public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);
}
