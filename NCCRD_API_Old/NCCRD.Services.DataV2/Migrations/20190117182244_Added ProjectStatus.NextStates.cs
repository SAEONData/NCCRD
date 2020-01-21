using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class AddedProjectStatusNextStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextStates",
                table: "ProjectStatus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextStates",
                table: "ProjectStatus");
        }
    }
}
