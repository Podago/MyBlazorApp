using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyBlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> GetOrders(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrdersAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<Order>>> GetOrder(int orderId)
        {
            var result = await _orderService.GetOrderAsync(orderId);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet("orderStatus/{orderStatusUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> GetOrderByStatus(string orderStatusUrl)
        {
            var result = await _orderService.GetOrdersByStatusAsync(orderStatusUrl);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
