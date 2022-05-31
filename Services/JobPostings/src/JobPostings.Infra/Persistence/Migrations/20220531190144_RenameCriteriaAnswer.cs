using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPostings.Infra.Persistence.Migrations
{
    public partial class RenameCriteriaAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaFullfillments");

            migrationBuilder.CreateTable(
                name: "CriteriaAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _criteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    _applicationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaAnswers_Applications__applicationId",
                        column: x => x._applicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriteriaAnswers_Criterias__criteriaId",
                        column: x => x._criteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaAnswers__applicationId",
                table: "CriteriaAnswers",
                column: "_applicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaAnswers__criteriaId",
                table: "CriteriaAnswers",
                column: "_criteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaAnswers");

            migrationBuilder.CreateTable(
                name: "CriteriaFullfillments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _criteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    _applicationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaFullfillments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaFullfillments_Applications__applicationId",
                        column: x => x._applicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriteriaFullfillments_Criterias__criteriaId",
                        column: x => x._criteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaFullfillments__applicationId",
                table: "CriteriaFullfillments",
                column: "_applicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaFullfillments__criteriaId",
                table: "CriteriaFullfillments",
                column: "_criteriaId");
        }
    }
}
