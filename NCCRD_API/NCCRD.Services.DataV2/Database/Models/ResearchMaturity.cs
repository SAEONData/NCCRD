﻿using System;
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
