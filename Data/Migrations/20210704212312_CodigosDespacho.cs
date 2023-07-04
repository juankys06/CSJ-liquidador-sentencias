using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class CodigosDespacho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodDespacho",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodEntidad",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodEspecialidad",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodLocalidad",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodDespacho",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodEntidad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodEspecialidad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodLocalidad",
                table: "AspNetUsers");
        }
    }
}
