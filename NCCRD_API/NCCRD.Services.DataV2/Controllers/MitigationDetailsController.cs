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
    [ODataRoutePrefix("MitigationDetails")]
    [EnableCors("CORSPolicy")]
    public class MitigationDetailsController : ODataController
    {
        public SQLDBContext _context { get; }
        public MitigationDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of MitigationDetail
        /// </summary>
        /// <returns>List of MitigationDetail</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<MitigationDetail> Get()
        {
            return _context.MitigationDetails.AsQueryable().Where(x => x.IsDeleted == false);
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