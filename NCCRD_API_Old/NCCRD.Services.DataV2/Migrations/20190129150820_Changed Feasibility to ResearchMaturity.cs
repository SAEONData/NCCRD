using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class ChangedFeasibilitytoResearchMaturity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_Feasibility_FeasibilityId",
                table: "ResearchDetails");

            migrationBuilder.DropTable(
                name: "Feasibility");

            migrationBuilder.RenameColumn(
                name: "FeasibilityId",
                table: "ResearchDetails",
                newName: "ResearchMaturityId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearchDetails_FeasibilityId",
                table: "ResearchDetails",
                newName: "IX_ResearchDetails_ResearchMaturityId");

            migrationBuilder.CreateTable(
                name: "ResearchMaturity",
                columns: table => new
                {
                    ResearchMaturityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchMaturity", x => x.ResearchMaturityId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_ResearchMaturity_ResearchMaturityId",
                table: "ResearchDetails",
                column: "ResearchMaturityId",
                principalTable: "ResearchMaturity",
                principalColumn: "ResearchMaturityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_ResearchMaturity_ResearchMaturityId",
                table: "ResearchDetails");

            migrationBuilder.DropTable(
                name: "ResearchMaturity");

            migrationBuilder.RenameColumn(
                name: "ResearchMaturityId",
                table: "ResearchDetails",
                newName: "FeasibilityId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearchDetails_ResearchMaturityId",
                table: "ResearchDetails",
                newName: "IX_ResearchDetails_FeasibilityId");

            migrationBuilder.CreateTable(
                name: "Feasibility",
                columns: table => new
                {
                    FeasibilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feasibility", x => x.FeasibilityId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_Feasibility_FeasibilityId",
                table: "ResearchDetails",
                column: "FeasibilityId",
                principalTable: "Feasibility",
                principalColumn: "FeasibilityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
