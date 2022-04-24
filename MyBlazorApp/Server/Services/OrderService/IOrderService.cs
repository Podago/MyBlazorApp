namespace MyBlazorApp.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetOrdersAsync();
    }
}
