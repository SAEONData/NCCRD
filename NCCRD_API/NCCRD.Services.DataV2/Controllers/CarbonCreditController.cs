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
    [ODataRoutePrefix("CarbonCredit")]
    [EnableCors("CORSPolicy")]
    public class CarbonCreditController : ODataController
    {
        public SQLDBContext _context { get; }
        public CarbonCreditController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// A list of CarbonCredit
        /// </summary>
        /// <returns>List of CarbonCredit</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CarbonCredit> Get()
        {
            return _context.CarbonCredit.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get CarbonCredit by id
        /// </summary>
        /// <param name="id">CarbonCreditId</param>
        /// <returns>Single CarbonCredit</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public CarbonCredit Get(int id)
        {
            var CarbonCredit = _context.CarbonCredit.FirstOrDefault(x => x.CarbonCreditId == id && x.IsDeleted == false);
            if (CarbonCredit == null)
                return new CarbonCredit();
            else
                return CarbonCredit;
        }

        /// <summary>
        /// Add/Update CarbonCredit
        /// </summary>
        /// <param name="data">A container for CarbonCredit</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]CarbonCredit data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.CarbonCredit.FirstOrDefault(x => x.CarbonCreditId == data.CarbonCreditId);
                if (existing == null) //ADD
                    _context.CarbonCredit.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete CarbonCredit
        /// </summary>
        /// <param name="id">CarbonCreditId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.CarbonCreditId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}