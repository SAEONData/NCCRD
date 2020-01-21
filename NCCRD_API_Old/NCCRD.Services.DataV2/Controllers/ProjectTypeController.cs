using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("ProjectType")]
    [EnableCors("CORSPolicy")]
    public class ProjectTypeController : ODataController
    {
        public SQLDBContext _context { get; }
        public ProjectTypeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ProjectType
        /// </summary>
        /// <returns>List of ProjectType</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ProjectType> Get()
        {
            return _context.ProjectType.AsQueryable();
        }
    }
}