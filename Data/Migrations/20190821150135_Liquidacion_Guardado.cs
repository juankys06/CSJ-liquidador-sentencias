using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class Liquidacion_Guardado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataLiquidacion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tipocodigo = table.Column<int>(nullable: false),
                    llaveproc = table.Column<string>(maxLength: 25, nullable: false),
                    fecha = table.Column<DateTime>(nullable: false),
                    data = table.Column<string>(nullable: false),
                    creadorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataLiquidacion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DataLiquidacion_AspNetUsers_creadorId",
                        column: x => x.creadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataLiquidacion_T927TIPOLIQUID_tipocodigo",
                        column: x => x.tipocodigo,
                        principalTable: "T927TIPOLIQUID",
                        principalColumn: "A927CODTIPO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataLiquidacion_creadorId",
                table: "DataLiquidacion",
                column: "creadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DataLiquidacion_tipocodigo",
                table: "DataLiquidacion",
                column: "tipocodigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataLiquidacion");
        }
    }
}
