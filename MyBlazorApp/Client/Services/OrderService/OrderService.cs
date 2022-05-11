namespace MyBlazorApp.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; } = 0;

        public string? OrderStatusUrl { get; set; }

        public event Action OrdersChanged;

        public OrderPage OrdersOnPage { get; set; } = new OrderPage();

        public List<Order> Orders { get; set; } = new List<Order>();

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Order>> GetOrder(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Order>>($"api/Order/{orderId}");
            return result;
        }

        public async Task GetOrders(int page, string? orderStatusUrl = null)
        {
            var result = orderStatusUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<OrderPage>>($"api/Order/page/{page}") :
                await _http.GetFromJsonAsync<ServiceResponse<OrderPage>>($"api/Order/ordersByStatus/{orderStatusUrl}/{page}");

            if (result != null && result.Data != null)
            {
                OrdersOnPage = result.Data;
                Orders = result.Data.Orders;
                CurrentPage = page;
                TotalPages = result.Data.TotalPages;
                OrderStatusUrl = orderStatusUrl;
            }
            //CurrentPage = 1;
            //TotalPages = 0;

            OrdersChanged.Invoke();
        }
    }
}