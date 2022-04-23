using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlazorApp.Server.Migrations
{
    public partial class OrederSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "ImgUrl", "Note", "Number", "Price" },
                values: new object[] { 1, "https://envybox.io/blog/wp-content/uploads/2020/10/10.jpg", "Небольшой заказ", "1", 9.99m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "ImgUrl", "Note", "Number", "Price" },
                values: new object[] { 2, "https://personalufa.ru/upload/iblock/dd1/dd18d2b8b4b413c1845ab8f3de679710.jpg", "Средний заказ", "2", 19.99m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "ImgUrl", "Note", "Number", "Price" },
                values: new object[] { 3, "https://cdn.the-village.ru/the-village.ru/post_image-image/2XxOa2gwgzh8kEzt0esBtw-wide.jpg", "Большой заказ", "3", 29.99m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
