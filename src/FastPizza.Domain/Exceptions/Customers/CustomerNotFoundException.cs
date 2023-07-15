namespace FastPizza.Domain.Exceptions.Customers
{
    public class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException()
        {
            this.TitleMessage = "Customer not found";

        }
    }
}
