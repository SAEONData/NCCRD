using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("Typology")]
    [EnableCors("CORSPolicy")]
    public class TypologyController : ODataController
    {
        public SQLDBContext _context { get; }
        private UserManager<TypologyController> _userManager;
        public TypologyController(SQLDBContext context, UserManager<TypologyController> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Get a list of Typology
        /// </summary>
        /// <returns>List of Typology</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<Typology> Get()
        {
            return _context.Typology.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get Typology by id
        /// </summary>
        /// <param name="id">TypologyId</param>
        /// <returns>Single Typology</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public Typology Get(int id)
        {
            var Typology = _context.Typology.FirstOrDefault(x => x.TypologyId == id && x.IsDeleted == false);
            if (Typology == null)
                return new Typology();
            else
                return Typology;
        }

        /// <summary>
        /// Add/Update Typology
        /// </summary>
        /// <param name="data">A container for Typology</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]Typology data)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            bool IsAdmin = currentUser.IsInRole("Admin");
            var id = _userManager.GetUserId(User); // Get user id:

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.Typology.FirstOrDefault(x => x.TypologyId == data.TypologyId);
                if (existing == null) //ADD
                    _context.Typology.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete Typology
        /// </summary>
        /// <param name="id">TypologyId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.TypologyId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}
