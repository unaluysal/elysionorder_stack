using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class bt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Saleses_SalesId",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_SalesId",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "SalesId",
                schema: "ElysionOrder",
                table: "Bills",
                newName: "InvoicerId");

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber",
                schema: "ElysionOrder",
                table: "Bills",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "BillDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "ElysionOrder",
                table: "Bills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ShipmentDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                schema: "ElysionOrder",
                table: "Bills",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalDiscount",
                schema: "ElysionOrder",
                table: "Bills",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WayBillDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WayBillNumber",
                schema: "ElysionOrder",
                table: "Bills",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BillItems",
                schema: "ElysionOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdateUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    ProdcutId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Piece = table.Column<int>(type: "integer", nullable: false),
                    TaxId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    Total = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ElysionOrder",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItems_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalSchema: "ElysionOrder",
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_ProductId",
                schema: "ElysionOrder",
                table: "BillItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_TaxId",
                schema: "ElysionOrder",
                table: "BillItems",
                column: "TaxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItems",
                schema: "ElysionOrder");

            migrationBuilder.DropColumn(
                name: "BillDate",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ShipmentDate",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "WayBillDate",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "WayBillNumber",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "InvoicerId",
                schema: "ElysionOrder",
                table: "Bills",
                newName: "SalesId");

            migrationBuilder.AlterColumn<int>(
                name: "BillNumber",
                schema: "ElysionOrder",
                table: "Bills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SalesId",
                schema: "ElysionOrder",
                table: "Bills",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Saleses_SalesId",
                schema: "ElysionOrder",
                table: "Bills",
                column: "SalesId",
                principalSchema: "ElysionOrder",
                principalTable: "Saleses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
