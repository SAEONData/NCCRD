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
    [ODataRoutePrefix("MitigationDetails")]
    [EnableCors("CORSPolicy")]
    public class MitigationDetailsController : ODataController
    {
        public SQLDBContext _context { get; }
        public MitigationDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of MitigationDetail
        /// </summary>
        /// <returns>List of MitigationDetail</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<MitigationDetail> Get()
        {
            return _context.MitigationDetails.AsQueryable();
        }
    }
}