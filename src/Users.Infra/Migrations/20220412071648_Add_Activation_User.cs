using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Infra.Migrations
{
    public partial class Add_Activation_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");
        }
    }
}
