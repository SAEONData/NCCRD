using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class RemovedAdaptationDetailIsResearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsResearch",
                table: "AdaptationDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsResearch",
                table: "AdaptationDetails",
                nullable: false,
                defaultValue: false);
        }
    }
}
