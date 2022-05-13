namespace MyBlazorApp.Shared
{
    public class OrderPage
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
