namespace OrderManagmen.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(s => s.Status).FirstOrDefaultAsync(o => o.Id == orderId);

            return orders;
        }

        public async Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken)
        {

            var orders = await _context.Orders.AsNoTracking().Include(o => o.Status).ToListAsync(cancellationToken);

            return orders;

        }

        public async Task<OrderPage> GetOrdersByStatusPageAsync(string orderStatusUrl, int page, CancellationToken cancellationToken, float ordersOnPage = 3f)
        {
            var status = await _context.OrderStatuses
                .AsNoTracking()
                .FirstOrDefaultAsync(status => status.Name.ToLower() == orderStatusUrl.ToLower(), cancellationToken);

            if (page == 0)
            {
                return null;
            }

            if (status == null)
            {
                return null;
            }

            var pageCount = Math.Ceiling(await _context.Orders
                                                .AsNoTracking()
                                                .Include(o => o.Status)
                                                .Where(order => order.Status == status).CountAsync(cancellationToken) / ordersOnPage);

            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Status)
                .Where(order => order.Status == status)
                .Skip((page - 1) * (int)ordersOnPage)
                .Take((int)ordersOnPage)
                .ToListAsync(cancellationToken);

            if (orders.Count == 0)
            {
                return null;
            }

            var orderPage = new OrderPage
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return orderPage;
        }

        public async Task<OrderPage> GetOrdersByPageAsync(int page, CancellationToken cancellationToken, float ordersOnPage = 3f)
        {
            if (page == 0)
            {
                return null;
            }

            var pageCount = Math.Ceiling(await _context.Orders.AsNoTracking().CountAsync(cancellationToken) / ordersOnPage);

            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Status)
                .Skip((page - 1) * (int)ordersOnPage)
                .Take((int)ordersOnPage)
                .ToListAsync(cancellationToken);

            if (orders.Count == 0)
            {
                return null;
            }

            var orderPage = new OrderPage
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return orderPage;
        }

        public async Task<int> AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
