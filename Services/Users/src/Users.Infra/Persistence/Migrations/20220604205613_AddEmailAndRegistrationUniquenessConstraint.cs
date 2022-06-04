using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Infra.Persistence.Migrations
{
    public partial class AddEmailAndRegistrationUniquenessConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Registration",
                table: "Users",
                column: "Registration",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Registration",
                table: "Users");
        }
    }
}
