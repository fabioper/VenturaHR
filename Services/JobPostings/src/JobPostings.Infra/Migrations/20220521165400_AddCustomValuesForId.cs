using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPostings.Infra.Migrations
{
    public partial class AddCustomValuesForId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_Companies__companyId",
                table: "JobPostings");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Companies_ExternalId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ExternalId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Companies",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostings_Companies__companyId",
                table: "JobPostings",
                column: "_companyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_Companies__companyId",
                table: "JobPostings");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Companies",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Companies_ExternalId",
                table: "Companies",
                column: "ExternalId");

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
    }
}
