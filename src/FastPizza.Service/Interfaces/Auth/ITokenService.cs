using FastPizza.Domain.Entities.Customers;

namespace FastPizza.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenereateToken(Customer customer);
}
