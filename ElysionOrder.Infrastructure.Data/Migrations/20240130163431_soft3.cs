using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class soft3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Currencies_CurrencyId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Taxes_TaxId",
                schema: "ElysionOrder",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxId",
                schema: "ElysionOrder",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyId",
                schema: "ElysionOrder",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Currencies_CurrencyId",
                schema: "ElysionOrder",
                table: "Products",
                column: "CurrencyId",
                principalSchema: "ElysionOrder",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Taxes_TaxId",
                schema: "ElysionOrder",
                table: "Products",
                column: "TaxId",
                principalSchema: "ElysionOrder",
                principalTable: "Taxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxId",
                schema: "ElysionOrder",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyId",
                schema: "ElysionOrder",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
    }
}
