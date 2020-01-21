using NCCRD.Services.DataV2.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.ViewModels
{
    public class Lookups
    {
        public int Id { get; set; }

        public AdaptationPurpose[] AdaptationPurpose { get; set; }
        public CarbonCredit[] CarbonCredit { get; set; }
        public CarbonCreditMarket[] CarbonCreditMarket { get; set; }
        public CDMMethodology[] CDMMethodology { get; set; }
        public CDMStatus[] CDMStatus { get; set; }
        public FundingStatus[] FundingStatus { get; set; }
        public ProjectStatus[] ProjectStatus { get; set; }
        public ProjectType[] ProjectType { get; set; }
        public ProjectSubType[] ProjectSubType { get; set; }
        public ResearchType[] ResearchType { get; set; }
        public TargetAudience[] TargetAudience { get; set; }
        public Typology[] Typology { get; set; }
        public Person[] Person { get; set; }
        public ValidationStatus[] ValidationStatus { get; set; }
        public VoluntaryGoldStandard[] VoluntaryGoldStandard { get; set; }
        public VoluntaryMethodology[] VoluntaryMethodology { get; set; }
        public ResearchMaturity[] ResearchMaturity { get; set; }
    }
}
