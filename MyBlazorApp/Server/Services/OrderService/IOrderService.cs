namespace MyBlazorApp.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetOrdersAsync();
        Task<ServiceResponse<Order>> GetOrderAsync(int orderId);
        Task<ServiceResponse<List<Order>>> GetOrdersByStatusAsync(string orderStatusUrl);
    }
}
