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
    [Table("ProjectFunder")]
    public class ProjectFunder
    {
        public int ProjectFunderId { get; set; }

        //FK - Funder
        [Range(1, int.MaxValue, ErrorMessage = "The Funder field is required.")]
        public int FunderId { get; set; }
        [IgnoreDataMember]
        public Funder Funder { get; set; }

        //FK - Project
        [Range(1, int.MaxValue, ErrorMessage = "The Project field is required.")]
        public int ProjectId { get; set; }
        [IgnoreDataMember]
        public Project Project { get; set; }
    }
}
