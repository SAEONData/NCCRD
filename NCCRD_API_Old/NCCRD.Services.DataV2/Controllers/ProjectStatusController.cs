using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [EnableCors("CORSPolicy")]
    [ODataRoutePrefix("ProjectStatus")]
    public class ProjectStatusController : ODataController
    {
        public SQLDBContext _context { get; }
        public ProjectStatusController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ProjectStatus
        /// </summary>
        /// <returns>List of ProjectStatus</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ProjectStatus> Get()
        {
            return _context.ProjectStatus.AsQueryable();
        }
    }
}
