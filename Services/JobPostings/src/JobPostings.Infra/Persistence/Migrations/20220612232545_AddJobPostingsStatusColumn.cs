using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPostings.Infra.Persistence.Migrations
{
    public partial class AddJobPostingsStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "JobPostings");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "JobPostings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JobPostings");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "JobPostings",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
