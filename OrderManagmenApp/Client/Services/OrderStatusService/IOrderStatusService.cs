namespace OrderManagmen.Client.Services.OrderStatusService
{
    public interface IOrderStatusService
    {
        List<OrderStatus> OrderStatuses { get; set; }

        Task GetOrderStatuses();

        Task<ServiceResponse<OrderStatus>> GetOrderStatus(int orderStatusId);
    }
}
