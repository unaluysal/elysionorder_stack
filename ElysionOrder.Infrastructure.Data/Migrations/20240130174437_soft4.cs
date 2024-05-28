using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class soft4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceListId",
                schema: "ElysionOrder",
                table: "Orders",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                schema: "ElysionOrder",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "ElysionOrder",
                table: "Orders",
                column: "ProductId",
                principalSchema: "ElysionOrder",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "ElysionOrder",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                schema: "ElysionOrder",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "ElysionOrder",
                table: "Orders",
                newName: "PriceListId");
        }
    }
}
