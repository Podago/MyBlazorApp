namespace OrderManagmen.Client.Services.OrderStatusService
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly HttpClient _http;
        public List<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();

        public OrderStatusService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetOrderStatuses()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderStatus>>>("api/OrderStatus");

            if(result != null && result.Data != null)
                OrderStatuses = result.Data;
        }

        public async Task<ServiceResponse<OrderStatus>> GetOrderStatus(int orderStatusId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderStatus>>($"api/OrderStatus/{orderStatusId}");
            return result;
        }
    }
}
