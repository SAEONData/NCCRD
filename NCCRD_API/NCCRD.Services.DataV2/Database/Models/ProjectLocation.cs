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
    [Table("ProjectLocation")]
    public class ProjectLocation
    {
        public int ProjectLocationId { get; set; }

        //Arbitrary DB Fields
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "System";
        [MaxLength(50)]
        public string LastModifiedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        //FK - Project
        [Range(0, int.MaxValue, ErrorMessage = "The Project field is required.")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        //FK - Location
        [Range(0, int.MaxValue, ErrorMessage = "The Location field is required.")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
