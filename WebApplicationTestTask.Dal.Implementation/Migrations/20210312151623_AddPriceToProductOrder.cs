using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationTestTask.Dal.Implementation.Migrations
{
    public partial class AddPriceToProductOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderProducts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 },
                column: "Price",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 },
                column: "Price",
                value: 400m);

            migrationBuilder.UpdateData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 2 },
                column: "Price",
                value: 200m);

            migrationBuilder.UpdateData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 3 },
                column: "Price",
                value: 150m);

            migrationBuilder.UpdateData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 1 },
                column: "Price",
                value: 60m);

            migrationBuilder.UpdateData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 3 },
                column: "Price",
                value: 150m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProducts");
        }
    }
}
