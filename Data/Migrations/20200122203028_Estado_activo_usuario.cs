using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class Estado_activo_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "AspNetUsers");
        }
    }
}
