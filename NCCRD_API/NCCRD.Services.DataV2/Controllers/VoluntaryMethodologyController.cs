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
    [ODataRoutePrefix("VoluntaryMethodology")]
    [EnableCors("CORSPolicy")]
    public class VoluntaryMethodologyController : ODataController
    {
        public SQLDBContext _context { get; }
        public VoluntaryMethodologyController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of VoluntaryMethodology
        /// </summary>
        /// <returns>List of VoluntaryMethodology</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<VoluntaryMethodology> Get()
        {
            return _context.VoluntaryMethodology.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get VoluntaryMethodology by id
        /// </summary>
        /// <param name="id">VoluntaryMethodologyId</param>
        /// <returns>Single VoluntaryMethodology</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public VoluntaryMethodology Get(int id)
        {
            var VoluntaryMethodology = _context.VoluntaryMethodology.FirstOrDefault(x => x.VoluntaryMethodologyId == id && x.IsDeleted == false);
            if (VoluntaryMethodology == null)
                return new VoluntaryMethodology();
            else
                return VoluntaryMethodology;
        }

        /// <summary>
        /// Add/Update VoluntaryMethodology
        /// </summary>
        /// <param name="data">A container for VoluntaryMethodology</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]VoluntaryMethodology data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.VoluntaryMethodology.FirstOrDefault(x => x.VoluntaryMethodologyId == data.VoluntaryMethodologyId);
                if (existing == null) //ADD
                    _context.VoluntaryMethodology.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete VoluntaryMethodology
        /// </summary>
        /// <param name="id">VoluntaryMethodologyId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.VoluntaryMethodologyId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}