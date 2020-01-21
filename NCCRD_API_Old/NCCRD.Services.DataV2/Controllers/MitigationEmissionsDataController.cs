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
    [ODataRoutePrefix("MitigationEmissionsData")]
    [EnableCors("CORSPolicy")]
    public class MitigationEmissionsDataController : ODataController
    {
        public SQLDBContext _context { get; }
        public MitigationEmissionsDataController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of MitigationEmissionsData
        /// </summary>
        /// <returns>List of MitigationEmissionsData</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<MitigationEmissionsData> Get()
        {
            return _context.MitigationEmissionsData.AsQueryable();
        }
    }
}