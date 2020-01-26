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
    [Table("MitigationEmissionsData")]
    public class MitigationEmissionsData
    {
        [Range(0, int.MaxValue, ErrorMessage = "The MitigationEmissionsDataId field is required.")]
        public int MitigationEmissionsDataId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The Year field is required.")]
        public int Year { get; set; }

        public double? CO2 { get; set; }

        public double? CH4 { get; set; }

        public double? CH4_CO2e { get; set; }

        public double? N2O { get; set; }

        public double? N2O_CO2e { get; set; }

        public double? HFC { get; set; }

        public double? HFC_CO2e { get; set; }

        public double? PFC { get; set; }

        public double? PFC_CO2e { get; set; }

        public double? SF6 { get; set; }

        public double? SF6_CO2e { get; set; }

        public double? Hydro { get; set; }

        public double? Hydro_CO2e { get; set; }

        public double? Tidal { get; set; }

        public double? Tidal_CO2e { get; set; }

        public double? Wind { get; set; }

        public double? Wind_CO2e { get; set; }

        public double? Solar { get; set; }

        public double? Solar_CO2e { get; set; }

        public double? FossilFuelElecRed { get; set; }

        public double? FossilFuelElecRed_CO2e { get; set; }

        public double? BioWaste { get; set; }

        public double? BioWaste_CO2e { get; set; }

        public double? Geothermal { get; set; }

        public double? Geothermal_CO2e { get; set; }

        //Arbitrary DB Fields
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "System";
        [MaxLength(50)]
        public string LastModifiedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        //FK - Project
        [Range(0, int.MaxValue, ErrorMessage = "The Project field is required.")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
