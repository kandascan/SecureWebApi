using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class RenameUserNameToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Task",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Task",
                newName: "Username");
        }
    }
}
