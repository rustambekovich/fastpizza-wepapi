namespace FastPizza.Domain.Entities.Products
{
    public class Product : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public float Unitprice { get; set; }
        public long CategoryID { get; set; }

    }
}
