using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPostings.Infra.Persistence.Migrations
{
    public partial class AddCriteriaDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriteriaAnswers_Criterias_CriteriaId",
                table: "CriteriaAnswers");

            migrationBuilder.RenameColumn(
                name: "CriteriaId",
                table: "CriteriaAnswers",
                newName: "_criteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CriteriaAnswers_CriteriaId",
                table: "CriteriaAnswers",
                newName: "IX_CriteriaAnswers__criteriaId");

            migrationBuilder.RenameColumn(
                name: "AppliedAt",
                table: "Applications",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Criterias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Criterias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CriteriaAnswers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CriteriaAnswers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Applications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Applicants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Applicants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_CriteriaAnswers_Criterias__criteriaId",
                table: "CriteriaAnswers",
                column: "_criteriaId",
                principalTable: "Criterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriteriaAnswers_Criterias__criteriaId",
                table: "CriteriaAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CriteriaAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CriteriaAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Applicants");

            migrationBuilder.RenameColumn(
                name: "_criteriaId",
                table: "CriteriaAnswers",
                newName: "CriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CriteriaAnswers__criteriaId",
                table: "CriteriaAnswers",
                newName: "IX_CriteriaAnswers_CriteriaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Applications",
                newName: "AppliedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_CriteriaAnswers_Criterias_CriteriaId",
                table: "CriteriaAnswers",
                column: "CriteriaId",
                principalTable: "Criterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
