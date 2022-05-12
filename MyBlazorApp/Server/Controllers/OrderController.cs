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
        public async Task<ActionResult<ServiceResponse<Order>>> GetOrder(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderAsync(orderId, cancellationToken);

            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("ordersByStatus/{orderStatusUrl}/{page}")]
        public async Task<ActionResult<ServiceResponse<OrderPage>>> GetOrdersByStatus(string orderStatusUrl, CancellationToken cancellationToken, int page = 1)
        {
            var result = await _orderService.GetOrdersByStatusAsync(orderStatusUrl, page, cancellationToken);

            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("page/{page}")]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> GetOrdersByPage(CancellationToken cancellationToken, int page = 1)
        {
            var result = await _orderService.GetOrdersByPageAsync(page, cancellationToken);

            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Order>>> AddOrder(Order order, CancellationToken cancellationToken)
        {
            var result = await _orderService.AddOrderAsync(order, cancellationToken);

            return Ok(result);
        }
    }
}
