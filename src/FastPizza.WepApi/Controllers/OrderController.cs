using FastPizza.Service.Dtos.OrdersDto;
using FastPizza.Service.Interfaces.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _serviceOrder;

        public OrderController(IOrderService service)
        {
            this._serviceOrder = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatedAsync([FromBody] OrderDto orderDto )
            =>Ok(await _serviceOrder.CreateAsync(orderDto));
    }
}
