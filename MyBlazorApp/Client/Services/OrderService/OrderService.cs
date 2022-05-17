using MyBlazorApp.Shared.DTO;
using MyBlazorApp.Shared.Models;
using System.Net;

namespace MyBlazorApp.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; } = 0;

        public string? OrderStatusUrl { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = "Loading Orders...";

        public event Action OrdersChanged;

        public List<Order> Orders { get; set; } = new List<Order>();

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Order>> GetOrder(int orderId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<Order>>($"api/Order/{orderId}");

                return result;
            }
            catch(HttpRequestException e)
            {
                return new ServiceResponse<Order>
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public async Task GetOrders(int page, string? orderStatusUrl = null)
        {
            try
            {
                var result = orderStatusUrl == null ?
                    await _http.GetFromJsonAsync<ServiceResponse<OrderPage>>($"api/Order/page/{page}") :
                    await _http.GetFromJsonAsync<ServiceResponse<OrderPage>>($"api/Order/ordersByStatus/{orderStatusUrl}/{page}");

                if (result != null && result.Data != null)
                {
                    Orders = result.Data.Orders;
                    TotalPages = result.Data.TotalPages;
                }
                if (result != null)
                {
                    CurrentPage = page;
                    OrderStatusUrl = orderStatusUrl;
                    Success = result.Success;
                    Message = result.Message;
                }

                OrdersChanged.Invoke();
            }
            catch (HttpRequestException e)
            {
                CurrentPage = page;
                OrderStatusUrl = orderStatusUrl;
                Success = false;
                Message = e.Message;

                OrdersChanged.Invoke();
            }
        }
    }
}