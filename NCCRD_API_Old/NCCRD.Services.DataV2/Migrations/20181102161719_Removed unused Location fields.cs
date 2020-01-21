using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class RemovedunusedLocationfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatDegree",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LatDirection",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LatMinutes",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LatSeconds",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LonDegree",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LonDirection",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LonMinutes",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LonSeconds",
                table: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LatDegree",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LatDirection",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LatMinutes",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LatSeconds",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LonDegree",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LonDirection",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LonMinutes",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LonSeconds",
                table: "Location",
                nullable: true);
        }
    }
}
