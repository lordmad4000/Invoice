using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoice.Infra.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailAddress",
                table: "User",
                column: "EmailAddress",
                unique: true);
            
            string creationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            migrationBuilder.Sql(@$"INSERT INTO invoice.User 
                                  (Id, 
                                   EmailAddress, 
                                   Password, 
                                   FirstName, 
                                   LastName,
                                   CreationDate)
                                  VALUES('88a9df1d-ca1a-43d8-857b-782e057b6985', 
                                         'admin@gmail.com', 
                                         'ddz4RPMmrctg9HEUd7xx2w==,kincfsSHNqMOF9SroDS+koqPrd4=', 
                                         'Admin', 
                                         'Admin',
                                         '{creationDate}');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
