using FastPizza.Domain.Entities.Users;

namespace FastPizza.DataAccess.Interfaces.Useries
{
    public interface IUser : IRepository<User, User>,
        IGetAll<User>, ISearchable<User>
    {
        public Task<User?> GetByPhoneAsync(string phone);
    }
}
