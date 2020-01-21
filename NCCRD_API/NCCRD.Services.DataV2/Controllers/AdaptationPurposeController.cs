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
    [ODataRoutePrefix("AdaptationPurpose")]
    [EnableCors("CORSPolicy")]
    public class AdaptationPurposeController : ODataController
    {
        public SQLDBContext _context { get; }
        public AdaptationPurposeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of AdaptationPurpose
        /// </summary>
        /// <returns>List of AdaptationPurpose</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<AdaptationPurpose> Get()
        {
            return _context.AdaptationPurpose.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get AdaptationPurpose by id
        /// </summary>
        /// <param name="id">AdaptationPurposeId</param>
        /// <returns>Single AdaptationPurpose</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public AdaptationPurpose Get(int id)
        {
            var AdaptationPurpose = _context.AdaptationPurpose.FirstOrDefault(x => x.AdaptationPurposeId == id && x.IsDeleted == false);
            if (AdaptationPurpose == null)
                return new AdaptationPurpose();
            else
                return AdaptationPurpose;
        }

        /// <summary>
        /// Add/Update AdaptationPurpose
        /// </summary>
        /// <param name="data">A container for AdaptationPurpose</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]AdaptationPurpose data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null) 
            {
                var existing = _context.AdaptationPurpose.FirstOrDefault(x => x.AdaptationPurposeId == data.AdaptationPurposeId);
                if (existing == null) //ADD
                    _context.AdaptationPurpose.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete AdaptationPurpose
        /// </summary>
        /// <param name="id">AdaptationPurposeId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.AdaptationPurposeId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}