using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("ProjectStatus")]
    public class ProjectStatus
    {
        public int ProjectStatusId { get; set; }

        public int RefId { get; set; }

        [Required]
        [MaxLength(450)]
        public string Value { get; set; }

        public string Description { get; set; }

        public string NextStates { get; set; }

        public virtual ICollection<AdaptationDetail> AdaptationDetails { get; set; }
        public virtual ICollection<MitigationDetail> MitigationDetails { get; set; }
    }
}
