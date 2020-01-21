using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.ViewModels
{
    public class ProjectGeoJson
    {
        public string type { get; set; }
        public GeoJsonGeometry geometry { get; set; }
        public GeoJsonProperties properties { get; set; }

        public ProjectGeoJson()
        {
            type = "";
            geometry = new GeoJsonGeometry();
            properties = new GeoJsonProperties();
        }
    }

    public class GeoJsonGeometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }

        public GeoJsonGeometry()
        {
            coordinates = new List<double>();
        }
    }

    public class GeoJsonProperties
    {
        public string id { get; set; }
        public string name { get; set; }

        public GeoJsonProperties()
        {
            id = "";
            name = "";
        }
    }
}
