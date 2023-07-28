using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Entities.Users;

namespace FastPizza.Service.Interfaces.UserAuth;

public interface ITokenUserService
{
    public string GenereateToken(User user);
}
