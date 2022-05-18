namespace OrderManagmen.Server.Controllers
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
            var statuses = await _orderStatuseService.GetOrderStatusesAsync();

            if(statuses == null)
            {
                return NotFound();
            }

            var result = new ServiceResponse<List<OrderStatus>>
            {
                Data = statuses
            };

            return Ok(result);
        }

        [HttpGet("{orderStatusId}")]
        public async Task<ActionResult<ServiceResponse<OrderStatus>>> GetOrderStatus(int orderStatusId)
        {
            var status = await _orderStatuseService.GetOrderStatuseAsync(orderStatusId);

            if (status == null)
            {
                return NotFound();
            }

            var result = new ServiceResponse<OrderStatus>
            {
                Data = status
            };

            return Ok(result);
        }
    }
}
