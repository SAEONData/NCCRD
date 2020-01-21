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
using NCCRD.Services.DataV2.Extensions;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("MitigationEmissionsData")]
    [EnableCors("CORSPolicy")]
    public class MitigationEmissionsDataController : ODataController
    {
        public SQLDBContext _context { get; }
        public MitigationEmissionsDataController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of MitigationEmissionsData
        /// </summary>
        /// <returns>List of MitigationEmissionsData</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<MitigationEmissionsData> Get()
        {
            return _context.MitigationEmissionsData.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get MitigationEmissionsData by id
        /// </summary>
        /// <param name="id">MitigationEmissionsDataId</param>
        /// <returns>Single MitigationEmissionsData</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public MitigationEmissionsData Get(int id)
        {
            var MitigationEmissionsData = _context.MitigationEmissionsData.FirstOrDefault(x => x.MitigationEmissionsDataId == id && x.IsDeleted == false);
            if (MitigationEmissionsData == null)
                return new MitigationEmissionsData();
            else
                return MitigationEmissionsData;
        }

        /// <summary>
        /// Add/Update MitigationEmissionsData
        /// </summary>
        /// <param name="data">A container for MitigationEmissionsData</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]MitigationEmissionsData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.MitigationEmissionsData.FirstOrDefault(x => x.MitigationEmissionsDataId == data.MitigationEmissionsDataId);
                if (existing == null) //ADD
                    _context.MitigationEmissionsData.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete MitigationEmissionsData
        /// </summary>
        /// <param name="id">MitigationEmissionsDataId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.MitigationEmissionsDataId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}