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
    [Table("ProjectSubType")]
    public class ProjectSubType
    {
        public int ProjectSubTypeId { get; set; }

        [Required]
        [MaxLength(450)]
        public string Value { get; set; }

        public string Description { get; set; }

        //FK - ProjectType
        [Range(0, int.MaxValue, ErrorMessage = "The ProjectType field is required.")]
        public int ProjectTypeId { get; set; }
        [IgnoreDataMember]
        public ProjectType ProjectType { get; set; }
    }
}
