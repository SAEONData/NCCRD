﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCRD.Database.Models
{
    [Table("Feasibility")]
    public class Feasibility
    {
        public int FeasibilityId { get; set; }

        //public ICollection<MAOption> MAOptions { get; set; }
    }
}
