namespace MyBlazorApp.Server.Services.OrderStatusService
{
    public class OrderStatuseService : IOrderStatuseService
    {
        private readonly DataContext _context;

        public OrderStatuseService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<OrderStatus>> GetOrderStatuseAsync(int statusId)
        {
            var response = new ServiceResponse<OrderStatus>();
            var status = await _context.OrderStatuses.FindAsync(statusId);
            if (status == null)
            {
                response.Success = false;
                response.Message = "Order status not found.";
            }
            else
            {
                response.Data = status;
            }
            return response;
        }

        public async Task<ServiceResponse<List<OrderStatus>>> GetOrderStatusesAsync()
        {
            var response = new ServiceResponse<List<OrderStatus>>
            {
                Data = await _context.OrderStatuses.ToListAsync()
            };

            return response;
        }
    }
}
