using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class UniqueColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Team",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SprintName",
                table: "Sprint",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_XRefSprintTask_TaskId",
                table: "XRefSprintTask",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XRefBacklogTask_TaskId",
                table: "XRefBacklogTask",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamName",
                table: "Team",
                column: "TeamName",
                unique: true,
                filter: "[TeamName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_SprintName",
                table: "Sprint",
                column: "SprintName",
                unique: true,
                filter: "[SprintName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_XRefSprintTask_TaskId",
                table: "XRefSprintTask");

            migrationBuilder.DropIndex(
                name: "IX_XRefBacklogTask_TaskId",
                table: "XRefBacklogTask");

            migrationBuilder.DropIndex(
                name: "IX_Team_TeamName",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Sprint_SprintName",
                table: "Sprint");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Team",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SprintName",
                table: "Sprint",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
