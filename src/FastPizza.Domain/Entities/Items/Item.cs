namespace FastPizza.Domain.Entities.Items
{
    public class Item : Auditable
    {
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public long Quantity { get; set; }
        public float TotalPrice { get; set; }
        public float ResultPrice { get; set; }
    }
}
