using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplexInvoice.Infra.Migrations
{
    public partial class add_iddocumenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdDocumentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdDocumentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdDocumentType_Id",
                table: "IdDocumentType",
                column: "Id",
                unique: true);

            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.IdDocumentType (Id, Name)
                                   VALUES('25cdf776-e147-4898-92b7-a3280cbe9a34','NIE'),
                                         ('2dda9061-4e0a-4c8a-bc10-cd998bb9946f', 'CIF'),
                                         ('8b751cfd-fefc-4a6c-8733-9e4fdb68decd', 'NIF'),
                                         ('ae2e1245-885b-477f-a79d-cb273e7c2bc1', 'DNI')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdDocumentType");
        }
    }
}
