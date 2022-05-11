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

        public async Task<ServiceResponse<Order>> GetOrderAsync(int orderId, CancellationToken cancellationToken)
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
            var response = new ServiceResponse<List<Order>>();

            try
            {
                response.Data = await _context.Orders.AsNoTracking().Include(o => o.Status).ToListAsync(cancellationToken);
                
                return response;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled by user.");

                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Operation was cancelled by user.";

                return response;
            }
        }

        public async Task<ServiceResponse<OrderPage>> GetOrdersByStatusAsync(string orderStatusUrl, int page, CancellationToken cancellationToken)
        {
            ServiceResponse<OrderPage> response = new ServiceResponse<OrderPage>();

            var status = await _context.OrderStatuses
                .AsNoTracking()
                .FirstOrDefaultAsync(status => status.Name.ToLower() == orderStatusUrl.ToLower());

            if (page == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "This page does not exist.";

                return response;
            }

            if (status == null)
            {
                response.Success = false;
                response.Message = "This category does not exist.";
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            try
            {
                var ordersOnPage = 2f;
                var pageCount = Math.Ceiling(_context.Orders
                                                .AsNoTracking()
                                                .Include(o => o.Status)
                                                .Where(order => order.Status == status).Count() / ordersOnPage);

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Include(o => o.Status)
                    .Where(order => order.Status == status)
                    .Skip((page - 1) * (int)ordersOnPage)
                    .Take((int)ordersOnPage)
                    .ToListAsync(cancellationToken);

                if (orders.Count == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "This page can not be found.";

                    return response;
                }

                response.Data = new OrderPage
                {
                    Orders = orders,
                    CurrentPage = page,
                    TotalPages = (int)pageCount
                };

                return response;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled by user.");

                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Operation was cancelled by user.";

                return response;
            }
        }

        public async Task<ServiceResponse<OrderPage>> GetOrdersByPageAsync(int page, CancellationToken cancellationToken)
        {
            ServiceResponse<OrderPage> response = new ServiceResponse<OrderPage>();

            if (page == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "This page does not exist.";

                return response;
            }

            try
            {
                var ordersOnPage = 2f;
                var pageCount = Math.Ceiling(_context.Orders.Count() / ordersOnPage);

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Include(o => o.Status)
                    .Skip((page - 1) * (int)ordersOnPage)
                    .Take((int)ordersOnPage)
                    .ToListAsync(cancellationToken);

                if (orders.Count == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "This page can not be found.";

                    return response;
                }

                response.Data = new OrderPage
                {
                    Orders = orders,
                    CurrentPage = page,
                    TotalPages = (int)pageCount
                };

                return response;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled by user.");

                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Operation was cancelled by user.";

                return response;
            }
        }

        public async Task<ServiceResponse<Order>> AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            ServiceResponse<Order> response = new ServiceResponse<Order>();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            response.Data = order;

            return response;
        }
    }
}
