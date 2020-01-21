using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.Extensions;
using NCCRD.Services.DataV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("Funders")]
    [EnableCors("CORSPolicy")]
    public class FundersController : ODataController
    {
        public SQLDBContext _context { get; }
        public FundersController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of Funder
        /// </summary>
        /// <returns>List of Funder</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<Funder> Get()
        {
            return _context.Funders.AsQueryable().Where(x => x.IsDeleted == false);
        }
        /// <summary>
        /// Get Funder by id
        /// </summary>
        /// <param name="id">FunderId</param>
        /// <returns>Single Funder</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public Funder Get(int id)
        {
            var Funder = _context.Funders.FirstOrDefault(x => x.FunderId == id && x.IsDeleted == false);
            if (Funder == null)
                return new Funder();
            else
                return Funder;
        }

        /// <summary>
        /// Add/Update Funder
        /// </summary>
        /// <param name="data">A container for Funder</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]Funder data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.Funders.FirstOrDefault(x => x.FunderId == data.FunderId);
                if (existing == null) //ADD
                    _context.Funders.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete Funder
        /// </summary>
        /// <param name="id">FunderId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.FunderId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}
