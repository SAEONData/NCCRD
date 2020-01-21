using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class AddedProjectDAOstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedDAOGoalId",
                table: "Project");

            migrationBuilder.CreateTable(
                name: "ProjectDAOs",
                columns: table => new
                {
                    ProjectDAOId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DAOId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDAOs", x => x.ProjectDAOId);
                    table.ForeignKey(
                        name: "FK_ProjectDAOs_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDAOs_ProjectId",
                table: "ProjectDAOs",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDAOs");

            migrationBuilder.AddColumn<Guid>(
                name: "LinkedDAOGoalId",
                table: "Project",
                nullable: true);
        }
    }
}
