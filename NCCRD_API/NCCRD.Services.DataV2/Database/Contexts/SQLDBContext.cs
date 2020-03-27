using Microsoft.EntityFrameworkCore;
using NCCRD.Services.DataV2.Database.Models;
using System.Linq;

namespace NCCRD.Services.DataV2.Database.Contexts
{
    public class SQLDBContext : DbContext
    {
        public DbSet<AdaptationDetail> AdaptationDetails { get; set; }
        public DbSet<AdaptationPurpose> AdaptationPurpose { get; set; }
        public DbSet<CarbonCredit> CarbonCredit { get; set; }
        public DbSet<CarbonCreditMarket> CarbonCreditMarket { get; set; }
        public DbSet<CDMMethodology> CDMMethodology { get; set; }
        public DbSet<CDMStatus> CDMStatus { get; set; }
        public DbSet<Funder> Funders { get; set; }
        public DbSet<FundingStatus> FundingStatus { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<MitigationDetail> MitigationDetails { get; set; }
        public DbSet<MitigationEmissionsData> MitigationEmissionsData { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectDAO> ProjectDAOs { get; set; }
        public DbSet<ProjectFunder> ProjectFunder { get; set; }
        public DbSet<ProjectLocation> ProjectLocation { get; set; }
        public DbSet<ProjectRegion> ProjectRegion { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<ProjectSubType> ProjectSubType { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<ResearchDetail> ResearchDetails { get; set; }
        public DbSet<ResearchType> ResearchType { get; set; }
        public DbSet<TargetAudience> TargetAudience { get; set; }
        public DbSet<Typology> Typology { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<ValidationStatus> ValidationStatus { get; set; }
        public DbSet<VersionHistory> VersionHistory { get; set; }
        public DbSet<VoluntaryGoldStandard> VoluntaryGoldStandard { get; set; }
        public DbSet<VoluntaryMethodology> VoluntaryMethodology { get; set; }
        public DbSet<ResearchMaturity> ResearchMaturity { get; set; }

        public DbSet<BulkUpload> BulkUpload { get; set; }

        public SQLDBContext() : base() { }

        public SQLDBContext(DbContextOptions options) : base(options) { }
    }
}
