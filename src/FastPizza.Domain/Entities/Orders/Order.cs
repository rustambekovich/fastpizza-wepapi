using FastPizza.Domain.Enums;

namespace FastPizza.Domain.Entities.Orders;

public class Order : Auditable
{
    public long CustomerId { get; set; }
    public long DeliveryId { get; set; }
    public OrderStatus Status { get; set; }
    public float ProductsPrice { get; set; }
    public float DeliveryPrice { get; set; }
    public float ResultPrice { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool IsPaid { get; set; }
    public string Description { get; set; } = string.Empty;
    public OrderType OrderType { get; set; }
    public long BranchID { get; set; }
}
