using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class edm2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EdmPassword",
                schema: "ElysionOrder",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "EdmUserName",
                schema: "ElysionOrder",
                table: "Companys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EdmPassword",
                schema: "ElysionOrder",
                table: "Companys",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EdmUserName",
                schema: "ElysionOrder",
                table: "Companys",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
