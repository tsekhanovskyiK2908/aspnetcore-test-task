using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationTestTask.Dal.Implementation.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvaliableQuantity", "CreationDate", "Description", "Name", "Price", "ProductCategory" },
                values: new object[] { 1, 10, new DateTime(2021, 2, 20, 15, 10, 0, 0, DateTimeKind.Unspecified), "Delicious", "Avocado", 20m, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvaliableQuantity", "CreationDate", "Description", "Name", "Price", "ProductCategory" },
                values: new object[] { 2, 30, new DateTime(2021, 2, 20, 15, 10, 0, 0, DateTimeKind.Unspecified), "Really Delicious", "Steak", 200m, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvaliableQuantity", "CreationDate", "Description", "Name", "Price", "ProductCategory" },
                values: new object[] { 3, 5, new DateTime(2021, 2, 20, 15, 10, 0, 0, DateTimeKind.Unspecified), "Looks good", "T-shirt", 150m, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
