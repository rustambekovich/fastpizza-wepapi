﻿namespace FastPizza.Domain.Exceptions.Orders
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException()
        {
            this.TitleMessage = "Order not found";
        }
    }
}
