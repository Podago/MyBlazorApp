using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagmen.Server.Migrations
{
    public partial class SeedMoreOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ImgUrl", "Note", "Number", "Price", "StatusId" },
                values: new object[] { 4, "https://cmt.com.ru/images/zakazat.jpg", "Заказ", "4", 29.99m, 3 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ImgUrl", "Note", "Number", "Price", "StatusId" },
                values: new object[] { 5, "https://umslon.ru/image/cache/catalog/stati/upakovka/kak-my-upakovyvaem-vashi-zakazy-1000x690.jpg", "Еще один заказ", "5", 29.99m, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ImgUrl", "Note", "Number", "Price", "StatusId" },
                values: new object[] { 6, "https://decorios.ru/upload/iblock/43c/43cc74ac7f933a74cbd9da71dc0b5ba5.jpg", "Новый заказ", "6", 29.99m, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
