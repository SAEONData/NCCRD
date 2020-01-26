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
    [ODataRoutePrefix("CDMStatus")]
    [EnableCors("CORSPolicy")]
    public class CDMStatusController : ODataController
    {
        public SQLDBContext _context { get; }
        public CDMStatusController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of CDMStatus
        /// </summary>
        /// <returns>List of CDMStatus</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CDMStatus> Get()
        {
            return _context.CDMStatus.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get CDMStatus by id
        /// </summary>
        /// <param name="id">CDMStatusId</param>
        /// <returns>Single CDMStatus</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public CDMStatus Get(int id)
        {
            var CDMStatus = _context.CDMStatus.FirstOrDefault(x => x.CDMStatusId == id && x.IsDeleted == false);
            if (CDMStatus == null)
                return new CDMStatus();
            else
                return CDMStatus;
        }

        /// <summary>
        /// Add/Update CDMStatus
        /// </summary>
        /// <param name="data">A container for CDMStatus</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]CDMStatus data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.CDMStatus.FirstOrDefault(x => x.CDMStatusId == data.CDMStatusId);
                if (existing == null) //ADD
                    _context.CDMStatus.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete CDMStatus
        /// </summary>
        /// <param name="id">CDMStatusId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.CDMStatusId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}