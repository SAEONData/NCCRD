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
    [ODataRoutePrefix("CDMMethodology")]
    [EnableCors("CORSPolicy")]
    public class CDMMethodologyController : ODataController
    {
        public SQLDBContext _context { get; }
        public CDMMethodologyController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of CDMMethodology
        /// </summary>
        /// <returns>List of CDMMethodology</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CDMMethodology> Get()
        {
            return _context.CDMMethodology.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get CDMMethodology by id
        /// </summary>
        /// <param name="id">CDMMethodologyId</param>
        /// <returns>Single CDMMethodology</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public CDMMethodology Get(int id)
        {
            var CDMMethodology = _context.CDMMethodology.FirstOrDefault(x => x.CDMMethodologyId == id && x.IsDeleted == false);
            if (CDMMethodology == null)
                return new CDMMethodology();
            else
                return CDMMethodology;
        }

        /// <summary>
        /// Add/Update CDMMethodology
        /// </summary>
        /// <param name="data">A container for CDMMethodology</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]CDMMethodology data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.CDMMethodology.FirstOrDefault(x => x.CDMMethodologyId == data.CDMMethodologyId);
                if (existing == null) //ADD
                    _context.CDMMethodology.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete CDMMethodology
        /// </summary>
        /// <param name="id">CDMMethodologyId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.CDMMethodologyId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}