using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Api.Migrations
{
    public partial class AddExternalIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Applicants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ExternalId",
                table: "Companies",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_ExternalId",
                table: "Applicants",
                column: "ExternalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_ExternalId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_ExternalId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Applicants");
        }
    }
}
