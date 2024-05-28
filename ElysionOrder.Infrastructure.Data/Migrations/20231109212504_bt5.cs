using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class bt5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                schema: "ElysionOrder",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_StoreId",
                schema: "ElysionOrder",
                table: "BillItems",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Stores_StoreId",
                schema: "ElysionOrder",
                table: "BillItems",
                column: "StoreId",
                principalSchema: "ElysionOrder",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Stores_StoreId",
                schema: "ElysionOrder",
                table: "BillItems");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_StoreId",
                schema: "ElysionOrder",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "ElysionOrder",
                table: "BillItems");
        }
    }
}
