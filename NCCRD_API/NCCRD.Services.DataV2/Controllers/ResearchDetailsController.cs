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
    [ODataRoutePrefix("ResearchDetails")]
    [EnableCors("CORSPolicy")]
    public class ResearchDetailsController : ODataController
    {
        public SQLDBContext _context { get; }
        public ResearchDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ResearchDetail
        /// </summary>
        /// <returns>List of ResearchDetail</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ResearchDetail> Get()
        {
            return _context.ResearchDetails.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get ResearchDetail by id
        /// </summary>
        /// <param name="id">ResearchDetailId</param>
        /// <returns>Single ResearchDetail</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public ResearchDetail Get(int id)
        {
            var ResearchDetail = _context.ResearchDetails.FirstOrDefault(x => x.ResearchDetailId == id && x.IsDeleted == false);
            if (ResearchDetail == null)
                return new ResearchDetail();
            else
                return ResearchDetail;
        }

        /// <summary>
        /// Add/Update ResearchDetail
        /// </summary>
        /// <param name="data">A container for ResearchDetail</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]ResearchDetail data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.ResearchDetails.FirstOrDefault(x => x.ResearchDetailId == data.ResearchDetailId);
                if (existing == null) //ADD
                    _context.ResearchDetails.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete ResearchDetail
        /// </summary>
        /// <param name="id">ResearchDetailId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.ResearchDetailId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}