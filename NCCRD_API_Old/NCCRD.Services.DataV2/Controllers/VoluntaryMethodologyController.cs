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
    [ODataRoutePrefix("VoluntaryMethodology")]
    [EnableCors("CORSPolicy")]
    public class VoluntaryMethodologyController : ODataController
    {
        public SQLDBContext _context { get; }
        public VoluntaryMethodologyController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of VoluntaryMethodology
        /// </summary>
        /// <returns>List of VoluntaryMethodology</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<VoluntaryMethodology> Get()
        {
            return _context.VoluntaryMethodology.AsQueryable();
        }
    }
}