using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class soft1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PriceLists_PriceListId",
                schema: "ElysionOrder",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PriceLists",
                schema: "ElysionOrder");

            migrationBuilder.DropTable(
                name: "PriceListTypes",
                schema: "ElysionOrder");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PriceListId",
                schema: "ElysionOrder",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceListTypes",
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
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
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
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceListTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaxId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLists_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "ElysionOrder",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLists_PriceListTypes_PriceListTypeId",
                        column: x => x.PriceListTypeId,
                        principalSchema: "ElysionOrder",
                        principalTable: "PriceListTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ElysionOrder",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLists_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalSchema: "ElysionOrder",
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PriceListId",
                schema: "ElysionOrder",
                table: "Orders",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_CurrencyId",
                schema: "ElysionOrder",
                table: "PriceLists",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_PriceListTypeId",
                schema: "ElysionOrder",
                table: "PriceLists",
                column: "PriceListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_ProductId",
                schema: "ElysionOrder",
                table: "PriceLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_TaxId",
                schema: "ElysionOrder",
                table: "PriceLists",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PriceLists_PriceListId",
                schema: "ElysionOrder",
                table: "Orders",
                column: "PriceListId",
                principalSchema: "ElysionOrder",
                principalTable: "PriceLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
