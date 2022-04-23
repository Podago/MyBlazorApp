namespace MyBlazorApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                    new Order
                    {
                        ID = 1,
                        Number = "1",
                        Price = 9.99m,
                        Note = "Небольшой заказ",
                        ImgUrl = "https://envybox.io/blog/wp-content/uploads/2020/10/10.jpg"
                    },
                    new Order
                    {
                        ID = 2,
                        Number = "2",
                        Price = 19.99m,
                        Note = "Средний заказ",
                        ImgUrl = "https://personalufa.ru/upload/iblock/dd1/dd18d2b8b4b413c1845ab8f3de679710.jpg"
                    },
                    new Order
                    {
                        ID = 3,
                        Number = "3",
                        Price = 29.99m,
                        Note = "Большой заказ",
                        ImgUrl = "https://cdn.the-village.ru/the-village.ru/post_image-image/2XxOa2gwgzh8kEzt0esBtw-wide.jpg"
                    }
                );
        }

        public DbSet<Order> Orders { get; set; }
    }
}
