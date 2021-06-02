using Microsoft.EntityFrameworkCore.Migrations;

namespace MicrosoftIdentity.API.Migrations
{
    public partial class CustomRole1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DropColumn(
                name: "status",
                table: "AspNetRoles");
        }
    }
}
