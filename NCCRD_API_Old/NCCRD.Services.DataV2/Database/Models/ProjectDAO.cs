using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Models
{
    public class ProjectDAO
    {
        public int ProjectDAOId { get; set; }

        public Guid DAOId { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
