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
    [Table("BulkUpload")]
    public class BulkUpload
    {
        public int BulkUploadId { get; set; }

        public string Title { get; set; }
        public string Path { get; set; }
        public byte[] File { get; set; }

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
