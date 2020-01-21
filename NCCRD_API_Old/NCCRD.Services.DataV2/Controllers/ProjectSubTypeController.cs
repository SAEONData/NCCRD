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
    [ODataRoutePrefix("ProjectSubType")]
    [EnableCors("CORSPolicy")]
    public class ProjectSubTypeController : ODataController
    {
        public SQLDBContext _context { get; }
        public ProjectSubTypeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ProjectSubType
        /// </summary>
        /// <returns>List of ProjectSubType</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ProjectSubType> Get()
        {
            return _context.ProjectSubType.AsQueryable();
        }
    }
}