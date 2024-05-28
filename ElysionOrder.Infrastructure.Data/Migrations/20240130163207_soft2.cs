using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class soft2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "ElysionOrder",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "ElysionOrder",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "TaxId",
                schema: "ElysionOrder",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId",
                schema: "ElysionOrder",
                table: "Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxId",
                schema: "ElysionOrder",
                table: "Products",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Currencies_CurrencyId",
                schema: "ElysionOrder",
                table: "Products",
                column: "CurrencyId",
                principalSchema: "ElysionOrder",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Taxes_TaxId",
                schema: "ElysionOrder",
                table: "Products",
                column: "TaxId",
                principalSchema: "ElysionOrder",
                principalTable: "Taxes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Currencies_CurrencyId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Taxes_TaxId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CurrencyId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TaxId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TaxId",
                schema: "ElysionOrder",
                table: "Products");
        }
    }
}
