using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("VersionHistory")]
    public class VersionHistory
    {
        public int VersionHistoryId { get; set; }

        [Required]
        [MaxLength(450)]
        public string VersionNumber { get; set; }

        [Required]
        public DateTime UpdateTime { get; set; }

        public string Comments { get; set; }
    }
}
