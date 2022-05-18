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
            try
            {
                var orders = await _orderService.GetOrdersAsync(cancellationToken);

                if(orders == null)
                {
                    return NotFound();
                }

                var result = new ServiceResponse<List<Order>>
                {
                    Data = orders
                };

                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest();
            }
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<Order>>> GetOrder(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(orderId, cancellationToken);

                if (order == null)
                {
                    return NotFound();
                }

                var result = new ServiceResponse<Order>
                {
                    Data = order
                };

                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest();
            }
        }

        [HttpGet("ordersByStatus/{orderStatusUrl}/{page}")]
        public async Task<ActionResult<ServiceResponse<OrderPage>>> GetOrdersByStatus(string orderStatusUrl, CancellationToken cancellationToken, int page = 1)
        {
            try
            {
                var orderPage = await _orderService.GetOrdersByStatusPageAsync(orderStatusUrl, page, cancellationToken);

                if (orderPage == null)
                {
                    return NotFound();
                }

                var result = new ServiceResponse<OrderPage>
                {
                    Data = orderPage
                };

                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest();
            }
        }

        [HttpGet("page/{page}")]
        public async Task<ActionResult<ServiceResponse<OrderPage>>> GetOrdersByPage(CancellationToken cancellationToken, int page = 1)
        {
            try
            {
                var orderPage = await _orderService.GetOrdersByPageAsync(page, cancellationToken);

                if (orderPage == null)
                {
                    return NotFound();
                }

                var result = new ServiceResponse<OrderPage>
                {
                    Data = orderPage
                };

                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<int>>> AddOrder(Order order, CancellationToken cancellationToken)
        {
            try
            {
                var newOrderId = await _orderService.AddOrderAsync(order, cancellationToken);

                if (newOrderId == null)
                {
                    return BadRequest();
                }

                var result = new ServiceResponse<int>
                {
                    Data = newOrderId,
                    Message = "Order was created."

                };

                return Created($"api/Order/{newOrderId}", result);
            }
            catch (OperationCanceledException)
            {
                return BadRequest();
            }
        }
    }
}
