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

        public async Task<ServiceResponse<List<Order>>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            var q = _context.Orders.Count(o => o.Number.Contains("1"));
            Console.WriteLine(q);
            try
            {
                var response = new ServiceResponse<List<Order>>
                {
                    Data = await _context.Orders.Include(o => o.Status).ToListAsync(cancellationToken)
                };
                return response;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Operation was cancelled by user.");
                var response = new ServiceResponse<List<Order>>
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest
                };
                return response;
            }
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersByStatusAsync(string orderStatusUrl)
        {
            ServiceResponse<List<Order>> response = new ServiceResponse<List<Order>>();

            var status = await _context.OrderStatuses.FirstOrDefaultAsync(status => status.Name.ToLower() == orderStatusUrl.ToLower());
            if (status == null)
            {
                response.Success = false;
                response.Message = "This category does not exist.";
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            response.Data = await _context.Orders
                                             .Where(order => order.Status == status)
                                             .ToListAsync();

            return response;
        }
    }
}
