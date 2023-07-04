using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class Auditoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "A921IDTasa",
            //    table: "T921DATASAINTE",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateTime>(nullable: false),
                    usuarioId = table.Column<string>(nullable: false),
                    modulo = table.Column<string>(nullable: true),
                    evento = table.Column<string>(nullable: true),
                    logAnterior = table.Column<string>(nullable: true),
                    logActual = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Auditoria_AspNetUsers_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Liquidaciones",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    fecha = table.Column<DateTime>(nullable: false),
                    tipocodigo = table.Column<int>(nullable: false),
                    autorId = table.Column<string>(nullable: false),
                    urlFile = table.Column<string>(nullable: false),
                    nameFile = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidaciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Liquidaciones_AspNetUsers_autorId",
                        column: x => x.autorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Liquidaciones_T927TIPOLIQUID_tipocodigo",
                        column: x => x.tipocodigo,
                        principalTable: "T927TIPOLIQUID",
                        principalColumn: "A927CODTIPO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_usuarioId",
                table: "Auditoria",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidaciones_autorId",
                table: "Liquidaciones",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidaciones_tipocodigo",
                table: "Liquidaciones",
                column: "tipocodigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Liquidaciones");

            //migrationBuilder.AlterColumn<int>(
            //    name: "A921IDTasa",
            //    table: "T921DATASAINTE",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
