namespace MyBlazorApp.Server.Services.OrderStatusService
{
    public class OrderStatuseService : IOrderStatuseService
    {
        private readonly DataContext _context;

        public OrderStatuseService(DataContext context)
        {
            _context = context;
        }

        public async Task<OrderStatus> GetOrderStatuseAsync(int statusId)
        {
            return await _context.OrderStatuses.FindAsync(statusId);
        }

        public async Task<List<OrderStatus>> GetOrderStatusesAsync()
        {
            return await _context.OrderStatuses.AsNoTracking().ToListAsync();
        }
    }
}
