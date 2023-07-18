﻿using FastPizza.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FastPizza.Domain.Entities.Orders;

public class Order : Auditable
{
    public long customer_id { get; set; }
    public long delivery_id { get; set; }
    public OrderStatus status { get; set; }
    public float products_price { get; set; }
    public float delivery_price { get; set; }
    public float result_price { get; set; }
    public float latitude { get; set; }
    public float longitude { get; set; }
    public PaymentType Type { get; set; }
    public bool is_paid { get; set; }
    public string description { get; set; } = string.Empty;
    public OrderType OrderType { get; set; }
    public long BranchID { get; set; }
}