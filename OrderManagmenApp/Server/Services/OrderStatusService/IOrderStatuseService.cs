namespace OrderManagmen.Server.Services.OrderStatusService
{
    public interface IOrderStatuseService
    {
        Task<List<OrderStatus>> GetOrderStatusesAsync();
        Task<OrderStatus> GetOrderStatuseAsync(int statusId);
    }
}
