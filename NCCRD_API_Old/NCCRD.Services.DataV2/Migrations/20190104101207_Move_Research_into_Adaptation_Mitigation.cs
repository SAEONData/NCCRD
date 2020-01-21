using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class Move_Research_into_Adaptation_Mitigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeasibilityId",
                table: "ResearchDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResearchDetailId",
                table: "MitigationDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsResearch",
                table: "AdaptationDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResearchDetailId",
                table: "AdaptationDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feasibility",
                columns: table => new
                {
                    FeasibilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feasibility", x => x.FeasibilityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchDetails_FeasibilityId",
                table: "ResearchDetails",
                column: "FeasibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_ResearchDetailId",
                table: "MitigationDetails",
                column: "ResearchDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AdaptationDetails_ResearchDetailId",
                table: "AdaptationDetails",
                column: "ResearchDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_ResearchDetails_ResearchDetailId",
                table: "AdaptationDetails",
                column: "ResearchDetailId",
                principalTable: "ResearchDetails",
                principalColumn: "ResearchDetailId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_ResearchDetails_ResearchDetailId",
                table: "MitigationDetails",
                column: "ResearchDetailId",
                principalTable: "ResearchDetails",
                principalColumn: "ResearchDetailId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_Feasibility_FeasibilityId",
                table: "ResearchDetails",
                column: "FeasibilityId",
                principalTable: "Feasibility",
                principalColumn: "FeasibilityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_ResearchDetails_ResearchDetailId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_ResearchDetails_ResearchDetailId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_Feasibility_FeasibilityId",
                table: "ResearchDetails");

            migrationBuilder.DropTable(
                name: "Feasibility");

            migrationBuilder.DropIndex(
                name: "IX_ResearchDetails_FeasibilityId",
                table: "ResearchDetails");

            migrationBuilder.DropIndex(
                name: "IX_MitigationDetails_ResearchDetailId",
                table: "MitigationDetails");

            migrationBuilder.DropIndex(
                name: "IX_AdaptationDetails_ResearchDetailId",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "FeasibilityId",
                table: "ResearchDetails");

            migrationBuilder.DropColumn(
                name: "ResearchDetailId",
                table: "MitigationDetails");

            migrationBuilder.DropColumn(
                name: "IsResearch",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "ResearchDetailId",
                table: "AdaptationDetails");
        }
    }
}
