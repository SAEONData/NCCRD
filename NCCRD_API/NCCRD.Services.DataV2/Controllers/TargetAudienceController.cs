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
    [ODataRoutePrefix("TargetAudience")]
    [EnableCors("CORSPolicy")]
    public class TargetAudienceController : ODataController
    {
        public SQLDBContext _context { get; }
        public TargetAudienceController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of TargetAudience
        /// </summary>
        /// <returns>List of TargetAudience</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<TargetAudience> Get()
        {
            return _context.TargetAudience.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get TargetAudience by id
        /// </summary>
        /// <param name="id">TargetAudienceId</param>
        /// <returns>Single TargetAudience</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public TargetAudience Get(int id)
        {
            var TargetAudience = _context.TargetAudience.FirstOrDefault(x => x.TargetAudienceId == id && x.IsDeleted == false);
            if (TargetAudience == null)
                return new TargetAudience();
            else
                return TargetAudience;
        }

        /// <summary>
        /// Add/Update TargetAudience
        /// </summary>
        /// <param name="data">A container for TargetAudience</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]TargetAudience data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.TargetAudience.FirstOrDefault(x => x.TargetAudienceId == data.TargetAudienceId);
                if (existing == null) //ADD
                    _context.TargetAudience.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete TargetAudience
        /// </summary>
        /// <param name="id">TargetAudienceId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.TargetAudienceId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}