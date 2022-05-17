using MyBlazorApp.Shared.Models;

namespace MyBlazorApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    urs => urs
                        .HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId),
                    urs => urs
                        .HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId),
                    j =>
                    {
                        j.HasKey(t => new { t.UserId, t.RoleId });
                        j.ToTable("UserRoles");
                    });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    PasswordHash = new byte[64],
                    PasswordSalt = new byte[128]
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                },
                new Role
                {
                    Id = 2,
                    Name = "User"
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserId = 1,
                    RoleId = 1
                }
            );

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus
                {
                    Id = 1,
                    Name = "Pending",
                    Url = "pending"
                },
                new OrderStatus
                {
                    Id = 2,
                    Name = "Completed",
                    Url = "completed"
                },
                new OrderStatus
                {
                    Id = 3,
                    Name = "Cancelled",
                    Url = "cancelled"
                }
            );

            modelBuilder.Entity<Order>().HasData(
                 new Order
                {
                    Id = 1,
                    Number = "1",
                    Price = 9.99m,
                    Note = "Небольшой заказ",
                    ImgUrl = "https://envybox.io/blog/wp-content/uploads/2020/10/10.jpg",
                    StatusId = 1
                },
                new Order
                {
                    Id = 2,
                    Number = "2",
                    Price = 19.99m,
                    Note = "Средний заказ",
                    ImgUrl = "https://www.tstn.ru/local/client/icons/hw_cart.svg",
                    StatusId = 2
                },
                new Order
                {
                    Id = 3,
                    Number = "3",
                    Price = 29.99m,
                    Note = "Большой заказ",
                    ImgUrl = "https://cdn.the-village.ru/the-village.ru/post_image-image/2XxOa2gwgzh8kEzt0esBtw-wide.jpg",
                    StatusId = 2
                },
                new Order
                {
                    Id = 4,
                    Number = "4",
                    Price = 29.99m,
                    Note = "Заказ",
                    ImgUrl = "https://cmt.com.ru/images/zakazat.jpg",
                    StatusId = 3
                },
                new Order
                {
                    Id = 5,
                    Number = "5",
                    Price = 29.99m,
                    Note = "Еще один заказ",
                    ImgUrl = "https://umslon.ru/image/cache/catalog/stati/upakovka/kak-my-upakovyvaem-vashi-zakazy-1000x690.jpg",
                    StatusId = 1
                },
                new Order
                {
                    Id = 6,
                    Number = "6",
                    Price = 29.99m,
                    Note = "Новый заказ",
                    ImgUrl = "https://decorios.ru/upload/iblock/43c/43cc74ac7f933a74cbd9da71dc0b5ba5.jpg",
                    StatusId = 1
                }
            );
        }
    }
}
