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
    [ODataRoutePrefix("ValidationStatus")]
    [EnableCors("CORSPolicy")]
    public class ValidationStatusController : ODataController
    {
        public SQLDBContext _context { get; }
        public ValidationStatusController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ValidationStatus
        /// </summary>
        /// <returns>List of ValidationStatus</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ValidationStatus> Get()
        {
            return _context.ValidationStatus.AsQueryable();
        }
    }
}