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
    [ODataRoutePrefix("Lookups")]
    [EnableCors("CORSPolicy")]
    public class LookupsController : ODataController
    {
        public SQLDBContext _context { get; }
        public LookupsController(SQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a lookup-object containing lookup-data.
        /// </summary>
        /// <returns>
        ///     Lookup-object containing lookup-data. 
        ///     <br/>
        ///     <b>Note: Look-up data (i.e. children) are not expanded by default.</b>
        /// </returns>
        [HttpGet]
        [EnableQuery]
        public Lookups Get()
        {
            return GetLookups();
        }

        //Add/Update
        /// <summary>
        /// Add/Update lookup-data
        /// </summary>
        /// <param name="data">A container for lookup-data</param>
        /// <returns>Success/Fail status</returns>
        [HttpPost]
        [EnableQuery]
        [Authorize(Roles = "Contributor,Custodian,Configurator,SysAdmin")]
        public async Task<IActionResult> Post([FromBody]Lookups data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Save AdaptationPurpose
            if (data.AdaptationPurpose != null)
            {
                foreach (var adaptationPurpose in data.AdaptationPurpose)
                {
                    var result = SaveAdaptationPurposeAsync(adaptationPurpose);
                    if (!(result is CreatedODataResult<AdaptationPurpose> || result is UpdatedODataResult<AdaptationPurpose>))
                    {
                        return result;
                    }
                }
            }

            //Save CarbonCredit
            if (data.CarbonCredit != null)
            {
                foreach (var carbonCredit in data.CarbonCredit)
                {
                    var result = SaveCarbonCreditAsync(carbonCredit);
                    if (!(result is CreatedODataResult<CarbonCredit> || result is UpdatedODataResult<CarbonCredit>))
                    {
                        return result;
                    }
                }
            }

            //Save CarbonCreditMarket
            if (data.CarbonCreditMarket != null)
            {
                foreach (var carbonCreditMarket in data.CarbonCreditMarket)
                {
                    var result = SaveCarbonCreditMarketAsync(carbonCreditMarket);
                    if (!(result is CreatedODataResult<CarbonCreditMarket> || result is UpdatedODataResult<CarbonCreditMarket>))
                    {
                        return result;
                    }
                }
            }

            //Save CDMMethodology
            if (data.CDMMethodology != null)
            {
                foreach (var cdmMethodology in data.CDMMethodology)
                {
                    var result = SaveCDMMethodologyAsync(cdmMethodology);
                    if (!(result is CreatedODataResult<CDMMethodology> || result is UpdatedODataResult<CDMMethodology>))
                    {
                        return result;
                    }
                }
            }

            //Save CDMStatus
            if (data.CDMStatus != null)
            {
                foreach (var cdmStatus in data.CDMStatus)
                {
                    var result = SaveCDMStatusAsync(cdmStatus);
                    if (!(result is CreatedODataResult<CDMStatus> || result is UpdatedODataResult<CDMStatus>))
                    {
                        return result;
                    }
                }
            }

            //Save Person
            if (data.Person != null)
            {
                foreach (var person in data.Person)
                {
                    var result = SavePersonAsync(person);
                    if (!(result is CreatedODataResult<Person> || result is UpdatedODataResult<Person>))
                    {
                        return result;
                    }
                }
            }

            //Save ProjectStatus
            if (data.ProjectStatus != null)
            {
                foreach (var projectStatus in data.ProjectStatus)
                {
                    var result = SaveProjectStatusAsync(projectStatus);
                    if (!(result is CreatedODataResult<ProjectStatus> || result is UpdatedODataResult<ProjectStatus>))
                    {
                        return result;
                    }
                }
            }

            //Save ProjectType
            if (data.ProjectType != null)
            {
                foreach (var projectType in data.ProjectType)
                {
                    var result = SaveProjectTypeAsync(projectType);
                    if (!(result is CreatedODataResult<ProjectType> || result is UpdatedODataResult<ProjectType>))
                    {
                        return result;
                    }
                }
            }

            //Save ProjectSubType
            if (data.ProjectSubType != null)
            {
                foreach (var projectSubType in data.ProjectSubType)
                {
                    var result = SaveProjectSubTypeAsync(projectSubType);
                    if (!(result is CreatedODataResult<ProjectSubType> || result is UpdatedODataResult<ProjectSubType>))
                    {
                        return result;
                    }
                }
            }

            //Save ResearchType
            if (data.ResearchType != null)
            {
                foreach (var researchType in data.ResearchType)
                {
                    var result = SaveResearchTypeAsync(researchType);
                    if (!(result is CreatedODataResult<ResearchType> || result is UpdatedODataResult<ResearchType>))
                    {
                        return result;
                    }
                }
            }

            //Save TargetAudience
            if (data.TargetAudience != null)
            {
                foreach (var targetAudience in data.TargetAudience)
                {
                    var result = SaveTargetAudienceAsync(targetAudience);
                    if (!(result is CreatedODataResult<TargetAudience> || result is UpdatedODataResult<TargetAudience>))
                    {
                        return result;
                    }
                }
            }

            //Save Typology
            if (data.Typology != null)
            {
                foreach (var typology in data.Typology)
                {
                    var result = SaveTypologyAsync(typology);
                    if (!(result is CreatedODataResult<Typology> || result is UpdatedODataResult<Typology>))
                    {
                        return result;
                    }
                }
            }

            //Save ValidationStatus
            if (data.ValidationStatus != null)
            {
                foreach (var validationStatus in data.ValidationStatus)
                {
                    var result = SaveValidationStatusAsync(validationStatus);
                    if (!(result is CreatedODataResult<ValidationStatus> || result is UpdatedODataResult<ValidationStatus>))
                    {
                        return result;
                    }
                }
            }

            //Save VoluntaryGoldStandard
            if (data.VoluntaryGoldStandard != null)
            {
                foreach (var voluntaryGoldStandard in data.VoluntaryGoldStandard)
                {
                    var result = SaveVoluntaryGoldStandardAsync(voluntaryGoldStandard);
                    if (!(result is CreatedODataResult<VoluntaryGoldStandard> || result is UpdatedODataResult<VoluntaryGoldStandard>))
                    {
                        return result;
                    }
                }
            }

            //Save VoluntaryMethodology
            if (data.VoluntaryMethodology != null)
            {
                foreach (var voluntaryMethodology in data.VoluntaryMethodology)
                {
                    var result = SaveVoluntaryMethodologyAsync(voluntaryMethodology);
                    if (!(result is CreatedODataResult<VoluntaryMethodology> || result is UpdatedODataResult<VoluntaryMethodology>))
                    {
                        return result;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        internal Lookups GetLookups(int id = 0)
        {
            //Get lookups
            var adaptationPurpose = _context.AdaptationPurpose.OrderBy(x => x.Value).ToArray();
            var carbonCredit = _context.CarbonCredit.OrderBy(x => x.Value).ToArray();
            var carbonCreditMarket = _context.CarbonCreditMarket.OrderBy(x => x.Value).ToArray();
            var cdmMethodology = _context.CDMMethodology.OrderBy(x => x.Value).ToArray();
            var cdmStatus = _context.CDMStatus.OrderBy(x => x.Value).ToArray();
            var fundingStatus = _context.FundingStatus.OrderByDescending(x => x.FundingStatusId).ToArray();
            var projectStatus = _context.ProjectStatus.OrderBy(x => x.ProjectStatusId).ToArray();
            var projectType = _context.ProjectType.OrderBy(x => x.Value).ToArray();
            var projectSubType = _context.ProjectSubType.OrderBy(x => x.Value).ToArray();
            var researchType = _context.ResearchType.OrderBy(x => x.Value).ToArray();
            var targetAudience = _context.TargetAudience.OrderBy(x => x.Value).ToArray();
            var typology = _context.Typology.OrderBy(x => x.Value).ToArray();
            var user = _context.Person.OrderBy(x => x.FirstName).ThenBy(x => x.Surname).ToArray();
            var validationStatus = _context.ValidationStatus.OrderBy(x => x.Value).ToArray();
            var voluntaryGoldStandard = _context.VoluntaryGoldStandard.OrderBy(x => x.Value).ToArray();
            var voluntaryMethodology = _context.VoluntaryMethodology.OrderBy(x => x.Value).ToArray();
            var researchMaturity = _context.ResearchMaturity.OrderBy(x => x.Value).ToArray();
            var mitigationSector = _context.MitigationSector.OrderBy(x => x.Description).ToArray();

            return new Lookups()
            {
                Id = id == 0 ? int.Parse(DateTime.Now.ToString("HHmmssfff")) : id,
                AdaptationPurpose = adaptationPurpose,
                CarbonCredit = carbonCredit,
                CarbonCreditMarket = carbonCreditMarket,
                CDMMethodology = cdmMethodology,
                CDMStatus = cdmStatus,
                FundingStatus = fundingStatus,
                ProjectStatus = projectStatus,
                ProjectSubType = projectSubType,
                ProjectType = projectType,
                ResearchType = researchType,
                TargetAudience = targetAudience,
                Typology = typology,
                Person = user,
                ValidationStatus = validationStatus,
                VoluntaryGoldStandard = voluntaryGoldStandard,
                VoluntaryMethodology = voluntaryMethodology,
                ResearchMaturity = researchMaturity,
                MitigationSector = mitigationSector
            };
        }

        private IActionResult SaveAdaptationPurposeAsync(AdaptationPurpose item)
        {
            //Check that Value/Name is unique
            if (_context.AdaptationPurpose.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.AdaptationPurposeId != item.AdaptationPurposeId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.AdaptationPurpose.FirstOrDefault(x => x.AdaptationPurposeId == item.AdaptationPurposeId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.AdaptationPurpose.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveCarbonCreditAsync(CarbonCredit item)
        {
            //Check that Value/Name is unique
            if (_context.CarbonCredit.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.CarbonCreditId != item.CarbonCreditId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.CarbonCredit.FirstOrDefault(x => x.CarbonCreditId == item.CarbonCreditId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.CarbonCredit.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveCarbonCreditMarketAsync(CarbonCreditMarket item)
        {
            //Check that Value/Name is unique
            if (_context.CarbonCreditMarket.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.CarbonCreditMarketId != item.CarbonCreditMarketId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.CarbonCreditMarket.FirstOrDefault(x => x.CarbonCreditMarketId == item.CarbonCreditMarketId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.CarbonCreditMarket.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveCDMMethodologyAsync(CDMMethodology item)
        {
            //Check that Value/Name is unique
            if (_context.CDMMethodology.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.CDMMethodologyId != item.CDMMethodologyId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.CDMMethodology.FirstOrDefault(x => x.CDMMethodologyId == item.CDMMethodologyId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.CDMMethodology.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveCDMStatusAsync(CDMStatus item)
        {
            //Check that Value/Name is unique
            if (_context.CDMStatus.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.CDMStatusId != item.CDMStatusId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.CDMStatus.FirstOrDefault(x => x.CDMStatusId == item.CDMStatusId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.CDMStatus.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SavePersonAsync(Person item)
        {
            //Check that Value/Name is unique
            if (_context.Person.AsNoTracking().FirstOrDefault(x => x.EmailAddress == item.EmailAddress && x.PersonId != item.PersonId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.Person.FirstOrDefault(x => x.PersonId == item.PersonId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.Person.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveProjectStatusAsync(ProjectStatus item)
        {
            //Check that Value/Name is unique
            if (_context.ProjectStatus.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.ProjectStatusId != item.ProjectStatusId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.ProjectStatus.FirstOrDefault(x => x.ProjectStatusId == item.ProjectStatusId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.ProjectStatus.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveProjectTypeAsync(ProjectType item)
        {
            //Check that Value/Name is unique
            if (_context.ProjectType.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.ProjectTypeId != item.ProjectTypeId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.ProjectType.FirstOrDefault(x => x.ProjectTypeId == item.ProjectTypeId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.ProjectType.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveProjectSubTypeAsync(ProjectSubType item)
        {
            //Check that Value/Name is unique
            if (_context.ProjectSubType.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.ProjectSubTypeId != item.ProjectSubTypeId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.ProjectSubType.FirstOrDefault(x => x.ProjectSubTypeId == item.ProjectSubTypeId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.ProjectSubType.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveResearchTypeAsync(ResearchType item)
        {
            //Check that Value/Name is unique
            if (_context.ResearchType.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.ResearchTypeId != item.ResearchTypeId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.ResearchType.FirstOrDefault(x => x.ResearchTypeId == item.ResearchTypeId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.ResearchType.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveTargetAudienceAsync(TargetAudience item)
        {
            //Check that Value/Name is unique
            if (_context.TargetAudience.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.TargetAudienceId != item.TargetAudienceId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.TargetAudience.FirstOrDefault(x => x.TargetAudienceId == item.TargetAudienceId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.TargetAudience.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveTypologyAsync(Typology item)
        {
            //Check that Value/Name is unique
            if (_context.Typology.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.TypologyId != item.TypologyId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.Typology.FirstOrDefault(x => x.TypologyId == item.TypologyId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.Typology.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveValidationStatusAsync(ValidationStatus item)
        {
            //Check that Value/Name is unique
            if (_context.ValidationStatus.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.ValidationStatusId != item.ValidationStatusId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.ValidationStatus.FirstOrDefault(x => x.ValidationStatusId == item.ValidationStatusId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.ValidationStatus.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveVoluntaryGoldStandardAsync(VoluntaryGoldStandard item)
        {
            //Check that Value/Name is unique
            if (_context.VoluntaryGoldStandard.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.VoluntaryGoldStandardId != item.VoluntaryGoldStandardId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.VoluntaryGoldStandard.FirstOrDefault(x => x.VoluntaryGoldStandardId == item.VoluntaryGoldStandardId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.VoluntaryGoldStandard.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }

        private IActionResult SaveVoluntaryMethodologyAsync(VoluntaryMethodology item)
        {
            //Check that Value/Name is unique
            if (_context.VoluntaryMethodology.AsNoTracking().FirstOrDefault(x => x.Value == item.Value && x.VoluntaryMethodologyId != item.VoluntaryMethodologyId) != null)
            {
                return BadRequest("Duplicate entry/value found.");
            }

            var exiting = _context.VoluntaryMethodology.FirstOrDefault(x => x.VoluntaryMethodologyId == item.VoluntaryMethodologyId);
            if (exiting == null)
            {
                //ADD
                HelperExtensions.ClearIdentityValue(ref item);
                HelperExtensions.ClearNullableInts(ref item);
                _context.VoluntaryMethodology.Add(item);
                return Created(item);
            }
            else
            {
                //UPDATE
                _context.Entry(exiting).CurrentValues.SetValues(item);
                return Updated(exiting);
            }
        }
    }
}