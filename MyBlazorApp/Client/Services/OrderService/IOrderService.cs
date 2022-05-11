namespace MyBlazorApp.Client.Services.OrderService
{
    public interface IOrderService
    {
        event Action OrdersChanged;

        List<Order> Orders { get; set; }

        OrderPage OrdersOnPage { get; set; }

        int CurrentPage { get; set; }

        int TotalPages { get; set; }

        string? OrderStatusUrl { get; set; }

        Task GetOrders(int page, string? orderStatusUrl = null);

        Task<ServiceResponse<Order>> GetOrder(int orderId);
    }
}
