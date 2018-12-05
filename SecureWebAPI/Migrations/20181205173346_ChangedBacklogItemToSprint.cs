using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class ChangedBacklogItemToSprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BacklogItem",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "Sprint",
                table: "Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sprint",
                table: "Task");

            migrationBuilder.AddColumn<bool>(
                name: "BacklogItem",
                table: "Task",
                nullable: false,
                defaultValue: false);
        }
    }
}
