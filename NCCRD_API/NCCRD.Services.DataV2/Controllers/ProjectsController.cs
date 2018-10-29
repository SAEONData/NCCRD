﻿using Microsoft.AspNet.OData;
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

        [HttpGet]
        [EnableQuery]
        public IQueryable<Project> Get()
        {
            return _context.Project.AsQueryable();
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public Project Get(int id)
        {
            return _context.Project.FirstOrDefault(x => x.ProjectId == id);
        }

        /*
        Filter query: http://localhost:62553/odata/Projects/Extensions.ByPolygon?$expand=ProjectLocations($expand=Location($select=LatCalculated,LonCalculated))&$select=ProjectId,ProjectTitle,ProjectDescription
        Get polygons here: http://196.21.191.55:8091/geoserver/SARVA/wms?service=WMS&version=1.1.0&request=GetMap&layers=SARVA:local_mn&styles=&bbox=16.3694229125977,-34.8977165222168,33.0274543762207,-22.0614833831787&width=768&height=591&srs=EPSG:4326&format=application/openlayers
        */
        [HttpPost]
        [EnableQuery]
        public IQueryable<Project> ByPolygon([FromBody] Polygon polyObj)
        {
            var projectIDs = GetByPolygon(polyObj.polygon);
            return _context.Project
                .Where(x => projectIDs.Contains(x.ProjectId))
                .AsQueryable();
        }

        [HttpPost]
        [EnableQuery]
        public IQueryable<Project> Filter([FromBody] Filters filters)
        {
            string titleFilter = filters.title;
            int statusFilter = filters.status;
            int typologyFilter = filters.typology;
            int regionFilter = filters.region;
            int sectorFilter = filters.sector;
            Guid daoidFilter = filters.daoid != null ? new Guid(filters.daoid) : Guid.Empty;

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
                sectorProjectIds.AddRange(_context.ResearchDetails.Where(x => sectorFilter == 0 || allSectorIDs.Contains((int)x.SectorId)).Select(x => x.ProjectId).ToList());

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
                statusProjectIds.AddRange(_context.AdaptationDetails.Where(x => x.ProjectStatusId == statusFilter).Select(x => x.ProjectId).ToList());
                statusProjectIds.AddRange(_context.MitigationDetails.Where(x => x.ProjectStatusId == statusFilter).Select(x => x.ProjectId).ToList());
            }


            //GET PORJECTS FILTERED//
            //Retrieve project details and filter on query params
            return _context.Project.OrderBy(p => p.ProjectTitle)
                        .Where(p =>
                            (string.IsNullOrEmpty(titleFilter) || p.ProjectTitle.ToLower().Contains(titleFilter.ToLower())) &&
                            (statusFilter == 0 || statusProjectIds.Contains(p.ProjectId)) &&
                            (regionFilter == 0 || regionProjectIds.Contains(p.ProjectId)) &&
                            (sectorFilter == 0 || sectorProjectIds.Contains(p.ProjectId)) &&
                            (typologyFilter == 0 || typologyProjectIds.Contains(p.ProjectId)) &&
                            (daoidFilter == Guid.Empty || p.ProjectDAOs.Any(dao => dao.DAOId == daoidFilter))
                        );
        }

        [HttpGet]
        [EnableQuery]
        public JsonResult GeoJson()
        {
            List<ProjectGeoJson> projectGeo = new List<ProjectGeoJson>();

            //GET PROJECT DATA//
            var projectData = (from proj in _context.Project
                               join projLoc in _context.ProjectLocation on proj.ProjectId equals projLoc.ProjectId
                               join loc in _context.Location on projLoc.LocationId equals loc.LocationId
                               select new
                               {
                                   proj.ProjectId,
                                   proj.ProjectTitle,
                                   loc.LatCalculated,
                                   loc.LonCalculated
                               }).ToList();

            foreach (var projDat in projectData)
            {
                ProjectGeoJson item = new ProjectGeoJson();
                item.type = "Feature";
                item.geometry = new GeoJsonGeometry()
                {
                    type = "Point",
                    coordinates = new List<double>() { (double)projDat.LatCalculated, (double)projDat.LonCalculated }
                };
                item.properties = new GeoJsonProperties()
                {
                    id = projDat.ProjectId.ToString(),
                    name = projDat.ProjectTitle
                };

                projectGeo.Add(item);
            }

            return new JsonResult(projectGeo);
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

        //private List<Sector> GetChildSectors(int sectorId, List<Sector> sectorList)
        //{
        //    var sectors = sectorList.Where(x => x.ParentSectorId == sectorId).ToList();

        //    var childSectors = new List<Sector>();
        //    foreach (var sector in sectors)
        //    {
        //        childSectors.AddRange(GetChildSectors(sector.SectorId, sectorList));
        //    }
        //    sectors.AddRange(childSectors);

        //    return sectors;
        //}
    }
}
