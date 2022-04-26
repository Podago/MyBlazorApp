namespace MyBlazorApp.Client.Services.OrderService
{
    public interface IOrderService
    {
        event Action OrdersChanged;

        List<Order> Orders { get; set; }

        Task GetOrders(string? orderStatusUrl = null);

        Task<ServiceResponse<Order>> GetOrder(int orderId);
    }
}
