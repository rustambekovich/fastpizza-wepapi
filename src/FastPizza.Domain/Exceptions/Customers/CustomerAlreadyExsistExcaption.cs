namespace FastPizza.Domain.Exceptions.Customers;

public class CustomerAlreadyExsistExcaption : AlreadyExistsExcaption
{
    public CustomerAlreadyExsistExcaption()
    {
        TitleMessage = "emil already exists";
    }

    public CustomerAlreadyExsistExcaption(string email)
    {
        TitleMessage = "This emil is already registered";
    }
}
