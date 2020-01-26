using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
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
            return _context.Person.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get Person by id
        /// </summary>
        /// <param name="id">PersonId</param>
        /// <returns>Single Person</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public Person Get(int id)
        {
            var Person = _context.Person.FirstOrDefault(x => x.PersonId == id && x.IsDeleted == false);
            if (Person == null)
                return new Person();
            else
                return Person;
        }

        /// <summary>
        /// Add/Update Person
        /// </summary>
        /// <param name="data">A container for Person</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]Person data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.Person.FirstOrDefault(x => x.PersonId == data.PersonId);
                if (existing == null) //ADD
                    _context.Person.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete Person
        /// </summary>
        /// <param name="id">PersonId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.PersonId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}