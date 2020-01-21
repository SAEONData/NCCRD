using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.Extensions;
using NCCRD.Services.DataV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("Funders")]
    [EnableCors("CORSPolicy")]
    public class FundersController : ODataController
    {
        public SQLDBContext _context { get; }
        public FundersController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of Funder
        /// </summary>
        /// <returns>List of Funder</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<Funder> Get()
        {
            return _context.Funders.AsQueryable();
        }
    }
}
