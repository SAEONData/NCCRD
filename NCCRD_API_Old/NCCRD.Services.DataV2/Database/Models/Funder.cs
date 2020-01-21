using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("Funders")]
    public class Funder
    {
        public int FunderId { get; set; }

        [Required]
        public string FundingAgency { get; set; }

        public string GrantProgName { get; set; }
        public decimal? TotalBudget { get; set; }
        public decimal? AnnualBudget { get; set; }
        public string PartnerDepsOrgs { get; set; }

        //FK - ProjectCoordinator (Person)
        [ForeignKey("ProjectCoordinator")]
        public int? ProjectCoordinatorId { get; set; }
        [IgnoreDataMember]
        public Person ProjectCoordinator { get; set; }

        //FK - FundingStatus
        public int? FundingStatusId { get; set; }
        [IgnoreDataMember]
        public FundingStatus FundingStatus { get; set; }
    }
}
