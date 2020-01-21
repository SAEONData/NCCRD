using NCCRD.Services.DataV2.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.ViewModels
{
    public class ProjectDetails
    {
        //ID
        public int Id { get; set; }

        //Project
        public Project Project { get; set; }

        //Project details
        public Funder[] Funders { get; set; }
        public AdaptationDetail[] AdaptationDetails { get; set; }
        public MitigationDetail[] MitigationDetails { get; set; }
        public MitigationEmissionsData[] MitigationEmissionsData { get; set; }
        public ResearchDetail[] ResearchDetails { get; set; }

        //Lookup data
        public Lookups Lookups { get; set; }
    }
}
