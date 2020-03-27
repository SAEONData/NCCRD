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
    [Table("BulkUploads")]
    public class BulkUpload
    {
        public int BulkUploadId { get; set; }

        public string Title { get; set; }
        public string Path { get; set; }
        public byte[] File { get; set; }
    }
}
