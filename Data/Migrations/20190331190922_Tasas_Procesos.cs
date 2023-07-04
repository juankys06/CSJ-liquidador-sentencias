using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class Tasas_Procesos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "T052BAPROCGENE",
            //    columns: table => new
            //    {
            //        A052CODIPROC = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A052DESCPROC = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T052BAPROCGENE", x => x.A052CODIPROC);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T053BACLASGENE",
            //    columns: table => new
            //    {
            //        A053CODICLAS = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A053DESCCLAS = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T053BACLASGENE", x => x.A053CODICLAS);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T071BASUBCGENE",
            //    columns: table => new
            //    {
            //        A071CODISUBC = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A071DESCSUBC = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T071BASUBCGENE", x => x.A071CODISUBC);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T112DRSUJEPROC",
            //    columns: table => new
            //    {
            //        A112LLAVPROC = table.Column<string>(unicode: false, maxLength: 23, nullable: false),
            //        A112CODISUJE = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A112NUMESUJE = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
            //        A112NOMBSUJE = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        A112CODIDOCU = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112CIUDSUJE = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
            //        A112NUMEPROC = table.Column<string>(unicode: false, maxLength: 21, nullable: false),
            //        A112CONSPROC = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        A112CODICARC = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A112CODICIUD = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
            //        A112DIRECCIO = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        A112TELEFONO = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        A112FLAGDETE = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112IDENREPR = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
            //        A112NOMBREPR = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
            //        A112FUNCABOG = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
            //        A112CODIENTI = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112CODICIU1 = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
            //        A112CODIESPE = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112CODINUME = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
            //        A112CARGO = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        A112CODISANC = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A112NUMEDIAS = table.Column<int>(nullable: true),
            //        A112NUMEMESE = table.Column<int>(nullable: true),
            //        A112NUMEANOS = table.Column<int>(nullable: true),
            //        A112FECHINIC = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A112FECHFINA = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A112FLAGEXCL = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112FLAGREHA = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112CODISANP = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A112NUMEDIAP = table.Column<int>(nullable: true),
            //        A112NUMEMESP = table.Column<int>(nullable: true),
            //        A112NUMEANOP = table.Column<int>(nullable: true),
            //        A112FLAGREVO = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112FLAGTERM = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A112FECHTERM = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A112OBSETERM = table.Column<string>(unicode: false, maxLength: 250, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T112DRSUJEPROC", x => new { x.A112LLAVPROC, x.A112NUMESUJE, x.A112CODISUJE });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T921DATASAINTE",
            //    columns: table => new
            //    {
            //        A921IDTasa = table.Column<int>(nullable: false),
            //        A921TipoTasa = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        A921VigenteDesde = table.Column<DateTime>(type: "datetime", nullable: false),
            //        A921VigenteHasta = table.Column<DateTime>(type: "datetime", nullable: false),
            //        A921ValorTasa = table.Column<double>(type: "float", nullable: true),
            //        A921Periodo = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        A921ResVigencia = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T921DATASAINTE", x => x.A921IDTasa);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T922TIPOSTASA",
            //    columns: table => new
            //    {
            //        A922IDTasa = table.Column<string>(nullable: false),
            //        A922NomTasa = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T922TIPOSTASA", x => x.A922IDTasa);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T926LIQUIDACIONES1",
            //    columns: table => new
            //    {
            //        A926LLAVPROC = table.Column<string>(maxLength: 23, nullable: true),
            //        A926TIPOLIQ = table.Column<string>(maxLength: 3, nullable: true),
            //        A926CONSLIQ = table.Column<int>(type: "int", nullable: false),
            //        A926FECELABORA = table.Column<string>(nullable: false),
            //        A926LIQUIDACION = table.Column<string>(type: "ntext", nullable: true),
            //        A926CODUSUARIO = table.Column<string>(maxLength: 4, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T926LIQUIDACIONES1", x => x.A926FECELABORA);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "T103DAINFOPROC",
            //    columns: table => new
            //    {
            //        A103LLAVPROC = table.Column<string>(unicode: false, maxLength: 23, nullable: false),
            //        A103NUMEPROC = table.Column<string>(unicode: false, maxLength: 21, nullable: false),
            //        A103CONSPROC = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        A103CIUDRADI = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
            //        A103ENTIRADI = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        A103ESPERADI = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        A103NUENRADI = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
            //        A103ANORADI = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A103NUMERADI = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
            //        A103FECHPROC = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103HORAPROC = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103CODIRAMA = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        A103CODIAREA = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A103CODIPROC = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A103CODICLAS = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A103CODISUBC = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A103CODIRECU = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
            //        A103CODIINST = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103FECHPRES = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103FOLIPROC = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
            //        A103CUADPROC = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
            //        A103CODINATU = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103CODIPROO = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103FECHORIG = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103CODICIUO = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
            //        A103CODIENTO = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        A103CODIESPO = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        A103CODINUMO = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
            //        A103ANOTORIG = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        A103CODIACTD = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103CODIPADD = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103FLAGCICD = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A103DESCACTD = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
            //        A103FECHINID = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103FECHFIND = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103FOLIPROD = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
            //        A103CUADPROD = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
            //        A103ANOTACTD = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
            //        A103FECHDESD = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103CODIACTS = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103CODIPADS = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103FLAGCICS = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A103DESCACTS = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
            //        A103FECHINIS = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103FECHFINS = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103FOLIPROS = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
            //        A103CUADPROS = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
            //        A103ANOTACTS = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
            //        A103FECHDESS = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103FLAGREPA = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A103FECHREPA = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103HORAREPA = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103CODIUSUA = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103FECHREGI = table.Column<DateTime>(type: "datetime", nullable: true),
            //        A103HORAREGI = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
            //        A103CODIPONE = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103NOMBPONE = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
            //        A103FLAGVIGE = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A103MAGIAPRO = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103NUMEOFIC = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
            //        A103CODIUBIC = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
            //        A103FLAGDETE = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
            //        A103CONSNORM = table.Column<int>(nullable: true),
            //        A103FLAGPROC = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_T103DAINFOPROC", x => x.A103LLAVPROC);
            //        table.ForeignKey(
            //            name: "FRK07_T103DAINFOPROC",
            //            column: x => x.A103CODICLAS,
            //            principalTable: "T053BACLASGENE",
            //            principalColumn: "A053CODICLAS",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FRK06_T103DAINFOPROC",
            //            column: x => x.A103CODIPROC,
            //            principalTable: "T052BAPROCGENE",
            //            principalColumn: "A052CODIPROC",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FRK08_T103DAINFOPROC",
            //            column: x => x.A103CODISUBC,
            //            principalTable: "T071BASUBCGENE",
            //            principalColumn: "A071CODISUBC",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_T103DAINFOPROC_A103CODIPROC",
            //    table: "T103DAINFOPROC",
            //    column: "A103CODIPROC");

            //migrationBuilder.CreateIndex(
            //    name: "IX_T103DAINFOPROC_A103CODISUBC",
            //    table: "T103DAINFOPROC",
            //    column: "A103CODISUBC");

            //migrationBuilder.CreateIndex(
            //    name: "T103DAINFOPROC9",
            //    table: "T103DAINFOPROC",
            //    column: "A103NUMEPROC");

            //migrationBuilder.CreateIndex(
            //    name: "T103DAINFOPROC7",
            //    table: "T103DAINFOPROC",
            //    columns: new[] { "A103NUMERADI", "A103NUENRADI" });

            //migrationBuilder.CreateIndex(
            //    name: "T103DAINFOPROC10",
            //    table: "T103DAINFOPROC",
            //    columns: new[] { "A103FLAGREPA", "A103CIUDRADI", "A103ENTIRADI", "A103ESPERADI", "A103NUENRADI" });

            //migrationBuilder.CreateIndex(
            //    name: "T103DAINFOPROC19",
            //    table: "T103DAINFOPROC",
            //    columns: new[] { "A103CODICLAS", "A103LLAVPROC", "A103CIUDRADI", "A103ENTIRADI", "A103ESPERADI", "A103NUENRADI", "A103ANORADI", "A103NUMERADI", "A103FLAGREPA" });

            //migrationBuilder.CreateIndex(
            //    name: "T103DAINFOPROC15",
            //    table: "T103DAINFOPROC",
            //    columns: new[] { "A103LLAVPROC", "A103CIUDRADI", "A103ENTIRADI", "A103ESPERADI", "A103NUENRADI", "A103CODICIUO", "A103CODIENTO", "A103CODIESPO", "A103FLAGREPA", "A103CONSPROC", "A103ANORADI", "A103NUMERADI", "A103CODINUMO", "A103NOMBPONE" });

            //migrationBuilder.CreateIndex(
            //    name: "T112DRSUJEPROC20",
            //    table: "T112DRSUJEPROC",
            //    column: "A112CODISUJE");

            //migrationBuilder.CreateIndex(
            //    name: "T112DRSUJEPROC12",
            //    table: "T112DRSUJEPROC",
            //    columns: new[] { "A112CODISUJE", "A112LLAVPROC", "A112NUMESUJE", "A112NOMBSUJE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "T103DAINFOPROC");

            //migrationBuilder.DropTable(
            //    name: "T112DRSUJEPROC");

            //migrationBuilder.DropTable(
            //    name: "T921DATASAINTE");

            //migrationBuilder.DropTable(
            //    name: "T922TIPOSTASA");

            //migrationBuilder.DropTable(
            //    name: "T926LIQUIDACIONES1");

            //migrationBuilder.DropTable(
            //    name: "T053BACLASGENE");

            //migrationBuilder.DropTable(
            //    name: "T052BAPROCGENE");

            //migrationBuilder.DropTable(
            //    name: "T071BASUBCGENE");
        }
    }
}
