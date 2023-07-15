namespace FastPizza.Domain.Entities.Customers
{
    public class Customer : Auditable
    {
        public string full_name { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;
        public string ImagePathCustomer { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
