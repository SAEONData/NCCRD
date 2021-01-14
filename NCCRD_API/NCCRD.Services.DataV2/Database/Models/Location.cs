using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("Location")]
    public class Location
    {
        public int LocationId { get; set; }

        //public double? LatDegree { get; set; }

        //public double? LatMinutes { get; set; }

        //public double? LatSeconds { get; set; }

        //public double? LatDirection { get; set; }

        public double? LatCalculated { get; set; }

        //public double? LonDegree { get; set; }

        //public double? LonMinutes { get; set; }

        //public double? LonSeconds { get; set; }

        //public double? LonDirection { get; set; }

        public double? LonCalculated { get; set; }

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
