using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class AddTeamIdToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Task");
        }
    }
}
