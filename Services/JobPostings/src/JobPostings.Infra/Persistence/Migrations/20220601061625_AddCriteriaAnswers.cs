using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPostings.Infra.Persistence.Migrations
{
    public partial class AddCriteriaAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriteriaAnswers_Criterias__criteriaId",
                table: "CriteriaAnswers");

            migrationBuilder.RenameColumn(
                name: "_criteriaId",
                table: "CriteriaAnswers",
                newName: "CriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CriteriaAnswers__criteriaId",
                table: "CriteriaAnswers",
                newName: "IX_CriteriaAnswers_CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriteriaAnswers_Criterias_CriteriaId",
                table: "CriteriaAnswers",
                column: "CriteriaId",
                principalTable: "Criterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_CriteriaAnswers_Criterias__criteriaId",
                table: "CriteriaAnswers",
                column: "_criteriaId",
                principalTable: "Criterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
