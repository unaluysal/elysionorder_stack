using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class bt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WayBillDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShipmentDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillId",
                schema: "ElysionOrder",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                schema: "ElysionOrder",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillId",
                schema: "ElysionOrder",
                table: "BillItems",
                column: "BillId",
                principalSchema: "ElysionOrder",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillId",
                schema: "ElysionOrder",
                table: "BillItems");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_BillId",
                schema: "ElysionOrder",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "BillId",
                schema: "ElysionOrder",
                table: "BillItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WayBillDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShipmentDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillDate",
                schema: "ElysionOrder",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);
        }
    }
}
