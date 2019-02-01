using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class RemoveUniqueFromTaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_XRefSprintTask_TaskId",
                table: "XRefSprintTask");

            migrationBuilder.CreateIndex(
                name: "IX_XRefSprintTask_TaskId",
                table: "XRefSprintTask",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_XRefSprintTask_TaskId",
                table: "XRefSprintTask");

            migrationBuilder.CreateIndex(
                name: "IX_XRefSprintTask_TaskId",
                table: "XRefSprintTask",
                column: "TaskId",
                unique: true);
        }
    }
}
