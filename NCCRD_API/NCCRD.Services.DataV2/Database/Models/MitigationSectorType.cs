using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("MitigationSectorType")]
    public class MitigationSectorType
    {
        public int MitigationSectorTypeId { get; set; }

        [Required]
        [MaxLength(450)]
        public string Description { get; set; }

        //Arbitrary DB Fields
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "System";
        [MaxLength(50)]
        public string LastModifiedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}