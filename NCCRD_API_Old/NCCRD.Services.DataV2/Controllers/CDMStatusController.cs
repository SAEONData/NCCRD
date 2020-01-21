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
    [ODataRoutePrefix("CDMStatus")]
    [EnableCors("CORSPolicy")]
    public class CDMStatusController : ODataController
    {
        public SQLDBContext _context { get; }
        public CDMStatusController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of CDMStatus
        /// </summary>
        /// <returns>List of CDMStatus</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CDMStatus> Get()
        {
            return _context.CDMStatus.AsQueryable();
        }
    }
}