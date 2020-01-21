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
    [ODataRoutePrefix("ResearchType")]
    [EnableCors("CORSPolicy")]
    public class ResearchTypeController : ODataController
    {
        public SQLDBContext _context { get; }
        public ResearchTypeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ResearchType
        /// </summary>
        /// <returns>List of ResearchType</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ResearchType> Get()
        {
            return _context.ResearchType.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get ResearchType by id
        /// </summary>
        /// <param name="id">ResearchTypeId</param>
        /// <returns>Single ResearchType</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public ResearchType Get(int id)
        {
            var ResearchType = _context.ResearchType.FirstOrDefault(x => x.ResearchTypeId == id && x.IsDeleted == false);
            if (ResearchType == null)
                return new ResearchType();
            else
                return ResearchType;
        }

        /// <summary>
        /// Add/Update ResearchType
        /// </summary>
        /// <param name="data">A container for ResearchType</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]ResearchType data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.ResearchType.FirstOrDefault(x => x.ResearchTypeId == data.ResearchTypeId);
                if (existing == null) //ADD
                    _context.ResearchType.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete ResearchType
        /// </summary>
        /// <param name="id">ResearchTypeId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.ResearchTypeId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}