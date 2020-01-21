using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.ViewModels
{
    public class Filters
    {
        public string title { get; set; }
        public int status { get; set; }
        public int typology { get; set; }
        public int region { get; set; }
        public int sector { get; set; }
        public int hazard { get; set; }
        public string daoid { get; set; }
        public string favorites { get; set; }
        public string verified { get; set; }
    }
}
