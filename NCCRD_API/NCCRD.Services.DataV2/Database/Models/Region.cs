﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    [Table("Region")]
    public class Region
    {
        public int RegionId { get; set; }

        [Required]
        [MaxLength(450)]
        public string RegionName { get; set; }

        public string RegionDesription { get; set; }

        //FK - LocationType
        [Range(0, int.MaxValue, ErrorMessage = "The LocationType field is required.")]
        public int LocationTypeId { get; set; }
        [IgnoreDataMember]
        public LocationType LocationType { get; set; }

        //FK - ParentRegion
        [ForeignKey("ParentRegion")]
        public int? ParentRegionId { get; set; }
        [IgnoreDataMember]
        public Region ParentRegion { get; set; }
    }
}
