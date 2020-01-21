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
    [ODataRoutePrefix("CDMMethodology")]
    [EnableCors("CORSPolicy")]
    public class CDMMethodologyController : ODataController
    {
        public SQLDBContext _context { get; }
        public CDMMethodologyController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of CDMMethodology
        /// </summary>
        /// <returns>List of CDMMethodology</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CDMMethodology> Get()
        {
            return _context.CDMMethodology.AsQueryable();
        }
    }
}