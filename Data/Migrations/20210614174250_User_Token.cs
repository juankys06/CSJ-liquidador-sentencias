using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class User_Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "active",
                table: "AspNetUsers",
                newName: "Active");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "AspNetUsers",
                newName: "active");
        }
    }
}
