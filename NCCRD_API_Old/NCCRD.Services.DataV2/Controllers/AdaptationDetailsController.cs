using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.Extensions;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("AdaptationDetails")]
    [EnableCors("CORSPolicy")]
    public class AdaptationDetailsController : ODataController
    {
        public SQLDBContext _context { get; }
        public AdaptationDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of AdaptationDetail
        /// </summary>
        /// <returns>List of AdaptationDetail</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<AdaptationDetail> Get()
        {
            return _context.AdaptationDetails.AsQueryable();
        }
    }
}