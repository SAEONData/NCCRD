using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCRD.Services.DataV2.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdaptationPurpose",
                columns: table => new
                {
                    AdaptationPurposeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdaptationPurpose", x => x.AdaptationPurposeId);
                });

            migrationBuilder.CreateTable(
                name: "CarbonCredit",
                columns: table => new
                {
                    CarbonCreditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarbonCredit", x => x.CarbonCreditId);
                });

            migrationBuilder.CreateTable(
                name: "CarbonCreditMarket",
                columns: table => new
                {
                    CarbonCreditMarketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarbonCreditMarket", x => x.CarbonCreditMarketId);
                });

            migrationBuilder.CreateTable(
                name: "CDMMethodology",
                columns: table => new
                {
                    CDMMethodologyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDMMethodology", x => x.CDMMethodologyId);
                });

            migrationBuilder.CreateTable(
                name: "CDMStatus",
                columns: table => new
                {
                    CDMStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDMStatus", x => x.CDMStatusId);
                });

            migrationBuilder.CreateTable(
                name: "FundingStatus",
                columns: table => new
                {
                    FundingStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingStatus", x => x.FundingStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LatDegree = table.Column<double>(nullable: true),
                    LatMinutes = table.Column<double>(nullable: true),
                    LatSeconds = table.Column<double>(nullable: true),
                    LatDirection = table.Column<double>(nullable: true),
                    LatCalculated = table.Column<double>(nullable: true),
                    LonDegree = table.Column<double>(nullable: true),
                    LonMinutes = table.Column<double>(nullable: true),
                    LonSeconds = table.Column<double>(nullable: true),
                    LonDirection = table.Column<double>(nullable: true),
                    LonCalculated = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(maxLength: 450, nullable: false),
                    FirstName = table.Column<string>(maxLength: 450, nullable: false),
                    Surname = table.Column<string>(maxLength: 450, nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Organisation = table.Column<string>(maxLength: 450, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 450, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    ProjectStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatus", x => x.ProjectStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectType",
                columns: table => new
                {
                    ProjectTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectType", x => x.ProjectTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ResearchType",
                columns: table => new
                {
                    ResearchTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchType", x => x.ResearchTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TargetAudience",
                columns: table => new
                {
                    TargetAudienceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetAudience", x => x.TargetAudienceId);
                });

            migrationBuilder.CreateTable(
                name: "Typology",
                columns: table => new
                {
                    TypologyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typology", x => x.TypologyId);
                });

            migrationBuilder.CreateTable(
                name: "ValidationStatus",
                columns: table => new
                {
                    ValidationStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationStatus", x => x.ValidationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "VersionHistory",
                columns: table => new
                {
                    VersionHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VersionNumber = table.Column<string>(maxLength: 450, nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionHistory", x => x.VersionHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "VoluntaryGoldStandard",
                columns: table => new
                {
                    VoluntaryGoldStandardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoluntaryGoldStandard", x => x.VoluntaryGoldStandardId);
                });

            migrationBuilder.CreateTable(
                name: "VoluntaryMethodology",
                columns: table => new
                {
                    VoluntaryMethodologyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoluntaryMethodology", x => x.VoluntaryMethodologyId);
                });

            migrationBuilder.CreateTable(
                name: "Funders",
                columns: table => new
                {
                    FunderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FundingAgency = table.Column<string>(nullable: false),
                    GrantProgName = table.Column<string>(nullable: true),
                    TotalBudget = table.Column<decimal>(nullable: true),
                    AnnualBudget = table.Column<decimal>(nullable: true),
                    PartnerDepsOrgs = table.Column<string>(nullable: true),
                    ProjectCoordinatorId = table.Column<int>(nullable: true),
                    FundingStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funders", x => x.FunderId);
                    table.ForeignKey(
                        name: "FK_Funders_FundingStatus_FundingStatusId",
                        column: x => x.FundingStatusId,
                        principalTable: "FundingStatus",
                        principalColumn: "FundingStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funders_Person_ProjectCoordinatorId",
                        column: x => x.ProjectCoordinatorId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubType",
                columns: table => new
                {
                    ProjectSubTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProjectTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubType", x => x.ProjectSubTypeId);
                    table.ForeignKey(
                        name: "FK_ProjectSubType_ProjectType_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "ProjectType",
                        principalColumn: "ProjectTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectTitle = table.Column<string>(maxLength: 450, nullable: false),
                    ProjectDescription = table.Column<string>(nullable: true),
                    LeadAgent = table.Column<string>(maxLength: 450, nullable: false),
                    HostPartner = table.Column<string>(maxLength: 450, nullable: true),
                    HostOrganisation = table.Column<string>(maxLength: 450, nullable: false),
                    StartYear = table.Column<int>(nullable: false),
                    EndYear = table.Column<int>(nullable: false),
                    ReminderSent = table.Column<bool>(nullable: true),
                    AlternativeContact = table.Column<string>(maxLength: 450, nullable: true),
                    AlternativeContactEmail = table.Column<string>(maxLength: 450, nullable: true),
                    Link = table.Column<string>(maxLength: 450, nullable: true),
                    ValidationComments = table.Column<string>(nullable: true),
                    BudgetLower = table.Column<decimal>(nullable: true),
                    BudgetUpper = table.Column<decimal>(nullable: true),
                    LinkedDAOGoalId = table.Column<Guid>(nullable: true),
                    ProjectStatusId = table.Column<int>(nullable: true),
                    ProjectTypeId = table.Column<int>(nullable: false),
                    ProjectSubTypeId = table.Column<int>(nullable: true),
                    ProjectManagerId = table.Column<int>(nullable: false),
                    ValidationStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_Person_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ProjectStatus_ProjectStatusId",
                        column: x => x.ProjectStatusId,
                        principalTable: "ProjectStatus",
                        principalColumn: "ProjectStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ProjectSubType_ProjectSubTypeId",
                        column: x => x.ProjectSubTypeId,
                        principalTable: "ProjectSubType",
                        principalColumn: "ProjectSubTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ProjectType_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "ProjectType",
                        principalColumn: "ProjectTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ValidationStatus_ValidationStatusId",
                        column: x => x.ValidationStatusId,
                        principalTable: "ValidationStatus",
                        principalColumn: "ValidationStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdaptationDetails",
                columns: table => new
                {
                    AdaptationDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    AdaptationPurposeId = table.Column<int>(nullable: false),
                    HazardId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    SectorId = table.Column<int>(nullable: true),
                    ProjectStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdaptationDetails", x => x.AdaptationDetailId);
                    table.ForeignKey(
                        name: "FK_AdaptationDetails_AdaptationPurpose_AdaptationPurposeId",
                        column: x => x.AdaptationPurposeId,
                        principalTable: "AdaptationPurpose",
                        principalColumn: "AdaptationPurposeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdaptationDetails_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdaptationDetails_ProjectStatus_ProjectStatusId",
                        column: x => x.ProjectStatusId,
                        principalTable: "ProjectStatus",
                        principalColumn: "ProjectStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MitigationDetails",
                columns: table => new
                {
                    MitigationDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VCS = table.Column<int>(nullable: true),
                    Other = table.Column<int>(nullable: true),
                    OtherDescription = table.Column<string>(nullable: true),
                    CDMProjectNumber = table.Column<string>(maxLength: 450, nullable: true),
                    CarbonCreditId = table.Column<int>(nullable: false),
                    CarbonCreditMarketId = table.Column<int>(nullable: true),
                    CDMStatusId = table.Column<int>(nullable: true),
                    CDMMethodologyId = table.Column<int>(nullable: true),
                    VoluntaryMethodologyId = table.Column<int>(nullable: true),
                    VoluntaryGoldStandardId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    SectorId = table.Column<int>(nullable: true),
                    ProjectStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MitigationDetails", x => x.MitigationDetailId);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_CDMMethodology_CDMMethodologyId",
                        column: x => x.CDMMethodologyId,
                        principalTable: "CDMMethodology",
                        principalColumn: "CDMMethodologyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_CDMStatus_CDMStatusId",
                        column: x => x.CDMStatusId,
                        principalTable: "CDMStatus",
                        principalColumn: "CDMStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_CarbonCredit_CarbonCreditId",
                        column: x => x.CarbonCreditId,
                        principalTable: "CarbonCredit",
                        principalColumn: "CarbonCreditId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_CarbonCreditMarket_CarbonCreditMarketId",
                        column: x => x.CarbonCreditMarketId,
                        principalTable: "CarbonCreditMarket",
                        principalColumn: "CarbonCreditMarketId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_ProjectStatus_ProjectStatusId",
                        column: x => x.ProjectStatusId,
                        principalTable: "ProjectStatus",
                        principalColumn: "ProjectStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_VoluntaryGoldStandard_VoluntaryGoldStandardId",
                        column: x => x.VoluntaryGoldStandardId,
                        principalTable: "VoluntaryGoldStandard",
                        principalColumn: "VoluntaryGoldStandardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MitigationDetails_VoluntaryMethodology_VoluntaryMethodologyId",
                        column: x => x.VoluntaryMethodologyId,
                        principalTable: "VoluntaryMethodology",
                        principalColumn: "VoluntaryMethodologyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MitigationEmissionsData",
                columns: table => new
                {
                    MitigationEmissionsDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    CO2 = table.Column<double>(nullable: true),
                    CH4 = table.Column<double>(nullable: true),
                    CH4_CO2e = table.Column<double>(nullable: true),
                    N2O = table.Column<double>(nullable: true),
                    N2O_CO2e = table.Column<double>(nullable: true),
                    HFC = table.Column<double>(nullable: true),
                    HFC_CO2e = table.Column<double>(nullable: true),
                    PFC = table.Column<double>(nullable: true),
                    PFC_CO2e = table.Column<double>(nullable: true),
                    SF6 = table.Column<double>(nullable: true),
                    SF6_CO2e = table.Column<double>(nullable: true),
                    Hydro = table.Column<double>(nullable: true),
                    Hydro_CO2e = table.Column<double>(nullable: true),
                    Tidal = table.Column<double>(nullable: true),
                    Tidal_CO2e = table.Column<double>(nullable: true),
                    Wind = table.Column<double>(nullable: true),
                    Wind_CO2e = table.Column<double>(nullable: true),
                    Solar = table.Column<double>(nullable: true),
                    Solar_CO2e = table.Column<double>(nullable: true),
                    FossilFuelElecRed = table.Column<double>(nullable: true),
                    FossilFuelElecRed_CO2e = table.Column<double>(nullable: true),
                    BioWaste = table.Column<double>(nullable: true),
                    BioWaste_CO2e = table.Column<double>(nullable: true),
                    Geothermal = table.Column<double>(nullable: true),
                    Geothermal_CO2e = table.Column<double>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MitigationEmissionsData", x => x.MitigationEmissionsDataId);
                    table.ForeignKey(
                        name: "FK_MitigationEmissionsData_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFunder",
                columns: table => new
                {
                    ProjectFunderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FunderId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFunder", x => x.ProjectFunderId);
                    table.ForeignKey(
                        name: "FK_ProjectFunder_Funders_FunderId",
                        column: x => x.FunderId,
                        principalTable: "Funders",
                        principalColumn: "FunderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectFunder_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLocation",
                columns: table => new
                {
                    ProjectLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLocation", x => x.ProjectLocationId);
                    table.ForeignKey(
                        name: "FK_ProjectLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectLocation_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRegion",
                columns: table => new
                {
                    ProjectRegionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRegion", x => x.ProjectRegionId);
                    table.ForeignKey(
                        name: "FK_ProjectRegion_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResearchDetails",
                columns: table => new
                {
                    ResearchDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(maxLength: 450, nullable: false),
                    PaperLink = table.Column<string>(maxLength: 450, nullable: true),
                    ResearchTypeId = table.Column<int>(nullable: false),
                    TargetAudienceId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    SectorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchDetails", x => x.ResearchDetailId);
                    table.ForeignKey(
                        name: "FK_ResearchDetails_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResearchDetails_ResearchType_ResearchTypeId",
                        column: x => x.ResearchTypeId,
                        principalTable: "ResearchType",
                        principalColumn: "ResearchTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResearchDetails_TargetAudience_TargetAudienceId",
                        column: x => x.TargetAudienceId,
                        principalTable: "TargetAudience",
                        principalColumn: "TargetAudienceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdaptationDetails_AdaptationPurposeId",
                table: "AdaptationDetails",
                column: "AdaptationPurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdaptationDetails_ProjectId",
                table: "AdaptationDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AdaptationDetails_ProjectStatusId",
                table: "AdaptationDetails",
                column: "ProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Funders_FundingStatusId",
                table: "Funders",
                column: "FundingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Funders_ProjectCoordinatorId",
                table: "Funders",
                column: "ProjectCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_CDMMethodologyId",
                table: "MitigationDetails",
                column: "CDMMethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_CDMStatusId",
                table: "MitigationDetails",
                column: "CDMStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_CarbonCreditId",
                table: "MitigationDetails",
                column: "CarbonCreditId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_CarbonCreditMarketId",
                table: "MitigationDetails",
                column: "CarbonCreditMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_ProjectId",
                table: "MitigationDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_ProjectStatusId",
                table: "MitigationDetails",
                column: "ProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_VoluntaryGoldStandardId",
                table: "MitigationDetails",
                column: "VoluntaryGoldStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationDetails_VoluntaryMethodologyId",
                table: "MitigationDetails",
                column: "VoluntaryMethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_MitigationEmissionsData_ProjectId",
                table: "MitigationEmissionsData",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectManagerId",
                table: "Project",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectStatusId",
                table: "Project",
                column: "ProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectSubTypeId",
                table: "Project",
                column: "ProjectSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectTypeId",
                table: "Project",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ValidationStatusId",
                table: "Project",
                column: "ValidationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFunder_FunderId",
                table: "ProjectFunder",
                column: "FunderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFunder_ProjectId",
                table: "ProjectFunder",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLocation_LocationId",
                table: "ProjectLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLocation_ProjectId",
                table: "ProjectLocation",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRegion_ProjectId",
                table: "ProjectRegion",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubType_ProjectTypeId",
                table: "ProjectSubType",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchDetails_ProjectId",
                table: "ResearchDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchDetails_ResearchTypeId",
                table: "ResearchDetails",
                column: "ResearchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchDetails_TargetAudienceId",
                table: "ResearchDetails",
                column: "TargetAudienceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdaptationDetails");

            migrationBuilder.DropTable(
                name: "MitigationDetails");

            migrationBuilder.DropTable(
                name: "MitigationEmissionsData");

            migrationBuilder.DropTable(
                name: "ProjectFunder");

            migrationBuilder.DropTable(
                name: "ProjectLocation");

            migrationBuilder.DropTable(
                name: "ProjectRegion");

            migrationBuilder.DropTable(
                name: "ResearchDetails");

            migrationBuilder.DropTable(
                name: "Typology");

            migrationBuilder.DropTable(
                name: "VersionHistory");

            migrationBuilder.DropTable(
                name: "AdaptationPurpose");

            migrationBuilder.DropTable(
                name: "CDMMethodology");

            migrationBuilder.DropTable(
                name: "CDMStatus");

            migrationBuilder.DropTable(
                name: "CarbonCredit");

            migrationBuilder.DropTable(
                name: "CarbonCreditMarket");

            migrationBuilder.DropTable(
                name: "VoluntaryGoldStandard");

            migrationBuilder.DropTable(
                name: "VoluntaryMethodology");

            migrationBuilder.DropTable(
                name: "Funders");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ResearchType");

            migrationBuilder.DropTable(
                name: "TargetAudience");

            migrationBuilder.DropTable(
                name: "FundingStatus");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "ProjectStatus");

            migrationBuilder.DropTable(
                name: "ProjectSubType");

            migrationBuilder.DropTable(
                name: "ValidationStatus");

            migrationBuilder.DropTable(
                name: "ProjectType");
        }
    }
}
