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
    [ODataRoutePrefix("ValidationStatus")]
    [EnableCors("CORSPolicy")]
    public class ValidationStatusController : ODataController
    {
        public SQLDBContext _context { get; }
        public ValidationStatusController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of ValidationStatus
        /// </summary>
        /// <returns>List of ValidationStatus</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<ValidationStatus> Get()
        {
            return _context.ValidationStatus.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get ValidationStatus by id
        /// </summary>
        /// <param name="id">ValidationStatusId</param>
        /// <returns>Single ValidationStatus</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public ValidationStatus Get(int id)
        {
            var ValidationStatus = _context.ValidationStatus.FirstOrDefault(x => x.ValidationStatusId == id && x.IsDeleted == false);
            if (ValidationStatus == null)
                return new ValidationStatus();
            else
                return ValidationStatus;
        }

        /// <summary>
        /// Add/Update ValidationStatus
        /// </summary>
        /// <param name="data">A container for ValidationStatus</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]ValidationStatus data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data != null)
            {
                var existing = _context.ValidationStatus.FirstOrDefault(x => x.ValidationStatusId == data.ValidationStatusId);
                if (existing == null) //ADD
                    _context.ValidationStatus.Add(data);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(data);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        /// <summary>
        /// Delete ValidationStatus
        /// </summary>
        /// <param name="id">ValidationStatusId</param>
        /// <returns>Success/Fail status</returns>
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.ValidationStatusId == 0)
                return NotFound("Record doesn't exist"); //BadRequest("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}