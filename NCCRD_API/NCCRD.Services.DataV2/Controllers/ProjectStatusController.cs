using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [EnableCors("CORSPolicy")]
    [ODataRoutePrefix("ProjectStatus")]
    public class ProjectStatusController : ODataController
    {
        public SQLDBContext _context { get; }
        public ProjectStatusController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ProjectStatus
        /// </summary>
        /// <returns>List of ProjectStatus</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ProjectStatus> Get()
        {
            return _context.ProjectStatus.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get ProjectStatus by id
        /// </summary>
        /// <param name="id">ProjectStatusId</param>
        /// <returns>Single ProjectStatus</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public ProjectStatus Get(int id)
        {
            var ProjectStatus = _context.ProjectStatus.FirstOrDefault(x => x.ProjectStatusId == id && x.IsDeleted == false);
            if (ProjectStatus == null)
                return new ProjectStatus();
            else
                return ProjectStatus;
        }

        /// <summary>
        /// Add/Update ProjectStatus
        /// </summary>
        /// <param name="data">A container for ProjectStatus</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]ProjectStatus data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.ProjectStatus.FirstOrDefault(x => x.ProjectStatusId == data.ProjectStatusId);
                if (existing == null) //ADD
                    _context.ProjectStatus.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete ProjectStatus
        /// </summary>
        /// <param name="id">ProjectStatusId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.ProjectStatusId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}