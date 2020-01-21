using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("ResearchMaturity")]
    public class ResearchMaturity
    {
        public int ResearchMaturityId { get; set; }

        [Required]
        public string Value { get; set; }

        public string Description { get; set; }

    }
}
