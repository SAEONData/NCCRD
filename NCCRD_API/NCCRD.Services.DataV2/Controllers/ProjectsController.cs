using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.Extensions;
using NCCRD.Services.DataV2.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("Projects")]
    [EnableCors("CORSPolicy")]
    public class ProjectsController : ODataController
    {
        public SQLDBContext _context { get; }
        IConfiguration _config { get; }

        public ProjectsController(SQLDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// Get a list of projects
        /// </summary>
        /// <returns>List of projects</returns>
        [HttpGet]
        [EnableQuery]
        public IQueryable<Project> Get()
        {
            return _context.Project.AsQueryable().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="id">ProjectId</param>
        /// <returns>Single project</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public Project Get(int id)
        {
            //return _context.Project.FirstOrDefault(x => x.ProjectId == id);
            var project = _context.Project.FirstOrDefault(x => x.ProjectId == id && x.IsDeleted == false);
            if (project == null)
                return new Project();
            else
                return project;

        }

        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="id">ProjectId</param>
        /// <returns>Single project</returns>
        [HttpDelete]
        [EnableQuery]
        [ODataRoute("({id})")]
        public StatusCodeResult Delete(int id)
        {
            Project project = _context.Project.FirstOrDefault(x => x.ProjectId == id);

            if (project == null)
                return NotFound();


            #region Foreign Key Constraints

            //ProjectRegion
            _context.ProjectRegion.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.ProjectRegion.Remove(a));

            //ProjectLocation
            _context.ProjectLocation.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.ProjectLocation.Remove(a));

            //MitigationEmmissionsData
            _context.MitigationEmissionsData.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.MitigationEmissionsData.Remove(a));

            //ResearchDetails
            _context.ResearchDetails.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.ResearchDetails.Remove(a));

            //AdaptationDetails
            _context.AdaptationDetails.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.AdaptationDetails.Remove(a));

            //MitigationDetails
            _context.MitigationDetails.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.MitigationDetails.Remove(a));

            //ProjectFunder
            _context.ProjectFunder.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.ProjectFunder.Remove(a));

            //ProjectDAOs
            _context.ProjectDAOs.Where(x => x.ProjectId == id).ToList().ForEach(a => _context.ProjectDAOs.Remove(a));

            #endregion

            _context.Project.Remove(project);
            _context.SaveChanges();

            return NoContent();
        }

        /*
        Filter query: http://localhost:62553/odata/Projects/Extensions.ByPolygon?$expand=ProjectLocations($expand=Location($select=LatCalculated,LonCalculated))&$select=ProjectId,ProjectTitle,ProjectDescription
        Get polygons here: http://196.21.191.55:8091/geoserver/SARVA/wms?service=WMS&version=1.1.0&request=GetMap&layers=SARVA:local_mn&styles=&bbox=16.3694229125977,-34.8977165222168,33.0274543762207,-22.0614833831787&width=768&height=591&srs=EPSG:4326&format=application/openlayers
        */
        /// <summary>
        /// Get projects by polygon
        /// </summary>
        /// <param name="polyObj">Polygon by which to search projects</param>
        /// <returns>Filtered list of projects</returns>
        [HttpPost]
        [EnableQuery]
        [ODataRoute("ByPolygon")]
        public IQueryable<Project> ByPolygon([FromBody] Polygon polyObj)
        {
            var projectIDs = GetByPolygon(polyObj.polygon);
            return _context.Project
                .Where(x => projectIDs.Contains(x.ProjectId))
                .AsQueryable();
        }

        /// <summary>
        /// Get a filtered list of projects
        /// </summary>
        /// <param name="filters">Composite object containing filters</param>
        /// <returns>Filtered list of projects</returns>
        [HttpPost]
        [EnableQuery]
        [ODataRoute("Filter")]
        public IQueryable<Project> Filter([FromBody] Filters filters)
        {
            string titleFilter = filters.title;
            string favsFilter = filters.favorites;
            int statusFilter = filters.status;
            int typologyFilter = filters.typology;
            int regionFilter = filters.region;
            int sectorFilter = filters.sector;
            int hazardFilter = filters.hazard;
            Guid daoidFilter = filters.daoid != null ? new Guid(filters.daoid) : Guid.Empty;
            string verified = filters.verified;

            //FAVORITES - OVERRIDES ALL OTHER FILTERS//
            if (!string.IsNullOrEmpty(favsFilter))
            {
                try
                {
                    var favs = favsFilter.Split(",").Select(f => int.Parse(f)).ToList();
                    return _context.Project.Where(p => favs.Contains(p.ProjectId));
                }
                catch
                {
                    return new List<Project>().AsQueryable();
                }
            }

            //REGION//
            var regionProjectIds = new List<int>();
            if (regionFilter > 0)
            {
                //Get all RegionIds (including children)
                var allRegionIDs = GetChildren(regionFilter, GetVMSData("regions/flat").Result).Select(r => r).Distinct().ToList();
                allRegionIDs.Add(regionFilter);

                //Get all ProjectIds assigned to these Regions and/or Typology
                regionProjectIds = _context.ProjectRegion.Where(p => allRegionIDs.Contains(p.RegionId)).Select(p => p.ProjectId).Distinct().ToList();
            }

            //SECTOR//
            var sectorProjectIds = new List<int>();
            if (sectorFilter > 0)
            {
                var allSectorIDs = GetChildren(sectorFilter, GetVMSData("sectors/flat").Result).Select(r => r).Distinct().ToList();
                allSectorIDs.Add(sectorFilter);

                sectorProjectIds.AddRange(_context.MitigationDetails.Where(x => sectorFilter == 0 || allSectorIDs.Contains((int)x.SectorId)).Select(x => x.ProjectId).ToList());
                sectorProjectIds.AddRange(_context.AdaptationDetails.Where(x => sectorFilter == 0 || allSectorIDs.Contains((int)x.SectorId)).Select(x => x.ProjectId).ToList());

                //Remove duplicates
                sectorProjectIds = sectorProjectIds.Distinct().ToList();
            }

            //TYPOLOGY//
            var typologyProjectIds = new List<int>();
            if (typologyFilter > 0)
            {
                if (_context.Typology.FirstOrDefault(x => x.TypologyId == typologyFilter).Value == "Adaptation")
                {
                    typologyProjectIds.AddRange(_context.AdaptationDetails.Select(x => x.ProjectId).Distinct().ToList());
                }
                else if (_context.Typology.FirstOrDefault(x => x.TypologyId == typologyFilter).Value == "Mitigation")
                {
                    typologyProjectIds.AddRange(_context.MitigationDetails.Select(x => x.ProjectId).Distinct().ToList());
                }
                else if (_context.Typology.FirstOrDefault(x => x.TypologyId == typologyFilter).Value == "Research")
                {
                    typologyProjectIds.AddRange(_context.ResearchDetails.Select(x => x.ProjectId).Distinct().ToList());
                }

                //Remove duplicates
                typologyProjectIds = typologyProjectIds.Distinct().ToList();
            }

            //STATUS//
            var statusProjectIds = new List<int>();
            if (statusFilter > 0)
            {
                statusProjectIds.AddRange(_context.Project.Where(x => x.ProjectStatusId == statusFilter).Select(x => x.ProjectId).ToList());
                statusProjectIds.AddRange(_context.AdaptationDetails.Where(x => x.ProjectStatusId == statusFilter).Select(x => x.ProjectId).ToList());
                statusProjectIds.AddRange(_context.MitigationDetails.Where(x => x.ProjectStatusId == statusFilter).Select(x => x.ProjectId).ToList());
                statusProjectIds = statusProjectIds.Distinct().ToList();
            }

            //HAZARD//
            var hazardProjectIds = new List<int>();
            if (hazardFilter > 0)
            {
                //Get all HazardIds (including children)
                var allHazardIDs = GetChildren(hazardFilter, GetVMSData("hazards/flat").Result).Select(r => r).Distinct().ToList();
                allHazardIDs.Add(hazardFilter);

                //Get all ProjectIds assigned to these Regions and/or Typology
                hazardProjectIds = _context.AdaptationDetails
                    .Where(a => allHazardIDs.Contains(a.HazardId == null ? 0 : (int)a.HazardId))
                    .Select(p => p.ProjectId)
                    .Distinct().ToList();
            }

            //GET PORJECTS FILTERED//
            //Retrieve project details and filter on query params
            return _context.Project.OrderBy(p => p.ProjectTitle)
                        .Where(p =>
                            (string.IsNullOrEmpty(titleFilter) || p.ProjectTitle.ToLower().Contains(titleFilter.ToLower())) &&
                            (statusFilter == 0 || statusProjectIds.Contains(p.ProjectId)) &&
                            (regionFilter == 0 || regionProjectIds.Contains(p.ProjectId)) &&
                            (sectorFilter == 0 || sectorProjectIds.Contains(p.ProjectId)) &&
                            (hazardFilter == 0 || hazardProjectIds.Contains(p.ProjectId)) &&
                            (typologyFilter == 0 || typologyProjectIds.Contains(p.ProjectId)) &&
                            (daoidFilter == Guid.Empty || p.ProjectDAOs.Any(dao => dao.DAOId == daoidFilter)) &&
                            (verified == "all" || verified == "verified" && p.Verified == true || verified == "unverified" && p.Verified == false)
                        );
        }

        /// <summary>
        /// Get a list of projects in GeoJSON format
        /// </summary>
        /// <returns>List of projects in GeoJSON format</returns>
        [HttpGet]
        [EnableQuery]
        [ODataRoute("GeoJson")]
        public JsonResult GeoJson()
        {

            var typologyData = _context.Typology.ToList();
            var vmsRegionData = GetVMSData("regions/flat").Result;
            var vmsSectorData = GetVMSData("sectors/flat").Result;

            var geoJSON = _context.ProjectLocation
                            .Include(pl => pl.Project)
                            .Include(pl => pl.Project.AdaptationDetails)
                            .Include(pl => pl.Project.MitigationDetails)
                            .Include(pl => pl.Project.ResearchDetails)
                            .Include(pl => pl.Project.ProjectRegions)
                            .Select(pl => new
                            {
                                type = "Feature",
                                geometry = new
                                {
                                    type = "Point",
                                    coordinates = new double[] {
                                        (double)pl.Location.LonCalculated,
                                        (double)pl.Location.LatCalculated
                                    }
                                },
                                properties = new
                                {
                                    id = pl.ProjectId,
                                    name = pl.Project.ProjectTitle,
                                    regions = GetGeoProps(pl.Project.ProjectRegions.Select(pr => pr.RegionId).ToArray(), vmsRegionData),
                                    sectors = GetGeoProps(GetProjectSectors(pl.Project.AdaptationDetails, pl.Project.MitigationDetails, pl.Project.ResearchDetails), vmsSectorData),
                                    typology = GetProjectTypology(pl.Project.AdaptationDetails, pl.Project.MitigationDetails, pl.Project.ResearchDetails, typologyData),
                                    status = GetProjectStatuses(pl.Project, pl.Project.AdaptationDetails, pl.Project.MitigationDetails)
                                },
                                data = new
                                {
                                    budgetLower = pl.Project.BudgetLower,
                                    budgetUpper = pl.Project.BudgetUpper,
                                    startYear = pl.Project.StartYear,
                                    endYear = pl.Project.EndYear
                                },
                                adaptation = pl.Project
                                    .AdaptationDetails
                                    .Select(a => new
                                    {
                                        hazard = a.HazardId
                                    })
                                    .ToList(),
                                mitigation = pl.Project
                                    .MitigationEmissionsData
                                    .Select(e => new
                                    {
                                        year = e.Year,
                                        CO2 = e.CO2 == null ? 0 : e.CO2
                                    })
                                    .ToList()
                            })
                            .Distinct()
                            .ToList();

            return new JsonResult(geoJSON);
        }

        private int[] GetProjectStatuses(Project project, IEnumerable<AdaptationDetail> adaptations, IEnumerable<MitigationDetail> mitigations)
        {
            var projectStatusIDs = new List<int>();

            if (project.ProjectStatusId != null)
            {
                projectStatusIDs.Add((int)project.ProjectStatusId);
            }

            projectStatusIDs.AddRange(adaptations.Select(a => a.ProjectStatusId).ToList());
            projectStatusIDs.AddRange(mitigations.Select(m => m.ProjectStatusId).ToList());

            return projectStatusIDs.Distinct().ToArray();
        }

        private int[] GetProjectSectors(IEnumerable<AdaptationDetail> adaptations, IEnumerable<MitigationDetail> mitigations, IEnumerable<ResearchDetail> research)
        {
            var sectors = new List<int>();

            sectors.AddRange(adaptations.Where(a => a.SectorId != null).Select(a => (int)a.SectorId));
            sectors.AddRange(mitigations.Where(m => m.SectorId != null).Select(a => (int)a.SectorId));

            return sectors.ToArray();
        }

        private int GetProjectTypology(IEnumerable<AdaptationDetail> adaptations, IEnumerable<MitigationDetail> mitigations, IEnumerable<ResearchDetail> research, IEnumerable<Typology> typologyData)
        {
            var typologyName = "";

            if (research.Count() > 0)
            {
                typologyName = "Research";
            }
            else if (adaptations.Count() > 0)
            {
                typologyName = "Adaptation";
            }
            else if (mitigations.Count() > 0)
            {
                typologyName = "Mitigation";
            }

            var typology = typologyData.FirstOrDefault(t => t.Value == typologyName);
            return typology != null ? typology.TypologyId : 0;
        }

        //##################//
        // Helper Functions //
        //##################//

        private List<int> GetByPolygon(string polygon)
        {
            //Check if polygon param is URL for download
            Uri uriResult;
            bool isPolyUrl = Uri.TryCreate(polygon, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isPolyUrl)
            {
                //Get actual polygon string from URL
                polygon = GetPolygonFromUrl(polygon);
            }

            var polygonWKT = new SqlParameter("@WKT", polygon);
            return _context.Project
                .FromSql("EXECUTE PolygonFilter @WKT", polygonWKT)
                .Select(x => x.ProjectId)
                .ToList();
        }

        private string GetPolygonFromUrl(string url)
        {
            string polygon = url;

            //Get polygon from URL
            if (!string.IsNullOrEmpty(url))
            {
                using (WebClient client = new WebClient())
                {
                    polygon = client.DownloadString(url);
                }
            }

            return polygon;
        }

        private async Task<List<StandardVocabItem>> GetVMSData(string relativeURL)
        {
            var result = new StandardVocabOutput();

            //Setup http-client
            var client = new HttpClient();
            client.BaseAddress = new Uri(_config.GetValue<string>("VmsApiBaseUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get data from VMS API
            var response = await client.GetAsync(relativeURL);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<StandardVocabOutput>(jsonString);
            }

            return result.Items;
        }

        private List<int> GetChildren(int filterID, List<StandardVocabItem> data)
        {
            var children = data
                .Where(x =>
                    x.AdditionalData.Any(y => y.Key == "ParentId" && y.Value == filterID.ToString())
                )
                .Select(x => int.Parse(x.Id))
                .ToList();

            var addChildren = new List<int>();
            foreach (var child in children)
            {
                //Add to temp list so as to not modify 'children' during iteration
                addChildren.AddRange(GetChildren(child, data));
            }
            //Transfer to actual list
            children.AddRange(addChildren);

            return children;
        }

        private List<int> GetParents(int filterID, List<StandardVocabItem> data)
        {
            var parentId = "";

            //Get ParentId
            var vmsItem = data.FirstOrDefault(x => x.Id == filterID.ToString());
            if (vmsItem != null)
            {
                var addItem = vmsItem.AdditionalData.FirstOrDefault(x => x.Key == "ParentId");
                if (!string.IsNullOrEmpty(addItem.Value))
                {
                    parentId = addItem.Value;
                }
            }

            var parents = data
                .Where(x =>
                    x.Id == parentId
                )
                .Select(x => int.Parse(x.Id))
                .ToList();

            var addParents = new List<int>();
            foreach (var p in parents)
            {
                //Add to temp list so as to not modify 'parents' during iteration
                addParents.AddRange(GetParents(p, data));
            }
            //Transfer to actual list
            parents.AddRange(addParents);

            return parents;
        }

        private List<List<int>> GetGeoProps(int[] items, List<StandardVocabItem> vmsItems)
        {
            var geoItems = new List<List<int>>();

            foreach (var r in items)
            {
                var itemGroup = new List<int>();
                itemGroup.Add(r);
                itemGroup.AddRange(GetParents(r, vmsItems));

                geoItems.Add(itemGroup);
            }

            return geoItems;
        }

        public int BulkUpload(Project record, ref System.Text.StringBuilder errorLog)
        {
            int returnInt = -1;
            try
            {
                _context.Project.Add(record);
                _context.SaveChanges();
                returnInt = record.ProjectId;
            }
            catch (Exception e)
            {
                errorLog.Append(e.Message + " | Project Title : " + record.ProjectTitle);
            }

            return returnInt;
        }
    }
}
