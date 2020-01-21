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
using NCCRD.Services.DataV2.Extensions;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("ResearchDetails")]
    [EnableCors("CORSPolicy")]
    public class ResearchDetailsController : ODataController
    {
        public SQLDBContext _context { get; }
        public ResearchDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ResearchDetail
        /// </summary>
        /// <returns>List of ResearchDetail</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ResearchDetail> Get()
        {
            return _context.ResearchDetails.AsQueryable();
        }
    }
}