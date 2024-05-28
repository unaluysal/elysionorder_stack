using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class bt4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdcutId",
                schema: "ElysionOrder",
                table: "BillItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProdcutId",
                schema: "ElysionOrder",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
