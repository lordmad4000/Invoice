using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoice.Infra.Migrations
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdDocumentType");
        }
    }
}
