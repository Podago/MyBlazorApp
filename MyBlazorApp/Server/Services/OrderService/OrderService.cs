namespace MyBlazorApp.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersAsync()
        {
            var response = new ServiceResponse<List<Order>>
            {
                Data = await _context.Orders.ToListAsync()
            };

            return response;
        }
    }
}
