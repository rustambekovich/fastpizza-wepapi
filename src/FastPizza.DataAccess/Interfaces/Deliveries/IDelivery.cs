using FastPizza.Domain.Entities.Deliveries;
namespace FastPizza.DataAccess.Interfaces.Deliveries
{
    public interface IDelivery : IRepository<Delivery, Delivery>,
        IGetAll<Delivery>, ISearchable<Delivery>
    {
        public Task<Delivery?> GetByPhoneAsync(string phone);
    }
}
