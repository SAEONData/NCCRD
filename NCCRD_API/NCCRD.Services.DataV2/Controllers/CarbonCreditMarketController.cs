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
    [ODataRoutePrefix("CarbonCreditMarket")]
    [EnableCors("CORSPolicy")]
    public class CarbonCreditMarketController : ODataController
    {
        public SQLDBContext _context { get; }
        public CarbonCreditMarketController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of CarbonCreditMarket
        /// </summary>
        /// <returns>List of CarbonCreditMarket</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<CarbonCreditMarket> Get()
        {
            return _context.CarbonCreditMarket.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get CarbonCreditMarket by id
        /// </summary>
        /// <param name="id">CarbonCreditMarketId</param>
        /// <returns>Single CarbonCreditMarket</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public CarbonCreditMarket Get(int id)
        {
            var CarbonCreditMarket = _context.CarbonCreditMarket.FirstOrDefault(x => x.CarbonCreditMarketId == id && x.IsDeleted == false);
            if (CarbonCreditMarket == null)
                return new CarbonCreditMarket();
            else
                return CarbonCreditMarket;
        }

        /// <summary>
        /// Add/Update CarbonCreditMarket
        /// </summary>
        /// <param name="data">A container for CarbonCreditMarket</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]CarbonCreditMarket data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.CarbonCreditMarket.FirstOrDefault(x => x.CarbonCreditMarketId == data.CarbonCreditMarketId);
                if (existing == null) //ADD
                    _context.CarbonCreditMarket.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete CarbonCreditMarket
        /// </summary>
        /// <param name="id">CarbonCreditMarketId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.CarbonCreditMarketId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}