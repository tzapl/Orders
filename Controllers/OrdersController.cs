using Microsoft.AspNetCore.Mvc;
using Orders.Models;
using Orders.Services;
using System.Text.Json;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult SubmitOrders([FromBody] List<OrderRequest> orders)
        {
            if (orders == null || !orders.Any())
                return BadRequest("Orders cannot be null or empty");

            _orderService.AddOrders(orders);
           
            return Accepted();

        }
    }
}
