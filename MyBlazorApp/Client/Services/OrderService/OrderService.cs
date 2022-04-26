namespace MyBlazorApp.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public event Action OrdersChanged;

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

        public async Task GetOrders(string? orderStatusUrl = null)
        {
            var result = orderStatusUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Order>>>("api/Order") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Order>>>($"api/Order/orderStatus/{orderStatusUrl}");

            if (result != null && result.Data != null)
                Orders = result.Data;

            OrdersChanged.Invoke();
        }
    }
}
