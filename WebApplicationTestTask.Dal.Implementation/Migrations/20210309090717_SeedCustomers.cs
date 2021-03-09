using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationTestTask.Dal.Implementation.Migrations
{
    public partial class SeedCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 1, "34, Hryhorenka avenue, Kyiv", new DateTime(2021, 3, 2, 16, 10, 0, 0, DateTimeKind.Unspecified), "Danylo Kozak" },
                    { 2, "16, Polyarna street, Kyiv", new DateTime(2021, 3, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), "Maryna Hetman" },
                    { 3, "120, Peremohy avenue, Kyiv", new DateTime(2021, 3, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), "Denys Ivahnenko" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductCategory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductCategory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProductCategory",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductCategory",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductCategory",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProductCategory",
                value: 1);
        }
    }
}
