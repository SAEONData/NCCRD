using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class AddPolygonfilterSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new StringBuilder();

            storedProcedureCode.Append("CREATE PROCEDURE dbo.PolygonFilter" + Environment.NewLine);
            storedProcedureCode.Append("@WKT VARCHAR(MAX)" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("DECLARE @QUERY VARCHAR(MAX) = " + Environment.NewLine);
            storedProcedureCode.Append("'SELECT Project.*" + Environment.NewLine);
            storedProcedureCode.Append("FROM  Location" + Environment.NewLine);
            storedProcedureCode.Append("INNER JOIN ProjectLocation ON Location.LocationId = ProjectLocation.LocationId" + Environment.NewLine);
            storedProcedureCode.Append("INNER JOIN Project ON ProjectLocation.ProjectId = Project.ProjectId" + Environment.NewLine);
            storedProcedureCode.Append("WHERE geometry::STGeomFromText(''~WKT~'', 0).MakeValid().STIntersects(geometry::STGeomFromText(''POINT('' + CAST(Location.LonCalculated as VARCHAR(MAX)) + '' '' + CAST(Location.LatCalculated as VARCHAR(MAX)) + '')'', 0)) = 1';" + Environment.NewLine);
            storedProcedureCode.Append("SET @QUERY = REPLACE(@QUERY, '~WKT~', @WKT)" + Environment.NewLine);
            storedProcedureCode.Append("EXEC(@QUERY);" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);

            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.PolygonFilter");
        }
    }
}
