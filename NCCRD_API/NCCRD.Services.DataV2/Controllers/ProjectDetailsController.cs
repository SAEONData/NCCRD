using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Results;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCRD.Services.DataV2.Database.Contexts;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.Extensions;
using NCCRD.Services.DataV2.ViewModels;

namespace NCCRD.Services.DataV2.Controllers
{
    [Produces("application/json")]
    [ODataRoutePrefix("ProjectDetails")]
    [EnableCors("CORSPolicy")]
    public class ProjectDetailsController : ODataController
    {
        private bool _projectAdded = false;

        public SQLDBContext _context { get; }
        public ProjectDetailsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get ProjectDetails for a specific ProjectId
        /// </summary>
        /// <param name="id">ProjectId</param>
        /// <returns>ProjectDetails for specified ProjectId</returns>
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 0)]
        [ODataRoute("({id})")]
        public ProjectDetails Get(int id)
        {
            //Get project and details
            var project = _context.Project
                .Include(x => x.ProjectRegions)
                .Include(x => x.ProjectDAOs)
                .Include(x => x.ProjectLocations).ThenInclude(x => x.Location)
                .FirstOrDefault(x => x.ProjectId == id && x.IsDeleted == false);

            var funders = _context.ProjectFunder.Include(x => x.Funder).Where(x => x.ProjectId == id && x.IsDeleted == false)
                .OrderBy(x => x.FunderId).Select(x => x.Funder).ToArray();

            var adaptations = _context.AdaptationDetails.Where(x => x.ProjectId == id && x.IsDeleted == false)
                .OrderBy(x => x.AdaptationDetailId).ToArray();

            var mitigations = _context.MitigationDetails.Where(x => x.ProjectId == id && x.IsDeleted == false)
                .OrderBy(x => x.MitigationDetailId).ToArray();

            var emissions = _context.MitigationEmissionsData.Where(x => x.ProjectId == id && x.IsDeleted == false)
                .OrderBy(x => x.MitigationEmissionsDataId).ToArray();

            var research = _context.ResearchDetails.Where(x => x.ProjectId == id && x.IsDeleted == false)
                .OrderBy(x => x.ResearchDetailId).ToArray();

            var lookups = new LookupsController(_context).GetLookups();

            var res = new ProjectDetails()
            {
                Id = id == 0 ? int.Parse(DateTime.Now.ToString("HHmmssfff")) : id,
                Project = project,
                Funders = funders,
                AdaptationDetails = adaptations,
                MitigationDetails = mitigations,
                MitigationEmissionsData = emissions,
                ResearchDetails = research,
                Lookups = lookups
            };

            return res;
        }

        //Add/Update
        /// <summary>
        /// Add/Update ProjectDetails
        /// </summary>
        /// <param name="data">A composite object containg all the project's details and associated data</param>
        /// <returns>ProjectId on success; Error/Code on fail</returns>
        [HttpPost]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]ProjectDetails data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool projectAdded = true;

                /*
                If you're adding a new project data.Project will always have a value.
                If you're editing a project, data.Project might be null, but it will always exist in the database
                */

                //Build Project for save
                var project = new Project();

                //Get project if exists
                if (_context.Project.Any(x => x.ProjectId == data.Id && x.IsDeleted == false))
                {
                    project = _context.Project
                        .Include(x => x.ProjectDAOs)
                        .Include(x => x.ProjectRegions)
                        .Include(x => x.ProjectLocations).ThenInclude(x => x.Location)
                        .Include(x => x.ProjectFunders).ThenInclude(x => x.Funder)
                        .Include(x => x.AdaptationDetails).ThenInclude(x => x.ResearchDetail)
                        .First(x => x.ProjectId == data.Id && x.IsDeleted == false);

                    projectAdded = false;
                }

                //Update 'project'
                if (data.Project != null)
                {
                    //###########################//
                    // Project //
                    //###########################//
                    //...Details
                    project.ProjectId = data.Id;
                    project.ProjectTitle = data.Project.ProjectTitle;
                    project.ProjectDescription = data.Project.ProjectDescription;
                    project.Link = data.Project.Link;
                    project.StartYear = data.Project.StartYear;
                    project.EndYear = data.Project.EndYear;
                    project.ProjectStatusId = data.Project.ProjectStatusId;
                    project.ProjectStatus = null; //Not needed if ID is present
                    project.BudgetLower = data.Project.BudgetLower;
                    project.BudgetUpper = data.Project.BudgetUpper;

                    //...Manager
                    project.LeadAgent = data.Project.LeadAgent;
                    project.ProjectManagerId = data.Project.ProjectManagerId;
                    project.ProjectManager = null; //Not needed if ID is present
                    project.HostOrganisation = data.Project.HostOrganisation;
                    project.HostPartner = data.Project.HostPartner;
                    project.AlternativeContact = data.Project.AlternativeContact;
                    project.AlternativeContactEmail = data.Project.AlternativeContactEmail;

                    //...Verified
                    project.Verified = data.Project.Verified;

                    //###########################//
                    //DAO Links
                    //###########################//
                    if (project.ProjectDAOs == null)
                    {
                        project.ProjectDAOs = new List<ProjectDAO>();
                    }

                    //...Remove
                    var daoRemove = project.ProjectDAOs.Where(x => !data.Project.ProjectDAOs.Any(y => y.DAOId == x.DAOId))
                        .Select(x => x.ProjectDAOId)
                        .ToArray();

                    foreach (var item in daoRemove)
                    {
                        var removeItem = project.ProjectDAOs.First(x => x.ProjectDAOId == item && x.IsDeleted == false);
                        //Adding in Soft Delete
                        removeItem.IsDeleted = true;
                    }

                    //...Add
                    var daoAdd = data.Project.ProjectDAOs.Where(x => !project.ProjectDAOs.Any(y => y.DAOId == x.DAOId && x.IsDeleted == false));
                    foreach (var item in daoAdd)
                    {
                        project.ProjectDAOs.Add(new ProjectDAO()
                        {
                            DAOId = item.DAOId,
                            ProjectId = project.ProjectId,
                            Project = project
                        });
                    }

                    //###########################//
                    //ProjectRegions
                    //###########################//
                    if (project.ProjectRegions == null)
                    {
                        project.ProjectRegions = new List<ProjectRegion>();
                    }

                    //...Remove
                    var prRemove = project.ProjectRegions.Where(x => !data.Project.ProjectRegions.Any(y => y.RegionId == x.RegionId && y.IsDeleted == false))
                        .Select(x => x.ProjectRegionId)
                        .ToArray();

                    foreach (var item in prRemove)
                    {
                        var removeItem = project.ProjectRegions.First(x => x.ProjectRegionId == item && x.IsDeleted == false);
                        //project.ProjectRegions.Remove(removeItem);
                        //Adding in Soft Delete
                        removeItem.IsDeleted = true;
                    }

                    //...Add
                    var prAdd = data.Project.ProjectRegions.Where(x => !project.ProjectRegions.Any(y => y.RegionId == x.RegionId && y.IsDeleted == false));
                    foreach (var item in prAdd)
                    {
                        project.ProjectRegions.Add(new ProjectRegion()
                        {
                            RegionId = item.RegionId,
                            ProjectId = project.ProjectId,
                            Project = project
                        });
                    }

                    //###########################//
                    //ProjectLocations
                    //###########################//
                    if (project.ProjectLocations == null)
                    {
                        project.ProjectLocations = new List<ProjectLocation>();
                    }

                    //...Remove
                    var locRemove = project.ProjectLocations.Where(x => !data.Project.ProjectLocations.Any(y => y.Location.LatCalculated == x.Location.LatCalculated && y.Location.LonCalculated == x.Location.LonCalculated) && x.IsDeleted == false)
                        .Select(x => x.ProjectLocationId)
                        .ToArray();

                    foreach (var item in locRemove)
                    {
                        var removeItem = project.ProjectLocations.First(x => x.ProjectLocationId == item);
                        //project.ProjectLocations.Remove(removeItem);
                        //Adding in Soft Delete
                        removeItem.IsDeleted = true;
                    }

                    //...Add
                    var locAdd = data.Project.ProjectLocations.Where(x => !project.ProjectLocations.Any(y => y.Location.LatCalculated == x.Location.LatCalculated && y.Location.LonCalculated == x.Location.LonCalculated) && x.IsDeleted == false);
                    foreach (var item in locAdd)
                    {
                        project.ProjectLocations.Add(new ProjectLocation()
                        {
                            LocationId = item.LocationId,
                            Location = new Location()
                            {
                                LatCalculated = item.Location.LatCalculated,
                                LonCalculated = item.Location.LonCalculated
                            },
                            ProjectId = project.ProjectId,
                            Project = project
                        });
                    }

                    //###########################//
                    //Funders
                    //###########################//
                    if (project.ProjectFunders == null)
                    {
                        project.ProjectFunders = new List<ProjectFunder>();
                    }

                    //...Remove
                    var pfRemove = project.ProjectFunders.Where(x => !data.Funders.Any(y => y.FunderId == x.FunderId) && x.IsDeleted == false)
                        .Select(x => x.ProjectFunderId)
                        .ToArray();

                    foreach (var item in pfRemove)
                    {
                        var removeItem = project.ProjectFunders.First(x => x.ProjectFunderId == item);
                        //project.ProjectFunders.Remove(removeItem);
                        //Adding in Soft Delete
                        removeItem.IsDeleted = false;
                    }

                    //...Update
                    var pfUpdate = project.ProjectFunders.Where(x => data.Funders.Any(y => y.FunderId == x.FunderId) && x.IsDeleted == false);
                    foreach (var item in pfUpdate)
                    {
                        var updateItem = item.Funder;
                        var updateSource = data.Funders.First(y => y.FunderId == updateItem.FunderId);

                        updateItem.GrantProgName = updateSource.GrantProgName;
                        updateItem.FundingAgency = updateSource.FundingAgency;
                        updateItem.PartnerDepsOrgs = updateSource.PartnerDepsOrgs;
                        updateItem.ProjectCoordinatorId = updateSource.ProjectCoordinatorId;
                        updateItem.TotalBudget = updateSource.TotalBudget;
                        updateItem.AnnualBudget = updateSource.AnnualBudget;
                        updateItem.FundingStatusId = updateSource.FundingStatusId;
                    }

                    //...Add
                    var pfAdd = data.Funders.Where(x => !project.ProjectFunders.Any(y => y.FunderId == x.FunderId) && x.IsDeleted == false);
                    foreach (var item in pfAdd)
                    {
                        project.ProjectFunders.Add(new ProjectFunder()
                        {
                            FunderId = item.FunderId,
                            Funder = new Funder()
                            {
                                GrantProgName = item.GrantProgName,
                                FundingAgency = item.FundingAgency,
                                PartnerDepsOrgs = item.PartnerDepsOrgs,
                                ProjectCoordinatorId = item.ProjectCoordinatorId,
                                TotalBudget = item.TotalBudget,
                                AnnualBudget = item.AnnualBudget,
                                FundingStatusId = item.FundingStatusId
                            },
                            ProjectId = project.ProjectId,
                            Project = project
                        });
                    }

                    //###########################//
                    //Adaptations
                    //###########################//
                    if (project.AdaptationDetails == null)
                    {
                        project.AdaptationDetails = new List<AdaptationDetail>();
                    }

                    //...Remove
                    var adRemove = project.AdaptationDetails.Where(x => !data.AdaptationDetails.Any(y => y.AdaptationDetailId == x.AdaptationDetailId) && x.IsDeleted == false)
                        .Select(x => x.AdaptationDetailId)
                        .ToArray();

                    foreach (var item in adRemove)
                    {
                        var removeItem = project.AdaptationDetails.First(x => x.AdaptationDetailId == item);

                        if (removeItem.ResearchDetail != null)
                        {
                            //Remove ResearchDetail
                            //_context.ResearchDetails.Remove(removeItem.ResearchDetail);
                            //Adding in Soft Delete
                            removeItem.ResearchDetail.IsDeleted = true;
                        }

                        //project.AdaptationDetails.Remove(removeItem);
                        //Adding in Soft Delete
                        removeItem.IsDeleted = true;
                    }

                    //...Update
                    var adUpdate = project.AdaptationDetails.Where(x => data.AdaptationDetails.Any(y => y.AdaptationDetailId == x.AdaptationDetailId) && x.IsDeleted == false);
                    foreach (var item in adUpdate)
                    {
                        var updateItem = item;
                        var updateSource = data.AdaptationDetails.First(y => y.AdaptationDetailId == updateItem.AdaptationDetailId && y.IsDeleted == false);

                        updateItem.Title = updateSource.Title;
                        updateItem.Description = updateSource.Description;
                        updateItem.AdaptationPurposeId = updateSource.AdaptationPurposeId;
                        updateItem.AdaptationPurpose = null; //Not needed if ID is present
                        updateItem.SectorId = updateSource.SectorId;
                        //updateItem.HazardId = updateSource.HazardId;
                        updateItem.ProjectStatusId = updateSource.ProjectStatusId;
                        updateItem.ProjectStatus = null; //Not needed if ID is present
                        updateItem.ContactName = updateSource.ContactName;
                        updateItem.ContactEmail = updateSource.ContactEmail;

                        //...ResearchDetail
                        //...Remove
                        if (updateItem.ResearchDetail != null && updateSource.ResearchDetail == null)
                        {
                            //Remove ResearchDetail
                            //_context.ResearchDetails.Remove(updateItem.ResearchDetail);
                            //Adding in Soft Delete 
                            updateItem.ResearchDetail.IsDeleted = false;
                            //NOTE HERE
                            //updateItem.ResearchDetail = null;
                        }

                        //...Update
                        if (updateItem.ResearchDetail != null && updateItem.IsDeleted == false && updateSource.ResearchDetail != null && updateSource.IsDeleted == false)
                        {
                            updateItem.ResearchDetail.Author = updateSource.ResearchDetail.Author;
                            updateItem.ResearchDetail.PaperLink = updateSource.ResearchDetail.PaperLink;
                            updateItem.ResearchDetail.ResearchTypeId = updateSource.ResearchDetail.ResearchTypeId;
                            updateItem.ResearchDetail.ResearchType = null; //Not needed if ID is present
                            updateItem.ResearchDetail.TargetAudienceId = updateSource.ResearchDetail.TargetAudienceId;
                            updateItem.ResearchDetail.TargetAudience = null; //Not needed if ID is present
                            updateItem.ResearchDetail.ResearchMaturityId = updateSource.ResearchDetail.ResearchMaturityId;
                            updateItem.ResearchDetail.ResearchMaturity = null; //Not needed if ID is present
                        }

                        //...Add
                        if (updateItem.ResearchDetail == null && updateSource.ResearchDetail != null)
                        {
                            updateItem.ResearchDetail = new ResearchDetail()
                            {
                                Author = updateSource.ResearchDetail.Author,
                                PaperLink = updateSource.ResearchDetail.PaperLink,
                                ResearchTypeId = updateSource.ResearchDetail.ResearchTypeId,
                                ResearchType = null, //Not needed if ID is present
                                TargetAudienceId = updateSource.ResearchDetail.TargetAudienceId,
                                TargetAudience = null, //Not needed if ID is present
                                ResearchMaturityId = updateSource.ResearchDetail.ResearchMaturityId,
                                ResearchMaturity = null, //Not needed if ID is present
                                ProjectId = project.ProjectId,
                                Project = project
                            };
                        }
                    }

                    //...Add
                    var adAdd = data.AdaptationDetails.Where(x => !project.AdaptationDetails.Any(y => y.AdaptationDetailId == x.AdaptationDetailId) && x.IsDeleted == false);
                    foreach (var item in adAdd)
                    {
                        project.AdaptationDetails.Add(new AdaptationDetail()
                        {
                            Title = item.Title,
                            Description = item.Description,
                            AdaptationPurposeId = item.AdaptationPurposeId,
                            AdaptationPurpose = null, //Not needed if ID is present
                            SectorId = item.SectorId,
                            HazardId = item.HazardId,
                            ProjectStatusId = item.ProjectStatusId,
                            ProjectStatus = null, //Not needed if ID is present
                            ContactName = item.ContactName,
                            ContactEmail = item.ContactEmail,

                            ResearchDetail = item.ResearchDetail == null ? null :
                                new ResearchDetail()
                                {
                                    Author = item.ResearchDetail.Author,
                                    PaperLink = item.ResearchDetail.PaperLink,
                                    ResearchTypeId = item.ResearchDetail.ResearchTypeId,
                                    ResearchType = null, //Not needed if ID is present
                                    TargetAudienceId = item.ResearchDetail.TargetAudienceId,
                                    TargetAudience = null, //Not needed if ID is present
                                    ResearchMaturityId = item.ResearchDetail.ResearchMaturityId,
                                    ResearchMaturity = null, //Not needed if ID is present
                                    ProjectId = project.ProjectId,
                                    Project = project
                                },

                            ProjectId = project.ProjectId,
                            Project = project
                        });
                    }

                }

                //Save
                if (projectAdded)
                {
                    _context.Project.Add(project);
                }
                _context.SaveChanges();

                //return data object
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}