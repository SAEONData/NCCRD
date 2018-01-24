﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Database.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } //hashed value

        public bool? Blocked { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public string Organisation { get; set; }


        //Physical Address//

        public string PhysicalAddressLine1 { get; set; }

        public string PhysicalAddressLine2 { get; set; }

        public string PhysicalAddressLine3 { get; set; }

        public string PhysicalAddressTown { get; set; }

        public string PhysicalAddressPostalCode { get; set; }

        public string PhysicalAddressProvince { get; set; } //Lookup again Region filtered to province

        [Required]
        public string PhysicalAddressCountry { get; set; } //Lookup agains Country

        //Postal Address//

        public string PostalAddressLine1 { get; set; }

        public string PostalAddressLine2 { get; set; }

        public string PostalAddressLine3 { get; set; }

        public string PostalAddressTown { get; set; }

        public string PostalAddressPostalCode { get; set; }

        public string PostalAddressProvince {get; set; } //Lookup again Region filtered to province

        public string PostalAddressCountry { get; set; } //Lookup agains Country

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string FaxNumber { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        [Required]
        public Title Title { get; set; }

        //public ICollection<Project> Projects { get; set; }
        //public ICollection<AppLog> AppLogs { get; set; }
        //public ICollection<ChangeLog> ChangeLogs { get; set; }
    }
}
