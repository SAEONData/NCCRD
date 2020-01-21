using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class Intitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_AdaptationPurpose_AdaptationPurposeId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_Project_ProjectId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_ProjectStatus_ProjectStatusId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_CarbonCredit_CarbonCreditId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_Project_ProjectId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_ProjectStatus_ProjectStatusId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationEmissionsData_Project_ProjectId",
                table: "MitigationEmissionsData");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Person_ProjectManagerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDAOs_Project_ProjectId",
                table: "ProjectDAOs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFunder_Funders_FunderId",
                table: "ProjectFunder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFunder_Project_ProjectId",
                table: "ProjectFunder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLocation_Location_LocationId",
                table: "ProjectLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLocation_Project_ProjectId",
                table: "ProjectLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRegion_Project_ProjectId",
                table: "ProjectRegion");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubType_ProjectType_ProjectTypeId",
                table: "ProjectSubType");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_Project_ProjectId",
                table: "ResearchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_ResearchType_ResearchTypeId",
                table: "ResearchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_TargetAudience_TargetAudienceId",
                table: "ResearchDetails");

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

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VoluntaryGoldStandard",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VoluntaryGoldStandard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VoluntaryGoldStandard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "VoluntaryGoldStandard",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "VoluntaryGoldStandard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VersionHistory",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VersionHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VersionHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "VersionHistory",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "VersionHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ValidationStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ValidationStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ValidationStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ValidationStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ValidationStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Typology",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Typology",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Typology",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Typology",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Typology",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TargetAudience",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TargetAudience",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TargetAudience",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "TargetAudience",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "TargetAudience",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ResearchType",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ResearchType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ResearchType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ResearchType",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ResearchType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ResearchMaturity",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ResearchMaturity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ResearchMaturity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ResearchMaturity",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ResearchMaturity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ResearchDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ResearchDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ResearchDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ResearchDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ResearchDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectType",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectType",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectSubType",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectSubType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectSubType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectSubType",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectSubType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectRegion",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectRegion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectRegion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectRegion",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectRegion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectLocation",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectLocation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectLocation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectLocation",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectLocation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectFunder",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectFunder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectFunder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectFunder",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectFunder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectDAOs",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectDAOs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDAOs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectDAOs",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectDAOs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Project",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Project",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Project",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Person",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Person",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Person",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MitigationEmissionsData",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MitigationEmissionsData",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MitigationEmissionsData",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MitigationEmissionsData",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "MitigationEmissionsData",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MitigationDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MitigationDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MitigationDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MitigationDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "MitigationDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Location",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Location",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Location",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Location",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Location",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FundingStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FundingStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FundingStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "FundingStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "FundingStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Funders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Funders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Funders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Funders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Funders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CDMStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CDMStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CDMStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CDMStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "CDMStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CDMMethodology",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CDMMethodology",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CDMMethodology",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CDMMethodology",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "CDMMethodology",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CarbonCreditMarket",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CarbonCreditMarket",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CarbonCreditMarket",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CarbonCreditMarket",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "CarbonCreditMarket",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CarbonCredit",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CarbonCredit",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CarbonCredit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CarbonCredit",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "CarbonCredit",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AdaptationPurpose",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AdaptationPurpose",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AdaptationPurpose",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "AdaptationPurpose",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "AdaptationPurpose",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AdaptationDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AdaptationDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AdaptationDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "AdaptationDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "AdaptationDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_AdaptationPurpose_AdaptationPurposeId",
                table: "AdaptationDetails",
                column: "AdaptationPurposeId",
                principalTable: "AdaptationPurpose",
                principalColumn: "AdaptationPurposeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_Project_ProjectId",
                table: "AdaptationDetails",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_ProjectStatus_ProjectStatusId",
                table: "AdaptationDetails",
                column: "ProjectStatusId",
                principalTable: "ProjectStatus",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_CarbonCredit_CarbonCreditId",
                table: "MitigationDetails",
                column: "CarbonCreditId",
                principalTable: "CarbonCredit",
                principalColumn: "CarbonCreditId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_Project_ProjectId",
                table: "MitigationDetails",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_ProjectStatus_ProjectStatusId",
                table: "MitigationDetails",
                column: "ProjectStatusId",
                principalTable: "ProjectStatus",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationEmissionsData_Project_ProjectId",
                table: "MitigationEmissionsData",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Person_ProjectManagerId",
                table: "Project",
                column: "ProjectManagerId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDAOs_Project_ProjectId",
                table: "ProjectDAOs",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFunder_Funders_FunderId",
                table: "ProjectFunder",
                column: "FunderId",
                principalTable: "Funders",
                principalColumn: "FunderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFunder_Project_ProjectId",
                table: "ProjectFunder",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLocation_Location_LocationId",
                table: "ProjectLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLocation_Project_ProjectId",
                table: "ProjectLocation",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRegion_Project_ProjectId",
                table: "ProjectRegion",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubType_ProjectType_ProjectTypeId",
                table: "ProjectSubType",
                column: "ProjectTypeId",
                principalTable: "ProjectType",
                principalColumn: "ProjectTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_Project_ProjectId",
                table: "ResearchDetails",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_ResearchType_ResearchTypeId",
                table: "ResearchDetails",
                column: "ResearchTypeId",
                principalTable: "ResearchType",
                principalColumn: "ResearchTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_TargetAudience_TargetAudienceId",
                table: "ResearchDetails",
                column: "TargetAudienceId",
                principalTable: "TargetAudience",
                principalColumn: "TargetAudienceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_AdaptationPurpose_AdaptationPurposeId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_Project_ProjectId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AdaptationDetails_ProjectStatus_ProjectStatusId",
                table: "AdaptationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_CarbonCredit_CarbonCreditId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_Project_ProjectId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationDetails_ProjectStatus_ProjectStatusId",
                table: "MitigationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MitigationEmissionsData_Project_ProjectId",
                table: "MitigationEmissionsData");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Person_ProjectManagerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDAOs_Project_ProjectId",
                table: "ProjectDAOs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFunder_Funders_FunderId",
                table: "ProjectFunder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFunder_Project_ProjectId",
                table: "ProjectFunder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLocation_Location_LocationId",
                table: "ProjectLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLocation_Project_ProjectId",
                table: "ProjectLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRegion_Project_ProjectId",
                table: "ProjectRegion");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubType_ProjectType_ProjectTypeId",
                table: "ProjectSubType");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_Project_ProjectId",
                table: "ResearchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_ResearchType_ResearchTypeId",
                table: "ResearchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchDetails_TargetAudience_TargetAudienceId",
                table: "ResearchDetails");

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

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VoluntaryGoldStandard");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VoluntaryGoldStandard");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VoluntaryGoldStandard");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "VoluntaryGoldStandard");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "VoluntaryGoldStandard");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VersionHistory");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VersionHistory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VersionHistory");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "VersionHistory");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "VersionHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ValidationStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ValidationStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ValidationStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ValidationStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ValidationStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Typology");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Typology");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Typology");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Typology");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Typology");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TargetAudience");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TargetAudience");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TargetAudience");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "TargetAudience");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "TargetAudience");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ResearchType");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ResearchType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ResearchType");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ResearchType");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ResearchType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ResearchMaturity");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ResearchMaturity");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ResearchMaturity");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ResearchMaturity");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ResearchMaturity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ResearchDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ResearchDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ResearchDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ResearchDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ResearchDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectType");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectType");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectType");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectSubType");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectSubType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectSubType");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectSubType");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectSubType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectRegion");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectRegion");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectRegion");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectRegion");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectRegion");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectLocation");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectLocation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectLocation");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectLocation");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectLocation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectFunder");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectFunder");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectFunder");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectFunder");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectFunder");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectDAOs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectDAOs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectDAOs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectDAOs");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectDAOs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MitigationEmissionsData");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MitigationEmissionsData");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MitigationEmissionsData");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MitigationEmissionsData");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "MitigationEmissionsData");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MitigationDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MitigationDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MitigationDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MitigationDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "MitigationDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FundingStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FundingStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FundingStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "FundingStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "FundingStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Funders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Funders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Funders");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Funders");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Funders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CDMStatus");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CDMStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CDMStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CDMStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "CDMStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CDMMethodology");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CDMMethodology");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CDMMethodology");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CDMMethodology");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "CDMMethodology");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CarbonCreditMarket");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CarbonCreditMarket");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CarbonCreditMarket");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CarbonCreditMarket");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "CarbonCreditMarket");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CarbonCredit");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CarbonCredit");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CarbonCredit");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CarbonCredit");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "CarbonCredit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AdaptationPurpose");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AdaptationPurpose");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AdaptationPurpose");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "AdaptationPurpose");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "AdaptationPurpose");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "AdaptationDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "AdaptationDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_AdaptationPurpose_AdaptationPurposeId",
                table: "AdaptationDetails",
                column: "AdaptationPurposeId",
                principalTable: "AdaptationPurpose",
                principalColumn: "AdaptationPurposeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_Project_ProjectId",
                table: "AdaptationDetails",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdaptationDetails_ProjectStatus_ProjectStatusId",
                table: "AdaptationDetails",
                column: "ProjectStatusId",
                principalTable: "ProjectStatus",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_CarbonCredit_CarbonCreditId",
                table: "MitigationDetails",
                column: "CarbonCreditId",
                principalTable: "CarbonCredit",
                principalColumn: "CarbonCreditId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_Project_ProjectId",
                table: "MitigationDetails",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationDetails_ProjectStatus_ProjectStatusId",
                table: "MitigationDetails",
                column: "ProjectStatusId",
                principalTable: "ProjectStatus",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MitigationEmissionsData_Project_ProjectId",
                table: "MitigationEmissionsData",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Person_ProjectManagerId",
                table: "Project",
                column: "ProjectManagerId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDAOs_Project_ProjectId",
                table: "ProjectDAOs",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFunder_Funders_FunderId",
                table: "ProjectFunder",
                column: "FunderId",
                principalTable: "Funders",
                principalColumn: "FunderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFunder_Project_ProjectId",
                table: "ProjectFunder",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLocation_Location_LocationId",
                table: "ProjectLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLocation_Project_ProjectId",
                table: "ProjectLocation",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRegion_Project_ProjectId",
                table: "ProjectRegion",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubType_ProjectType_ProjectTypeId",
                table: "ProjectSubType",
                column: "ProjectTypeId",
                principalTable: "ProjectType",
                principalColumn: "ProjectTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_Project_ProjectId",
                table: "ResearchDetails",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_ResearchType_ResearchTypeId",
                table: "ResearchDetails",
                column: "ResearchTypeId",
                principalTable: "ResearchType",
                principalColumn: "ResearchTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchDetails_TargetAudience_TargetAudienceId",
                table: "ResearchDetails",
                column: "TargetAudienceId",
                principalTable: "TargetAudience",
                principalColumn: "TargetAudienceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
