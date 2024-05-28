using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class paymentway : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentWayId",
                schema: "ElysionOrder",
                table: "Payments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentWays",
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentWays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentWayId",
                schema: "ElysionOrder",
                table: "Payments",
                column: "PaymentWayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentWays_PaymentWayId",
                schema: "ElysionOrder",
                table: "Payments",
                column: "PaymentWayId",
                principalSchema: "ElysionOrder",
                principalTable: "PaymentWays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentWays_PaymentWayId",
                schema: "ElysionOrder",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentWays",
                schema: "ElysionOrder");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentWayId",
                schema: "ElysionOrder",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentWayId",
                schema: "ElysionOrder",
                table: "Payments");
        }
    }
}
