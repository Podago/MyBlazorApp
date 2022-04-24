namespace MyBlazorApp.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        public async Task GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Order>>>("api/order");
            if (result != null && result.Data != null)
                Orders = result.Data;
        }
    }
}
