using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class ExtendTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Task",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Task",
                newName: "Taskname");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effort",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Effort",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Task",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Taskname",
                table: "Task",
                newName: "Name");
        }
    }
}
