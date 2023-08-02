using FastPizza.Domain.Entities.Orders;

namespace FastPizza.DataAccess.Interfaces.Orders;

public interface IOrderRepository : IRepository<Order, Order>,
    IGetAll<Order>, ISearchable<Order>
{

}
