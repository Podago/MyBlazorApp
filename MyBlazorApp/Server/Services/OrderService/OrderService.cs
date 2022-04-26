using System.Net;

namespace MyBlazorApp.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Order>> GetOrderAsync(int orderId)
        {
            var response = new ServiceResponse<Order>();
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                response.Success = false;
                response.Message = "This order does not exist.";
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.Data = order;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersAsync()
        {
            var response = new ServiceResponse<List<Order>>
            {
                Data = await _context.Orders.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersByStatusAsync(string orderStatusUrl)
        {
            ServiceResponse<List<Order>> response = new ServiceResponse<List<Order>>();

            var checkForCategory = _context.OrderStatuses.Select(s => s.Name.ToLower()).Contains(orderStatusUrl.ToLower());
            if(checkForCategory == false)
            {
                response.Success = false;
                response.Message = "This category does not exist.";
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            response.Data = await _context.Orders
                                 .Where(o => o.Status.Url.ToLower() == orderStatusUrl.ToLower())
                                 .ToListAsync();

            return response;
        }
    }
}
