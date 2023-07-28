namespace FastPizza.Domain.Exceptions.Users;

public class UserAlreadyExsistExcaption : AlreadyExistsExcaption
{
    public UserAlreadyExsistExcaption()
    {
        TitleMessage = "emil already exists";
    }

    public UserAlreadyExsistExcaption(string email)
    {
        TitleMessage = "This emil is already registered";
    }
}
