using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPostings.Infra.Migrations
{
    public partial class AddCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobPostings");

            migrationBuilder.AddColumn<string>(
                name: "_companyId",
                table: "JobPostings",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.UniqueConstraint("AK_Companies_ExternalId", x => x.ExternalId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings__companyId",
                table: "JobPostings",
                column: "_companyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ExternalId",
                table: "Companies",
                column: "ExternalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostings_Companies__companyId",
                table: "JobPostings",
                column: "_companyId",
                principalTable: "Companies",
                principalColumn: "ExternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_Companies__companyId",
                table: "JobPostings");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_JobPostings__companyId",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "_companyId",
                table: "JobPostings");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JobPostings",
                type: "uuid",
                nullable: true);
        }
    }
}
