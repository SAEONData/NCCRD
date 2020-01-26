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

        //Arbitrary DB Fields
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "System";
        [MaxLength(50)]
        public string LastModifiedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

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
