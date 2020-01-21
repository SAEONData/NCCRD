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
    [ODataRoutePrefix("User")]
    [EnableCors("CORSPolicy")]
    public class UserController : ODataController
    {
        public SQLDBContext _context { get; }
        public UserController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of Person (previously named User)
        /// </summary>
        /// <returns>List of Person (previously named User)</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<Person> Get()
        {
            return _context.Person.AsQueryable();
        }
    }
}