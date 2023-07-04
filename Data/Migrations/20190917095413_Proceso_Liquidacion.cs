using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class Proceso_Liquidacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "llaveprocA103llavproc",
                table: "Liquidaciones",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Liquidaciones_llaveprocA103llavproc",
                table: "Liquidaciones",
                column: "llaveprocA103llavproc");

            migrationBuilder.AddForeignKey(
                name: "FK_Liquidaciones_T103DAINFOPROC_llaveprocA103llavproc",
                table: "Liquidaciones",
                column: "llaveprocA103llavproc",
                principalTable: "T103DAINFOPROC",
                principalColumn: "A103LLAVPROC",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liquidaciones_T103DAINFOPROC_llaveprocA103llavproc",
                table: "Liquidaciones");

            migrationBuilder.DropIndex(
                name: "IX_Liquidaciones_llaveprocA103llavproc",
                table: "Liquidaciones");

            migrationBuilder.DropColumn(
                name: "llaveprocA103llavproc",
                table: "Liquidaciones");
        }
    }
}
