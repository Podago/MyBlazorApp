using Microsoft.AspNetCore.Mvc;

namespace MyBlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatuseService _orderStatuseService;

        public OrderStatusController(IOrderStatuseService orderStatuseService)
        {
            _orderStatuseService = orderStatuseService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderStatus>>>> GetOrderStatuses()
        {
            var result = await _orderStatuseService.GetOrderStatusesAsync();

            return Ok(result);
        }

        [HttpGet("{orderStatusId}")]
        public async Task<ActionResult<ServiceResponse<List<OrderStatus>>>> GetOrderStatuse(int orderStatusId)
        {
            var result = await _orderStatuseService.GetOrderStatuseAsync(orderStatusId);

            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
