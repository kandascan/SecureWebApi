using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureWebAPI.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "XRefTeamUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.Sql($"Insert into [dbo].[Role] ([RoleId], [RoleName]) values ('1','Product owner')");
            migrationBuilder.Sql($"Insert into [dbo].[Role] ([RoleId], [RoleName]) values ('2','Scrum master')");
            migrationBuilder.Sql($"Insert into [dbo].[Role] ([RoleId], [RoleName]) values ('3','Developer')");
            migrationBuilder.Sql($"Insert into [dbo].[Role] ([RoleId], [RoleName]) values ('4','Tester')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "XRefTeamUser");
        }
    }
}