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
    [Table("ProjectLocation")]
    public class ProjectLocation
    {
        public int ProjectLocationId { get; set; }

        //FK - Project
        [Range(1, int.MaxValue, ErrorMessage = "The Project field is required.")]
        public int ProjectId { get; set; }
        //[IgnoreDataMember]
        public Project Project { get; set; }

        //FK - Location
        [Range(1, int.MaxValue, ErrorMessage = "The Location field is required.")]
        public int LocationId { get; set; }
        //[IgnoreDataMember]
        public Location Location { get; set; }
    }
}
