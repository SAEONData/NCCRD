using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class AddedTitleContactNameContactEmailtoAdaptainDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "AdaptationDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "AdaptationDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AdaptationDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AdaptationDetails");
        }
    }
}
