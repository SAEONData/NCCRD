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
    [ODataRoutePrefix("ProjectType")]
    [EnableCors("CORSPolicy")]
    public class ProjectTypeController : ODataController
    {
        public SQLDBContext _context { get; }
        public ProjectTypeController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ProjectType
        /// </summary>
        /// <returns>List of ProjectType</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ProjectType> Get()
        {
            return _context.ProjectType.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get ProjectType by id
        /// </summary>
        /// <param name="id">ProjectTypeId</param>
        /// <returns>Single ProjectType</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public ProjectType Get(int id)
        {
            var ProjectType = _context.ProjectType.FirstOrDefault(x => x.ProjectTypeId == id && x.IsDeleted == false);
            if (ProjectType == null)
                return new ProjectType();
            else
                return ProjectType;
        }

        /// <summary>
        /// Add/Update ProjectType
        /// </summary>
        /// <param name="data">A container for ProjectType</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]ProjectType data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.ProjectType.FirstOrDefault(x => x.ProjectTypeId == data.ProjectTypeId);
                if (existing == null) //ADD
                    _context.ProjectType.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete ProjectType
        /// </summary>
        /// <param name="id">ProjectTypeId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.ProjectTypeId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}