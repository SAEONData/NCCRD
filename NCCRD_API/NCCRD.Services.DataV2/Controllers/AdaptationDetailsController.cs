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
using Microsoft.EntityFrameworkCore;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.Extensions;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("AdaptationDetails")]
    [EnableCors("CORSPolicy")]
    public class AdaptationDetailsController : ODataController
    {
        public SQLDBContext _context { get; }
        public AdaptationDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of AdaptationDetail
        /// </summary>
        /// <returns>List of AdaptationDetail</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<AdaptationDetail> Get()
        {
            return _context.AdaptationDetails.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get AdaptationDetail by id
        /// </summary>
        /// <param name="id">AdaptationDetailId</param>
        /// <returns>Single AdaptationDetail</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public AdaptationDetail Get(int id)
        {
            var AdaptationDetail = _context.AdaptationDetails.FirstOrDefault(x => x.AdaptationDetailId == id && x.IsDeleted == false);
            if (AdaptationDetail == null)
                return new AdaptationDetail();
            else
                return AdaptationDetail;
        }

        /// <summary>
        /// Add/Update AdaptationDetail
        /// </summary>
        /// <param name="data">A container for AdaptationDetail</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]AdaptationDetail data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                data.LastModifiedDate = DateTime.Now;
                var existing = _context.AdaptationDetails.FirstOrDefault(x => x.AdaptationDetailId == data.AdaptationDetailId);
                if (existing == null) //ADD
                    _context.AdaptationDetails.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete AdaptationDetail
        /// </summary>
        /// <param name="id">AdaptationDetailId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.AdaptationDetailId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}