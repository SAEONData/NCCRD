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
    [ODataRoutePrefix("VoluntaryGoldStandard")]
    [EnableCors("CORSPolicy")]
    public class VoluntaryGoldStandardController : ODataController
    {
        public SQLDBContext _context { get; }
        public VoluntaryGoldStandardController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of VoluntaryGoldStandard
        /// </summary>
        /// <returns>List of VoluntaryGoldStandard</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<VoluntaryGoldStandard> Get()
        {
            return _context.VoluntaryGoldStandard.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get VoluntaryGoldStandard by id
        /// </summary>
        /// <param name="id">VoluntaryGoldStandardId</param>
        /// <returns>Single VoluntaryGoldStandard</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public VoluntaryGoldStandard Get(int id)
        {
            var VoluntaryGoldStandard = _context.VoluntaryGoldStandard.FirstOrDefault(x => x.VoluntaryGoldStandardId == id && x.IsDeleted == false);
            if (VoluntaryGoldStandard == null)
                return new VoluntaryGoldStandard();
            else
                return VoluntaryGoldStandard;
        }

        /// <summary>
        /// Add/Update VoluntaryGoldStandard
        /// </summary>
        /// <param name="data">A container for VoluntaryGoldStandard</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]VoluntaryGoldStandard data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.VoluntaryGoldStandard.FirstOrDefault(x => x.VoluntaryGoldStandardId == data.VoluntaryGoldStandardId);
                if (existing == null) //ADD
                    _context.VoluntaryGoldStandard.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete VoluntaryGoldStandard
        /// </summary>
        /// <param name="id">VoluntaryGoldStandardId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.VoluntaryGoldStandardId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}