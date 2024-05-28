using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysionOrder.Infrastructure.Data.Migrations
{
    public partial class bt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BillTypeId",
                schema: "ElysionOrder",
                table: "Bills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BillTypes",
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
                    table.PrimaryKey("PK_BillTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillTypeId",
                schema: "ElysionOrder",
                table: "Bills",
                column: "BillTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillTypes_BillTypeId",
                schema: "ElysionOrder",
                table: "Bills",
                column: "BillTypeId",
                principalSchema: "ElysionOrder",
                principalTable: "BillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillTypes_BillTypeId",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "BillTypes",
                schema: "ElysionOrder");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BillTypeId",
                schema: "ElysionOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillTypeId",
                schema: "ElysionOrder",
                table: "Bills");
        }
    }
}
