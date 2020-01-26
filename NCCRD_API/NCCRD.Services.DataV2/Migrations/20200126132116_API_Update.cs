using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class API_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VoluntaryMethodology",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VoluntaryMethodology",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VoluntaryMethodology",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "VoluntaryMethodology",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "VoluntaryMethodology",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VoluntaryMethodology");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VoluntaryMethodology");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VoluntaryMethodology");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "VoluntaryMethodology");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "VoluntaryMethodology");

        }
    }
}
