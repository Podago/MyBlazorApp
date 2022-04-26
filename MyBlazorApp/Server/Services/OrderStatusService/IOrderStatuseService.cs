namespace MyBlazorApp.Server.Services.OrderStatusService
{
    public interface IOrderStatuseService
    {
        Task<ServiceResponse<List<OrderStatus>>> GetOrderStatusesAsync();
        Task<ServiceResponse<OrderStatus>> GetOrderStatuseAsync(int statusId);
    }
}
