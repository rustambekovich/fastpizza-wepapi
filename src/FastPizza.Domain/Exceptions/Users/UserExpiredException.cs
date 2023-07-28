namespace FastPizza.Domain.Exceptions.Users
{
    public class UserExpiredException : ExpiredException
    {
        public UserExpiredException()
        {
            TitleMessage = "Customer data has expired!";
        }
    }
}
