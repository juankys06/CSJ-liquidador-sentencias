using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class AutoGuardar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "autoGuardar",
                table: "DataLiquidacion",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "autoGuardar",
                table: "DataLiquidacion");
        }
    }
}
