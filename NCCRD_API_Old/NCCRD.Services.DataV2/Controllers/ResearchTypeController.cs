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
    [ODataRoutePrefix("ResearchType")]
    [EnableCors("CORSPolicy")]
    public class ResearchTypeController : ODataController
    {
        public SQLDBContext _context { get; }
        public ResearchTypeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ResearchType
        /// </summary>
        /// <returns>List of ResearchType</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ResearchType> Get()
        {
            return _context.ResearchType.AsQueryable();
        }
    }
}