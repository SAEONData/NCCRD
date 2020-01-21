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
    [ODataRoutePrefix("CarbonCreditMarket")]
    [EnableCors("CORSPolicy")]
    public class CarbonCreditMarketController : ODataController
    {
        public SQLDBContext _context { get; }
        public CarbonCreditMarketController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of CarbonCreditMarket
        /// </summary>
        /// <returns>List of CarbonCreditMarket</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CarbonCreditMarket> Get()
        {
            return _context.CarbonCreditMarket.AsQueryable();
        }
    }
}