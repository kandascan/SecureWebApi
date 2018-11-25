using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class AddPriorityAndEffort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Taskname",
                table: "Task",
                newName: "TaskName");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Task",
                newName: "PriorityId");

            migrationBuilder.RenameColumn(
                name: "Effort",
                table: "Task",
                newName: "EffortId");

            migrationBuilder.CreateTable(
                name: "Effort",
                columns: table => new
                {
                    EffortId = table.Column<int>(nullable: false),
                    EffortName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effort", x => x.EffortId);
                });
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('1','1')");
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('2','2')");
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('3','3')");
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('4','5')");
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('5','8')");
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('6','13')");
            migrationBuilder.Sql($"Insert into [dbo].[Effort] ([EffortId], [EffortName]) values ('7','21')");

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    PriorityId = table.Column<int>(nullable: false),
                    PriorityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.PriorityId);
                });
            migrationBuilder.Sql($"Insert into [dbo].[Priority] ([PriorityId], [PriorityName]) values ('1','High')");
            migrationBuilder.Sql($"Insert into [dbo].[Priority] ([PriorityId], [PriorityName]) values ('2','Normal')");
            migrationBuilder.Sql($"Insert into [dbo].[Priority] ([PriorityId], [PriorityName]) values ('3','Low')");
            migrationBuilder.Sql($"Insert into [dbo].[Priority] ([PriorityId], [PriorityName]) values ('4','Blocker')");
            migrationBuilder.Sql($"Insert into [dbo].[Priority] ([PriorityId], [PriorityName]) values ('5','Critical')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Effort");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "Task",
                newName: "Taskname");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "Task",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "EffortId",
                table: "Task",
                newName: "Effort");
        }
    }
}
