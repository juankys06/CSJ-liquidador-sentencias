using Microsoft.EntityFrameworkCore.Migrations;

namespace liquidador_web.Data.Migrations
{
    public partial class WebServiceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "A921IDWebService",
                table: "T921DATASAINTE",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UX_T921DATASAINTE_A921IDWebService",
                table: "T921DATASAINTE",
                column: "A921IDWebService",
                unique: true,
                filter: "[A921IDWebService] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_T921DATASAINTE_A921IDWebService",
                table: "T921DATASAINTE");

            migrationBuilder.DropColumn(
                name: "A921IDWebService",
                table: "T921DATASAINTE");
        }
    }
}
