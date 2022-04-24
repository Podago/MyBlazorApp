namespace MyBlazorApp.Client.Services.OrderService
{
    public interface IOrderService
    {
        List<Order> Orders { get; set; }

        Task GetOrders();
    }
}
