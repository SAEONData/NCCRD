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
    [ODataRoutePrefix("ProjectSubType")]
    [EnableCors("CORSPolicy")]
    public class ProjectSubTypeController : ODataController
    {
        public SQLDBContext _context { get; }
        public ProjectSubTypeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ProjectSubType
        /// </summary>
        /// <returns>List of ProjectSubType</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ProjectSubType> Get()
        {
            return _context.ProjectSubType.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get ProjectSubType by id
        /// </summary>
        /// <param name="id">ProjectSubTypeId</param>
        /// <returns>Single ProjectSubType</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public ProjectSubType Get(int id)
        {
            var ProjectSubType = _context.ProjectSubType.FirstOrDefault(x => x.ProjectSubTypeId == id && x.IsDeleted == false);
            if (ProjectSubType == null)
                return new ProjectSubType();
            else
                return ProjectSubType;
        }

        /// <summary>
        /// Add/Update ProjectSubType
        /// </summary>
        /// <param name="data">A container for ProjectSubType</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]ProjectSubType data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.ProjectSubType.FirstOrDefault(x => x.ProjectSubTypeId == data.ProjectSubTypeId);
                if (existing == null) //ADD
                    _context.ProjectSubType.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete ProjectSubType
        /// </summary>
        /// <param name="id">ProjectSubTypeId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.ProjectSubTypeId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}