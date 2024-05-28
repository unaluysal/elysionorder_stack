using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class paymentday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDay",
                schema: "ElysionOrder",
                table: "Payments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDay",
                schema: "ElysionOrder",
                table: "Payments");
        }
    }
}
