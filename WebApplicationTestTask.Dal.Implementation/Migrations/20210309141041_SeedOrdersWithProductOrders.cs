using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationTestTask.Dal.Implementation.Migrations
{
    public partial class SeedOrdersWithProductOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Comment", "CustomerId", "OrderDate", "OrderStatus", "TotalCost" },
                values: new object[] { 1, "Delivery to the door", 1, new DateTime(2021, 2, 3, 10, 30, 25, 0, DateTimeKind.Unspecified), 4, 500m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Comment", "CustomerId", "OrderDate", "OrderStatus", "TotalCost" },
                values: new object[] { 2, "Delivery to the office", 2, new DateTime(2021, 2, 28, 14, 30, 59, 0, DateTimeKind.Unspecified), 2, 350m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Comment", "CustomerId", "OrderDate", "OrderStatus", "TotalCost" },
                values: new object[] { 3, "Call before arriving", 3, new DateTime(2021, 3, 1, 8, 59, 50, 0, DateTimeKind.Unspecified), 1, 210m });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "ProductSize", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 0, 5 },
                    { 1, 2, 0, 2 },
                    { 2, 2, 0, 1 },
                    { 2, 3, 0, 1 },
                    { 3, 1, 0, 3 },
                    { 3, 3, 0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
