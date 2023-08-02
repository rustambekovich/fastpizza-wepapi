using FastPizza.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Dtos.OrdersDto
{
    public class OrderDto
    {
        public long CustomerId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderType OrderType { get; set; }
        public List<long> ProductID { get; set; }
    }
}
