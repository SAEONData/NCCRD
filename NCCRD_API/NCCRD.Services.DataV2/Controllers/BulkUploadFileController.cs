using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    //[ODataRoutePrefix("BulkUploadFileController")]
    [EnableCors("CORSPolicy")]
    public class BulkUploadFileController : ODataController
    {
        #region Properties

        //public int BulkUploadId { get; set; }
        //public string Title { get; set; }
        //public string Path { get; set; }
        //public byte[] File { get; set; }
        //public string CreatedBy { get; set; } = "System";
        //public string LastModifiedBy { get; set; } = "System";
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        //public bool IsDeleted { get; set; } = false;

        #endregion

        public SQLDBContext _context { get; }
        public IConfiguration _config { get; }

        public BulkUploadFileController(SQLDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("GetAllFiles")]
        public IQueryable<BulkUpload> Get()
        {
            return _context.BulkUpload.AsQueryable().Where(x => x.IsDeleted == false);
        }

        public BulkUpload Get(int id)
        {
            var bulkUploadFile = _context.BulkUpload.FirstOrDefault(x => x.BulkUploadId == id && x.IsDeleted == false);
            if (bulkUploadFile == null)
                return new BulkUpload();
            else
                return bulkUploadFile;
        }
        
        public BulkUpload Get(string fileName)
        {
            BulkUpload bulkUploadFile = _context.BulkUpload.FirstOrDefault(x => x.Title == fileName && x.IsDeleted == false);
            if (bulkUploadFile == null)
                return new BulkUpload();
            else
                return bulkUploadFile;
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("RetrieveFileById(Id={Id})")]
        public FileResult RetrieveFile(int id)
        {
            BulkUpload bulkUploadFile = _context.BulkUpload.FirstOrDefault(x => x.BulkUploadId == id && x.IsDeleted == false);
            if (bulkUploadFile == null)
                return null;
            else
                return File(bulkUploadFile.File, "application/octet-stream", bulkUploadFile.Title);
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("RetrieveFileByName(Name={Name})")]
        public ActionResult RetrieveFile(string name)
        {
            BulkUpload bulkUploadFile = _context.BulkUpload.FirstOrDefault(x => x.Title == name && x.IsDeleted == false);
            if (bulkUploadFile == null)
                return new NotFoundObjectResult(new { StatusCode = 404, Message = "404 - Not Found. The File you are seeking was not found, please check the spelling of the name and try again"});
            else
                return File(bulkUploadFile.File, "application/octet-stream", bulkUploadFile.Title);
        }

        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]BulkUpload file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (file != null)
            {
                var existing = _context.BulkUpload.FirstOrDefault(x => x.BulkUploadId == file.BulkUploadId);
                if (existing == null) //ADD
                    _context.BulkUpload.Add(file);
                else //UPDATE
                    _context.Entry(existing).CurrentValues.SetValues(file);
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        public void BulkUploadInsert(FileInfo file, string localFilePath, ref System.Text.StringBuilder errorLog)
        {
            BulkUpload bulkUploadFile = new BulkUpload();
            
            try
            {
                using (FileStream fs = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(localFilePath);
                    fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    bulkUploadFile.File = bytes;
                }
            }
            catch (Exception e) { errorLog.Append(e.Message); }

            bulkUploadFile.Title = file.Name;
            bulkUploadFile.Path = localFilePath;
            bulkUploadFile.CreatedBy = bulkUploadFile.LastModifiedBy = "System";
            bulkUploadFile.CreatedDate = bulkUploadFile.LastModifiedDate = DateTime.Now;
            bulkUploadFile.IsDeleted = false;

            if (errorLog.Length == 0)
            {
                _context.BulkUpload.Add(bulkUploadFile);
                _context.SaveChanges();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var existing = Get(id);

            if (existing.BulkUploadId == 0)
                return NotFound("Record doesn't exist");

            existing.IsDeleted = true;
            existing.LastModifiedBy = "System";
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }
    }
}
 