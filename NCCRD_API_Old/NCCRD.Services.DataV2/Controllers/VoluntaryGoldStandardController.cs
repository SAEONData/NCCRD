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
    [ODataRoutePrefix("VoluntaryGoldStandard")]
    [EnableCors("CORSPolicy")]
    public class VoluntaryGoldStandardController : ODataController
    {
        public SQLDBContext _context { get; }
        public VoluntaryGoldStandardController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of VoluntaryGoldStandard
        /// </summary>
        /// <returns>List of VoluntaryGoldStandard</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<VoluntaryGoldStandard> Get()
        {
            return _context.VoluntaryGoldStandard.AsQueryable();
        }
    }
}