namespace FastPizza.Domain.Exceptions.Customers
{
    public class CustomerExpiredException : ExpiredException
    {
        public CustomerExpiredException()
        {
            TitleMessage = "Customer data has expired!";
        }
    }
}
