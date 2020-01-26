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
    [Table("ResearchDetails")]
    public class ResearchDetail
    {
        [Range(0, int.MaxValue, ErrorMessage = "The ResearchDetailId field is required.")]
        public int ResearchDetailId { get; set; }

        [Required]
        [MaxLength(450)]
        public string Author { get; set; }

        [MaxLength(450)]
        public string PaperLink { get; set; } //Optional

        //Arbitrary DB Fields
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "System";
        [MaxLength(50)]
        public string LastModifiedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        //FK - ResearchType
        [Range(0, int.MaxValue, ErrorMessage = "The ResearchType field is required.")]
        public int ResearchTypeId { get; set; }
        [IgnoreDataMember]
        public ResearchType ResearchType { get; set; }

        //FK - TargetAudience
        [Range(0, int.MaxValue, ErrorMessage = "The TargetAudience field is required.")]
        public int TargetAudienceId { get; set; }
        [IgnoreDataMember]
        public TargetAudience TargetAudience { get; set; }

        //FK - Project
        [Range(0, int.MaxValue, ErrorMessage = "The Project field is required.")]
        public int ProjectId { get; set; }
        [IgnoreDataMember]
        public Project Project { get; set; }

        //FK - ResearchMaturity
        public int? ResearchMaturityId { get; set; }
        public ResearchMaturity ResearchMaturity { get; set; }
    }
}
