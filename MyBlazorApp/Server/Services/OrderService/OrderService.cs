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
                _context.Entry(order).Reference(o => o.Status).Load();
                response.Data = order;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersAsync()
        {
            var response = new ServiceResponse<List<Order>>
            {
                Data = await _context.Orders.Include(o => o.Status).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersByStatusAsync(string orderStatusUrl)
        {
            ServiceResponse<List<Order>> response = new ServiceResponse<List<Order>>();

            var status = _context.OrderStatuses.FirstOrDefault(cat => cat.Name.ToLower() == orderStatusUrl.ToLower());
            if (status == null)
            {
                response.Success = false;
                response.Message = "This category does not exist.";
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            response.Data = await _context.Orders
                                             .Where(o => o.Status == status)
                                             .ToListAsync();

            return response;
        }
    }
}
