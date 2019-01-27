using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class SprintColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColumnId",
                table: "XRefSprintTask",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "XRefSprintTask",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SprintColumn",
                columns: table => new
                {
                    ColumnId = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintColumn", x => x.ColumnId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SprintColumn_ColumnName",
                table: "SprintColumn",
                column: "ColumnName",
                unique: true,
                filter: "[ColumnName] IS NOT NULL");

            migrationBuilder.Sql($"Insert into [dbo].[SprintColumn] ([ColumnId], [ColumnName]) values ('1','To do')");
            migrationBuilder.Sql($"Insert into [dbo].[SprintColumn] ([ColumnId], [ColumnName]) values ('2','In progress')");
            migrationBuilder.Sql($"Insert into [dbo].[SprintColumn] ([ColumnId], [ColumnName]) values ('3','QA')");
            migrationBuilder.Sql($"Insert into [dbo].[SprintColumn] ([ColumnId], [ColumnName]) values ('4','Done')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SprintColumn");

            migrationBuilder.DropColumn(
                name: "ColumnId",
                table: "XRefSprintTask");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "XRefSprintTask");
        }
    }
}
