namespace MyBlazorApp.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetOrdersAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<Order>> GetOrderAsync(int orderId, CancellationToken cancellationToken);
        Task<ServiceResponse<OrderPage>> GetOrdersByStatusAsync(string orderStatusUrl, int page, CancellationToken cancellationToken);
        Task<ServiceResponse<OrderPage>> GetOrdersByPageAsync(int page, CancellationToken cancellationToken);
        Task<ServiceResponse<Order>> AddOrderAsync(Order order, CancellationToken cancellationToken);
    }
}
