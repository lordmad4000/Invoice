using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoice.Infra.Migrations
{
    public partial class add_tipodocumentoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoDocumentoId",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentoId", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocumentoId_Id",
                table: "TipoDocumentoId",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoDocumentoId");
        }
    }
}
