using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using Excel;
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
    [ODataRoutePrefix("BulkUpload")]
    [EnableCors("CORSPolicy")]
    public class BulkUploadController : ODataController
    {
        public SQLDBContext _context { get; }
        IConfiguration _config { get; }
        public BulkUploadController(SQLDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// Insert a mass amount of Projects
        /// </summary>
        /// <returns>Http Status code</returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Post([FromForm] IFormFile file)
        {
            System.Text.StringBuilder errorLog = new System.Text.StringBuilder();
            try
            {

                //Get file
                var fileDetails = new FileInfo(file.FileName);
                var fileExtension = fileDetails.Extension;
                int insertedRecs = 0;

                //Save File
                IFormFile formFile = file;
                string filePath = "C:/UPLOADS/" + file.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                    formFile.CopyTo(stream);

                if (file != null)
                    InsertValues(ReadExcel(filePath), ref errorLog, ref insertedRecs);
                else
                    return StatusCode(415, new { File = file.Name, StatusCode = 415, Meaning = "Unsuported meadia type, nothing has been uploaded. Review your file and attempt again" });

                //If successful, upload the file to the DB then delete the file from the Server
                if (insertedRecs > 0)
                    Cleanup(fileDetails, filePath, ref errorLog);

            }
            catch (Exception a) { errorLog.Append(a); }

            if (errorLog.Length != 0)
                return StatusCode(500, new { StatusCode = 500, Meaning = "Internal Server Error - An error has occrured, your spreadsheet has not been uploaded, please review it and attempt again ", DetailedError = errorLog.ToString() });
            else
                return StatusCode(200, new { StatusCode = 200, Meaning = "OK - Everything is fine and your data has been ingested without issue" });
        }
        
        private void InsertValues(DataTable values, ref System.Text.StringBuilder errorLog, ref int insertedCounter)
        {
            #region Header stuff
            //Check Headers and ensure that all the mandatory headers are present, if not then error returned

            string[] mandatoryFields = new string[] {
                                                        "ProjectTitle (ProjectDetails)",
                                                        "Description (ProjectDetails)",
                                                        "Link (ProjectDetails)",
                                                        "StartYear (ProjectDetails)",
                                                        "EndYear (ProjectDetails)",
                                                        "ProjectStatus (ProjectDetails)",
                                                        "BudgetRange (ProjectDetails)",
                                                        "Province (ProjectLocation)",
                                                        "DistrictMunicipality (ProjectLocation)",
                                                        "LocalMunicipalityMetros (ProjectLocation)",  
                                                        "Locations (ProjectLocation)",
                                                        "LeadOrganisation (ProjectManager)",
                                                        "Name (ProjectManager)",
                                                        "Organisation (ProjectManager)",
                                                        "PhoneNumber (ProjectManager)",
                                                        "Email (ProjectManager)",
                                                        "HostOrganisation (ProjectManager)",
                                                        "HostPartner (ProjectManager)",
                                                        "AltContact (ProjectManager)",
                                                        "AltContactEmail (ProjectManager)",
                                                        "AddFunding (ActionOverview)",
                                                        "AddAdaptationApplied (ActionOverview)",
                                                        "AddAdaptationResearch (ActionOverview)",
                                                        "GrantProgramName (Funding)",
                                                        "FundingAgency (Funding)",
                                                        "PartneringDepartmentsOrganisations (Funding)",
                                                        "ProjectCoordinator (Funding)",
                                                        "TotalBudget (Funding)",
                                                        "AnnualBudget (Funding)",
                                                        "FundingStatus (Funding)",
                                                        "Title (AdaptationDetails)",
                                                        "Description (AdaptationDetails)",
                                                        "Purpose (AdaptationDetails)",
                                                        "Sector (AdaptationDetails)",
                                                        "ProjectStatus (AdaptationDetails)",
                                                        "Name (AdaptationContact)",
                                                        "EmailAddress (AdaptationContact)",
                                                        "Author (ResearchDetails)",
                                                        "PaperLink (ResearchDetails)",
                                                        "ResearchType (ResearchDetails)",
                                                        "TargetAudience (ResearchDetails)",
                                                        "ResearchMaturity (ResearchDetails)",  
                                                        "Other (MitigationDetails)",
                                                        "OtherDescription (MitigationDetails)",
                                                        "CDMProjectNumber (MitigationDetails)",
                                                        "CarbonCredit (MitigationDetails)",
                                                        "CarbonCreditMarket (MitigationDetails)",
                                                        "CDMStatus (MitigationDetails)",
                                                        "CDMMethodology (MitigationDetails)",
                                                        "VoluntaryMethodology (MitigationDetails)",
                                                        "VoluntaryGoldStandard (MitigationDetails)",
                                                        "Sector (MitigationDetails)",
                                                        "Year (EmmissionsData)",
                                                        "C02 (EmmissionsData)",
                                                        "CH4 (EmmissionsData)",
                                                        "CH4_CO2e (EmmissionsData)",
                                                        "N2O (EmmissionsData)",
                                                        "N2O_CO2e (EmmissionsData)",
                                                        "HFC (EmmissionsData)",
                                                        "HFC_CO2e (EmmissionsData)",
                                                        "PFC (EmmissionsData)",
                                                        "PFC_CO2e (EmmissionsData)",
                                                        "SF6 (EmmissionsData)",
                                                        "SF6_CO2e (EmmissionsData)",
                                                        "Hydro (EmmissionsData)",
                                                        "Hydro_CO2e (EmmissionsData)",
                                                        "Tidal (EmmissionsData)",
                                                        "Tidal_CO2e (EmmissionsData)",
                                                        "Wind (EmmissionsData)",
                                                        "Wind_CO2e (EmmissionsData)",
                                                        "Solar (EmmissionsData)",
                                                        "Solar_CO2e (EmmissionsData)",
                                                        "FossilFuelElecRed (EmmissionsData)",
                                                        "FossilFuelElecRed_CO2e (EmmissionsData)",
                                                        "BioWaste (EmmissionsData)",
                                                        "BioWaste_CO2e (EmmissionsData)",
                                                        "Geothermal (EmmissionsData)",
                                                        "Geothermal_CO2e (EmmissionsData)"
                                                    };

            #endregion

            #region Seperate Data
            //Remove the descriptor rows so we only process the data on the first line 
            //and remove all empty rows on the second (so we dont insert blank data and know where the input stops)
            values.Rows.Cast<DataRow>().Where(x => new string[] { "Importance", "Tooltip", "Entry Type" }.Contains(x["Field"].ToString())).ToList<DataRow>().ForEach(x => values.Rows.Remove(x));
            mandatoryFields.ToList().ForEach(x => values.Rows.Cast<DataRow>().ToList().Where(row => row[x].ToString() == "" || row[x] == null).ToList<DataRow>().ForEach(dr => values.Rows.Remove(dr)));

            List<DataRow> duplicates = new List<DataRow>();

            foreach (DataRow row in values.Rows)
            {
                foreach (string dbProject in _context.Project.ToList<Project>().Select(c => c.ProjectTitle))
                {
                    if (dbProject == row["ProjectTitle (ProjectDetails)"].ToString())
                        duplicates.Add(row);
                }
            }

            duplicates.ForEach(x => values.Rows.Remove(x));

            #endregion

            for (int rowCounter = 0; rowCounter < values.Rows.Count; rowCounter++)
            {
                Project IndividualRecord = new Project();
                ProjectStatus projectStatusLookup = new ProjectStatus();
                Person projectManagerLookup = new Person();
                List<ProjectLocation> locationSectionLookup = new List<ProjectLocation>();
                ResearchDetail researchDetailLookup = new ResearchDetail
                {
                    Author = values.Rows[rowCounter]["Author (ResearchDetails)"].ToString(),
                    PaperLink = values.Rows[rowCounter]["PaperLink (ResearchDetails)"].ToString(),
                    ResearchType = new ResearchType { Value = values.Rows[rowCounter]["ResearchType (ResearchDetails)"].ToString() },
                    TargetAudience = new TargetAudience { Value = values.Rows[rowCounter]["TargetAudience (ResearchDetails)"].ToString() },
                    ResearchMaturity = new ResearchMaturity { Value = values.Rows[rowCounter]["ResearchMaturity (ResearchDetails)"].ToString() }
                };
                CarbonCredit carbonCreditLookup = new CarbonCredit();
                CarbonCreditMarket carbonCreditMarketLookup = new CarbonCreditMarket();
                CDMStatus cdmStatusLookup = new CDMStatus();
                CDMMethodology cdmMethodologyLookup = new CDMMethodology();
                VoluntaryMethodology voluntaryMethodologyLookup = new VoluntaryMethodology();
                VoluntaryGoldStandard voluntaryGoldStandardLookup = new VoluntaryGoldStandard();
                AdaptationDetail adaptationDetailLookup = new AdaptationDetail();

                #region Lazy loading lookups

                projectStatusLookup = _context.ProjectStatus.ToList<ProjectStatus>().Where(x => x.Value == values.Rows[rowCounter]["ProjectStatus (ProjectDetails)"].ToString().Split(':')[0]).FirstOrDefault();
                projectManagerLookup = _context.Person.ToList<Person>().Where(person => person.EmailAddress == values.Rows[rowCounter]["Email (ProjectManager)"].ToString() && person.FirstName == values.Rows[rowCounter]["Name (ProjectManager)"].ToString().Split(' ')[0]).FirstOrDefault();

                #region Project Location
                List<Location> locationList = new List<Location>();
                
                if (values.Rows[rowCounter]["Locations (ProjectLocation)"].ToString().Contains(";"))
                {
                    //If it contains a semicolon then its multiple locations
                    foreach (string coordinate in values.Rows[rowCounter]["Locations (ProjectLocation)"].ToString().Split(";"))
                    {
                        locationList.Add(new Location
                        {
                            LatCalculated = double.Parse(coordinate.Split('.')[0]),
                            LonCalculated = double.Parse(coordinate.Split('.')[1])
                        });
                    }
                }
                else
                {
                    //If there's no semicolon then its a sinlge location 
                    locationList.Add(new Location
                    {
                        LatCalculated = double.Parse(values.Rows[rowCounter]["Locations (ProjectLocation)"].ToString().Split('.')[0]),
                        LonCalculated = double.Parse(values.Rows[rowCounter]["Locations (ProjectLocation)"].ToString().Split('.')[1])
                    });
                }
                #endregion 

                adaptationDetailLookup = new AdaptationDetail
                {
                    Title = values.Rows[rowCounter]["Title (AdaptationDetails)"].ToString(),
                    Description = values.Rows[rowCounter]["Description (AdaptationDetails)"].ToString(),
                    AdaptationPurpose = _context.AdaptationPurpose.ToList<AdaptationPurpose>().Where(x => x.Value == values.Rows[rowCounter]["Purpose (AdaptationDetails)"].ToString()).FirstOrDefault(),
                    AdaptationPurposeId = _context.AdaptationPurpose.ToList<AdaptationPurpose>().Where(x => x.Value == values.Rows[rowCounter]["Purpose (AdaptationDetails)"].ToString()).FirstOrDefault().AdaptationPurposeId,
                    SectorId = FindSectorId(values.Rows[rowCounter]["Sector (AdaptationDetails)"].ToString()),
                    ProjectStatus = _context.ProjectStatus.ToList<ProjectStatus>().Where(x => x.Value == values.Rows[rowCounter]["ProjectStatus (AdaptationDetails)"].ToString()).FirstOrDefault(),
                    ContactName = values.Rows[rowCounter]["Name (AdaptationContact)"].ToString(),
                    ContactEmail = values.Rows[rowCounter]["EmailAddress (AdaptationContact)"].ToString()
                };

                projectManagerLookup = projectManagerLookup == null ? new Person
                {
                    FirstName = values.Rows[rowCounter]["Name (ProjectManager)"].ToString().Split(' ')[0],
                    Surname = values.Rows[rowCounter]["Name (ProjectManager)"].ToString().Split(' ').ToList().Skip(1).Aggregate((i, j) => i + " " + j),
                    Organisation = values.Rows[rowCounter]["Organisation (ProjectManager)"].ToString(),
                    PhoneNumber = values.Rows[rowCounter]["PhoneNumber (ProjectManager)"].ToString(),
                    EmailAddress = values.Rows[rowCounter]["Email (ProjectManager)"].ToString()
                } : projectManagerLookup;

                carbonCreditLookup = _context.CarbonCredit.ToList<CarbonCredit>().Where(rec => rec.Value == values.Rows[rowCounter]["CarbonCredit (MitigationDetails)"].ToString().ToLower()).FirstOrDefault();
                carbonCreditMarketLookup = _context.CarbonCreditMarket.ToList<CarbonCreditMarket>().Where(rec => rec.Value == values.Rows[rowCounter]["CarbonCreditMarket (MitigationDetails)"].ToString()).FirstOrDefault();
                cdmStatusLookup = _context.CDMStatus.ToList<CDMStatus>().Where(rec => rec.Value == values.Rows[rowCounter]["CDMStatus (MitigationDetails)"].ToString()).FirstOrDefault();
                cdmMethodologyLookup = _context.CDMMethodology.ToList<CDMMethodology>().Where(rec => rec.Value == values.Rows[rowCounter]["CDMMethodology (MitigationDetails)"].ToString()).FirstOrDefault();
                voluntaryMethodologyLookup = _context.VoluntaryMethodology.ToList<VoluntaryMethodology>().Where(rec => rec.Value == values.Rows[rowCounter]["VoluntaryMethodology (MitigationDetails)"].ToString()).FirstOrDefault();
                voluntaryGoldStandardLookup = _context.VoluntaryGoldStandard.ToList<VoluntaryGoldStandard>().Where(rec => rec.Value == values.Rows[rowCounter]["VoluntaryGoldStandard (MitigationDetails)"].ToString()).FirstOrDefault();

                #endregion

                #region Project Details
                //ProjectDetails
                IndividualRecord.ProjectId = 0;
                IndividualRecord.ProjectTitle = values.Rows[rowCounter]["ProjectTitle (ProjectDetails)"].ToString();
                IndividualRecord.ProjectDescription = values.Rows[rowCounter]["Description (ProjectDetails)"].ToString();
                IndividualRecord.Link = values.Rows[rowCounter]["Link (ProjectDetails)"].ToString();
                IndividualRecord.StartYear = int.Parse(values.Rows[rowCounter]["StartYear (ProjectDetails)"].ToString());
                IndividualRecord.EndYear = int.Parse(values.Rows[rowCounter]["EndYear (ProjectDetails)"].ToString());
                IndividualRecord.ProjectStatus = projectStatusLookup == null ? new ProjectStatus { Value = values.Rows[rowCounter]["ProjectStatus (ProjectDetails)"].ToString() } : projectStatusLookup;
                IndividualRecord.LeadAgent = "";
                IndividualRecord.BudgetLower = decimal.Parse(values.Rows[rowCounter]["BudgetRange (ProjectDetails)"].ToString().Split('-')[0].Replace(",", "").Trim());
                IndividualRecord.BudgetUpper = decimal.Parse(values.Rows[rowCounter]["BudgetRange (ProjectDetails)"].ToString().Split('-')[1].Replace(",", "").Trim());

                #endregion

                #region Project Manager

                //ProjectManager
                IndividualRecord.HostOrganisation = values.Rows[rowCounter]["LeadOrganisation (ProjectManager)"].ToString();
                IndividualRecord.ProjectManager = projectManagerLookup;
                IndividualRecord.HostOrganisation = values.Rows[rowCounter]["HostOrganisation (ProjectManager)"].ToString();
                IndividualRecord.HostPartner = values.Rows[rowCounter]["HostPartner (ProjectManager)"].ToString();
                IndividualRecord.AlternativeContact = values.Rows[rowCounter]["AltContact (ProjectManager)"].ToString();
                IndividualRecord.AlternativeContactEmail = values.Rows[rowCounter]["AltContactEmail (ProjectManager)"].ToString();

                #endregion

                #region Funding

                Person projectFunderPerson = _context.Person.ToList<Person>().Where(person =>
                                                    person.FirstName == values.Rows[rowCounter]["ProjectCoordinator (Funding)"].ToString().Split(' ')[0].ToString() &&
                                                    person.Surname == values.Rows[rowCounter]["ProjectCoordinator (Funding)"].ToString().Split(' ').ToList().Skip(1).Aggregate((i, j) => i + " " + j)).FirstOrDefault();
                if (projectFunderPerson == null)
                    projectFunderPerson = new Person
                    {
                        FirstName = values.Rows[rowCounter]["ProjectCoordinator (Funding)"].ToString().Split(' ')[0].ToString(),
                        Surname = values.Rows[rowCounter]["ProjectCoordinator (Funding)"].ToString().Split(' ').ToList().Skip(1).Aggregate((i, j) => i + " " + j),
                        EmailAddress = " ",
                        Organisation = " "
                    };

                ICollection<ProjectFunder> projectFunders = new List<ProjectFunder>
                {
                    new ProjectFunder
                    {
                        Funder = new Funder {
                                                GrantProgName = values.Rows[rowCounter]["GrantProgramName (Funding)"].ToString(),
                                                FundingAgency = values.Rows[rowCounter]["FundingAgency (Funding)"].ToString(),
                                                PartnerDepsOrgs = values.Rows[rowCounter]["PartneringDepartmentsOrganisations (Funding)"].ToString(),
                                                ProjectCoordinator = projectFunderPerson,
                                                TotalBudget = decimal.Parse(values.Rows[rowCounter]["TotalBudget (Funding)"].ToString()),
                                                AnnualBudget = decimal.Parse(values.Rows[rowCounter]["AnnualBudget (Funding)"].ToString()),
                                                FundingStatusId = _context.FundingStatus.ToList<FundingStatus>().Where(x => x.Value == values.Rows[rowCounter]["FundingStatus (Funding)"].ToString()).FirstOrDefault().FundingStatusId
                                            },
                    }
                };
                IndividualRecord.ProjectFunders = projectFunders;

                #endregion

                #region AdaptationContact

                //AdaptationContact
                ICollection<AdaptationDetail> adaptationDetails = new List<AdaptationDetail>
                {
                    new AdaptationDetail
                    {
                        ContactName = values.Rows[rowCounter]["Name (AdaptationContact)"].ToString(),
                        ContactEmail = values.Rows[rowCounter]["EmailAddress (AdaptationContact)"].ToString()
                    }
                };

                #endregion

                #region ResearchDetails

                ICollection<ResearchDetail> researchDetail = new List<ResearchDetail>
                {
                    researchDetailLookup
                };

                IndividualRecord.ResearchDetails = researchDetail;

                #endregion

                #region MitigationDetails

                MitigationDetail mitigationDetailLookup = new MitigationDetail();

                //Removed from Excel templpate 
                mitigationDetailLookup.Other = int.Parse(values.Rows[rowCounter]["Other (MitigationDetails)"].ToString());
                mitigationDetailLookup.OtherDescription = values.Rows[rowCounter]["OtherDescription (MitigationDetails)"].ToString();
                mitigationDetailLookup.CDMProjectNumber = values.Rows[rowCounter]["CDMProjectNumber (MitigationDetails)"].ToString();
                mitigationDetailLookup.CarbonCredit = carbonCreditLookup;
                mitigationDetailLookup.CarbonCreditMarket = carbonCreditMarketLookup;
                mitigationDetailLookup.CDMStatus = cdmStatusLookup;
                mitigationDetailLookup.CDMMethodology = cdmMethodologyLookup;
                mitigationDetailLookup.VoluntaryMethodology = voluntaryMethodologyLookup;
                mitigationDetailLookup.VoluntaryGoldStandard = voluntaryGoldStandardLookup;
                mitigationDetailLookup.SectorId = _context.MitigationSector.Where(x => x.Description == values.Rows[rowCounter]["Sector (MitigationDetails)"].ToString()).FirstOrDefault().MitigationSectorId;
                mitigationDetailLookup.ResearchDetail = researchDetailLookup;

                #endregion

                #region EmissionsData

                MitigationEmissionsData mitigationEmissionsData = new MitigationEmissionsData();

                mitigationEmissionsData.Year = int.Parse(values.Rows[rowCounter]["Year (EmmissionsData)"].ToString());
                mitigationEmissionsData.CO2 = double.Parse(values.Rows[rowCounter]["C02 (EmmissionsData)"].ToString());
                mitigationEmissionsData.CH4 = double.Parse(values.Rows[rowCounter]["CH4 (EmmissionsData)"].ToString());
                mitigationEmissionsData.CH4_CO2e = double.Parse(values.Rows[rowCounter]["CH4_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.N2O = double.Parse(values.Rows[rowCounter]["N2O (EmmissionsData)"].ToString());
                mitigationEmissionsData.N2O_CO2e = double.Parse(values.Rows[rowCounter]["N2O_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.HFC = double.Parse(values.Rows[rowCounter]["HFC (EmmissionsData)"].ToString());
                mitigationEmissionsData.HFC_CO2e = double.Parse(values.Rows[rowCounter]["HFC_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.PFC = double.Parse(values.Rows[rowCounter]["PFC (EmmissionsData)"].ToString());
                mitigationEmissionsData.PFC_CO2e = double.Parse(values.Rows[rowCounter]["PFC_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.SF6 = double.Parse(values.Rows[rowCounter]["SF6 (EmmissionsData)"].ToString());
                mitigationEmissionsData.SF6_CO2e = double.Parse(values.Rows[rowCounter]["SF6_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.Hydro = double.Parse(values.Rows[rowCounter]["Hydro (EmmissionsData)"].ToString());
                mitigationEmissionsData.Hydro_CO2e = double.Parse(values.Rows[rowCounter]["Hydro_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.Tidal = double.Parse(values.Rows[rowCounter]["Tidal (EmmissionsData)"].ToString());
                mitigationEmissionsData.Tidal_CO2e = double.Parse(values.Rows[rowCounter]["Tidal_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.Wind = double.Parse(values.Rows[rowCounter]["Wind (EmmissionsData)"].ToString());
                mitigationEmissionsData.Wind_CO2e = double.Parse(values.Rows[rowCounter]["Wind_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.Solar = double.Parse(values.Rows[rowCounter]["Solar (EmmissionsData)"].ToString());
                mitigationEmissionsData.Solar_CO2e = double.Parse(values.Rows[rowCounter]["Solar_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.FossilFuelElecRed = double.Parse(values.Rows[rowCounter]["FossilFuelElecRed (EmmissionsData)"].ToString());
                mitigationEmissionsData.FossilFuelElecRed_CO2e = double.Parse(values.Rows[rowCounter]["FossilFuelElecRed_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.BioWaste = double.Parse(values.Rows[rowCounter]["BioWaste (EmmissionsData)"].ToString());
                mitigationEmissionsData.BioWaste_CO2e = double.Parse(values.Rows[rowCounter]["BioWaste_CO2e (EmmissionsData)"].ToString());
                mitigationEmissionsData.Geothermal = double.Parse(values.Rows[rowCounter]["Geothermal (EmmissionsData)"].ToString());
                mitigationEmissionsData.Geothermal_CO2e = double.Parse(values.Rows[rowCounter]["Geothermal_CO2e (EmmissionsData)"].ToString());

                #endregion

                int projectId = new ProjectsController(_context, _config).BulkUpload(IndividualRecord, ref errorLog);

                if (projectId > 0)
                {
                    mitigationEmissionsData.ProjectId = projectId;
                    mitigationDetailLookup.ProjectId = projectId;
                    mitigationDetailLookup.ProjectStatusId = int.Parse(IndividualRecord.ProjectStatusId.ToString());
                    adaptationDetailLookup.ProjectId = projectId;
                    adaptationDetailLookup.ProjectStatusId = int.Parse(IndividualRecord.ProjectStatusId.ToString());
                    
                    foreach (Location location in locationList)
                    {
                        locationSectionLookup.Add(new ProjectLocation
                        {
                            Location = location,
                            ProjectId = projectId
                        });
                    }

                    _context.ProjectLocation.AddRange(locationSectionLookup);

                    _context.MitigationEmissionsData.Add(mitigationEmissionsData);
                    _context.MitigationDetails.Add(mitigationDetailLookup);
                    _context.AdaptationDetails.Add(adaptationDetailLookup);

                    try
                    {
                        _context.SaveChanges();
                        insertedCounter++;
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }

                var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                response.Content = new System.Net.Http.StringContent(errorLog.ToString());
            }
        }

        private DataTable ReadExcel(string fileName)
        {
            string extention = "xlsx";
            int failed = 0;
            try { extention = Path.GetFileName(fileName).Split(".")[1]; }
            catch (Exception e) { failed = 1; }
            finally { extention = failed == 1 ? extention = "xlsx" : extention; }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            DataSet dsData = null;
            FileStream stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = null;

            try
            {
                if (extention.Equals("xls"))//1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                else //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                excelReader.IsFirstRowAsColumnNames = true;
                dsData = excelReader.AsDataSet();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                try
                {
                    if (excelReader != null)
                        excelReader.Close();
                }
                catch (Exception e) { throw e; }
            }

            return dsData.Tables["Template"];
        }

        private void Cleanup(FileInfo fileDetails, string localFilePath, ref System.Text.StringBuilder errorLog)
        {
            //Check if file was saved
            if (System.IO.File.Exists(localFilePath))
            {
                new BulkUploadFileController(this._context, this._config).BulkUploadInsert(fileDetails, localFilePath, ref errorLog);

                //Remove the locally saved file
                if (System.IO.File.Exists(fileDetails.FullName))
                    System.IO.File.Delete(fileDetails.FullName);
            }
        }

        [HttpDelete]
        [ODataRoute("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(int id)
        {
            return await new BulkUploadFileController(this._context, this._config).Delete(id);
        }

        private int FindSectorId(string sector)
        {
            Dictionary<int, string> SectorList = new Dictionary<int, string> {
                { 1, "Agriculture, Forestry, Fishing" },
                { 2, "Construction" },
                { 3, "Finance, Insurance, Real Estate" },
                { 4, "Manufacturing" },
                { 5, "Mining" },
                { 6, "Public Administration" },
                { 7, "Retail Trade" },
                { 8, "Services" },
                { 9, "Transportation & Public Utilities" },
                { 10, "Wholesale Trade" },
                { 11, "Administration of Economic Programs" },
                { 12, "Administration of Environmental Quality and Housing Programs" },
                { 13, "Administration of Human Resource Programs" },
                { 14, "Agricultural Production - Crops" },
                { 15, "Agricultural Production - Livestock" },
                { 16, "Agricultural Services" },
                { 17, "Amusement and Recreation Services" },
                { 18, "Apparel and Accessory Stores" },
                { 19, "Apparel and other Finished Products Made from Fabrics and Similar Materials" },
                { 20, "Automotive Dealers and Gasoline Service Stations" },
                { 21, "Automotive Repair, Services, and Parking" },
                { 22, "Bituminous Coal and Lignite Mining" },
                { 23, "Building Construction General Contractors and Operative Builders" },
                { 24, "Building Materials, Hardware, Garden Supply, and Mobile Home Dealers" },
                { 25, "Business Services" },
                { 26, "Chemicals and Allied Products" },
                { 27, "Communications" },
                { 28, "Construction Special Trade Contractors" },
                { 29, "Depository Institutions" },
                { 30, "Eating and Drinking Places" },
                { 31, "Educational Services" },
                { 32, "Electric, Gas and Sanitary Services" },
                { 33, "Electronic and other Electrical Equipment and Components, except Computer Equipment" },
                { 34, "Engineering, Accounting, Research, Management, and Related Services" },
                { 35, "Executive, Legislative, and General Government, except Finance" },
                { 36, "Fabricated Metal Products, except Machinery and Transportation Equipment" },
                { 37, "Fishing, Hunting and Trapping" },
                { 38, "Food and Kindred Products" },
                { 39, "Food Stores" },
                { 40, "Forestry" },
                { 41, "Furniture and Fixtures" },
                { 42, "General Merchandise Stores" },
                { 43, "Health Services" },
                { 44, "Heavy Construction other than Building Construction Contractors" },
                { 45, "Holding and other Investment Offices" },
                { 46, "Home Furniture, Furnishings, and Equipment Stores" },
                { 47, "Hotels, Rooming Houses, Camps, and other Lodging Places" },
                { 48, "Industrial and Commercial Machinery and Computer Equipment" },
                { 49, "Insurance Agents, Brokers and Service" },
                { 50, "Insurance Carriers" },
                { 51, "Justice, Public Order, and Safety" },
                { 52, "Leather and Leather Products" },
                { 53, "Legal Services" },
                { 54, "Local and Suburban Transit and Interurban Highway Passenger Transportation" },
                { 55, "Lumber and Wood Products, except Furniture" },
                { 56, "Measuring, Analyzing, and Controlling Instruments; Photographic, Medical and Optical Goods; Watches and Clocks" },
                { 57, "Membership Organizations" },
                { 58, "Metal Mining" },
                { 59, "Mining and Quarrying of Nonmetallic Minerals, except Fuels" },
                { 60, "Miscellaneous Manufacturing Industries" },
                { 61, "Miscellaneous Repair Services" },
                { 62, "Miscellaneous Retail" },
                { 63, "Miscellaneous Services" },
                { 64, "Motion Pictures" },
                { 65, "Motor Freight Transportation and Warehousing" },
                { 66, "Museums, Art Galleries, and Botanical and Zoological Gardens" },
                { 67, "National Security and International Affairs" },
                { 68, "Non-Depository Credit Institutions" },
                { 69, "Nonclassifiable Establishments" },
                { 70, "Oil and Gas Extraction" },
                { 71, "Paper and Allied Products" },
                { 72, "Personal Services" },
                { 73, "Petroleum Refining and Related Industries" },
                { 74, "Pipelines, except Natural Gas" },
                { 75, "Primary Metal Industries" },
                { 76, "Printing, Publishing, and Allied Industries" },
                { 77, "Private Households" },
                { 78, "Public Finance, Taxation, and Monetary Policy" },
                { 79, "Railroad Transportation" },
                { 80, "Real Estate" },
                { 81, "Rubber and Miscellaneous Plastics Products" },
                { 82, "Security and Commodity Brokers, Dealers, Exchanges, and Services" },
                { 83, "Social Services" },
                { 84, "Stone, Clay, Glass, and Concrete Products" },
                { 85, "Textile Mill Products" },
                { 86, "Tobacco Products" },
                { 87, "Transportation by Air" },
                { 88, "Transportation Equipment" },
                { 89, "Transportation Services" },
                { 90, "Water Transportation" },
                { 91, "Wholesale Trade-Durable Goods" },
                { 92, "Wholesale Trade-Nondurable Goods" },
                { 93, "17 Terminal and Service Facilities for Motor Vehicle" },
                { 94, "67 Converted Paper and Paperboard Products, except Containers and Boxes" },
                { 95, "73 Radio, Television, Consumer Electronics, and Music Stores" },
                { 96, "79 Service Industries for the Printing Trade" },
                { 97, "Abrasive, Asbestos, and Miscellaneous" },
                { 98, "Accident and Health Insurance and Medical" },
                { 99, "Accounting, Auditing, and Bookkeeping Services" },
                { 100, "Administration of Educational Programs" },
                { 101, "Administration of Environmental Quality" },
                { 102, "Administration of General Economic Programs" },
                { 103, "Administration of Housing and Urban Planning" },
                { 104, "Administration of Public Health Programs" },
                { 105, "Administration of Social, Human Resource and Income Maintenance Programs" },
                { 106, "Administration of Veterans' Affairs, except Health and Insurance" },
                { 107, "Advertising" },
                { 108, "Agricultural Chemicals" },
                { 109, "Air Transportation, Nonscheduled" },
                { 110, "Air Transportation, Scheduled, and Air Courier" },
                { 111, "Aircraft and Parts" },
                { 112, "Airports, Flying Fields, and Airport" },
                { 113, "Animal Services, except Veterinary" },
                { 114, "Animal Specialties" },
                { 115, "Anthracite Mining" },
                { 116, "Apparel, Piece Goods, and Notions" },
                { 117, "Arboreta and Botanical or Zoological Gardens" },
                { 118, "Arrangement of Passenger Transportation" },
                { 119, "Arrangement of Transportation of Freight and Cargo" },
                { 120, "Asphalt Paving and Roofing Materials" },
                { 121, "Auto and Home Supply Stores" },
                { 122, "Automobile Parking" },
                { 123, "Automotive Dealers, not elsewhere classified" },
                { 124, "Automotive Rental and Leasing, without Drivers" },
                { 125, "Automotive Repair Shops" },
                { 126, "Automotive Services, except Repair" },
                { 127, "Bakery Products" },
                { 128, "Barber Shops" },
                { 129, "Beauty Shops" },
                { 130, "Beer, Wine, and Distilled Alcoholic Beverages" },
                { 131, "Beverages" },
                { 132, "Bituminous Coal and Lignite Mining" },
                { 133, "Blankbooks, Looseleaf Binders, and Bookbinding" },
                { 134, "Boat Dealers" },
                { 135, "Books" },
                { 136, "Boot and Shoe Cut Stock and Findings" },
                { 137, "Bowling Centers" },
                { 138, "Broadwoven Fabric Mills, Cotton" },
                { 139, "Broadwoven Fabric Mills, Manmade Fiber and Silk" },
                { 140, "Broadwoven Fabric Mills, Wool (including Dyeing and Finishing)" },
                { 141, "Bus Charter Service" },
                { 142, "Business Associations" },
                { 143, "Business Credit Institutions" },
                { 144, "Cable and other Pay Television Services" },
                { 145, "Camps and Recreational Vehicle Parks" },
                { 146, "Candy, Nut, and Confectionery Stores" },
                { 147, "Canned, Frozen, and Preserved Fruits, Vegetables, and Food Specialties" },
                { 148, "Carpentry and Floor Work" },
                { 149, "Carpets and Rugs" },
                { 150, "Cash Grains" },
                { 151, "Cement, Hydraulic" },
                { 152, "Central Reserve Depository Institutions" },
                { 153, "Chemical and Fertilizer Mineral Mining" },
                { 154, "Chemicals and Allied Products" },
                { 155, "Chewing and Smoking Tobacco and Snuff" },
                { 156, "Child Day Care Services" },
                { 157, "Children's and Infants' Wear Stores" },
                { 158, "Cigarettes" },
                { 159, "Cigars" },
                { 160, "Civic, Social, and Fraternal Associations" },
                { 161, "Clay, Ceramic, and Refractory Minerals" },
                { 162, "Coal Mining Services" },
                { 163, "Coating, Engraving, and Allied Services" },
                { 164, "Colleges, Universities, Professional Schools, Junior Colleges, and Technical Institutes" },
                { 165, "Combination Electric and Gas, and other Utility" },
                { 166, "Commercial Banks" },
                { 167, "Commercial Fishing" },
                { 168, "Commercial Printing" },
                { 169, "Commercial Sports" },
                { 170, "Commodity Contracts Brokers and Dealers" },
                { 171, "Communication Services, not elsewhere classified" },
                { 172, "Communications Equipment" },
                { 173, "Computer and Office Equipment" },
                { 174, "Computer Programming, Data Processing, and other Computer Related Services" },
                { 175, "Concrete Work" },
                { 176, "Concrete, Gypsum, and Plaster Products" },
                { 177, "Construction, Mining, and Materials Handling" },
                { 178, "Consumer Credit Reporting Agencies, Mercantile" },
                { 179, "Copper Ores" },
                { 180, "Costume Jewelry, Costume Novelties, Buttons, and Miscellaneous Notions, except Precious Metal" },
                { 181, "Courts" },
                { 182, "Credit Unions" },
                { 183, "Crop Services" },
                { 184, "Crude Petroleum and Natural Gas" },
                { 185, "Crushed and Broken Stone, including Riprap" },
                { 186, "Cut Stone and Stone Products" },
                { 187, "Cutlery, Hand Tools, and General Hardware" },
                { 188, "Dairy Farms" },
                { 189, "Dairy Products" },
                { 190, "Dairy Products Stores" },
                { 191, "Dance Studios, Schools, and Halls" },
                { 192, "Deep Sea Domestic Transportation of Freight" },
                { 193, "Deep Sea Foreign Transportation of Freight" },
                { 194, "Department Stores" },
                { 195, "Dimension Stone" },
                { 196, "Dolls, Toys, Games and Sporting and Athletic" },
                { 197, "Drug Stores and Proprietary Stores" },
                { 198, "Drugs" },
                { 199, "Drugs, Drug Proprietaries, and Druggists' Sundries" },
                { 200, "Dyeing and Finishing Textiles, except Wool Fabrics" },
                { 201, "Eating and Drinking Places" },
                { 202, "Electric Lighting and Wiring Equipment" },
                { 203, "Electric Services" },
                { 204, "Electric Transmission and Distribution Equipment" },
                { 205, "Electrical Goods" },
                { 206, "Electrical Industrial Apparatus" },
                { 207, "Electrical Repair Shops" },
                { 208, "Electrical Work" },
                { 209, "Electronic Components and Accessories" },
                { 210, "Elementary and Secondary Schools" },
                { 211, "Engineering, Architectural, and Surveying" },
                { 212, "Engines and Turbines" },
                { 213, "Executive and Legislative Offices Combined" },
                { 214, "Executive Offices" },
                { 215, "Fabricated Rubber Products, not elsewhere classified" },
                { 216, "Fabricated Structural Metal Products" },
                { 217, "Family Clothing Stores" },
                { 218, "Farm and Garden Machinery and Equipment" },
                { 219, "Farm Labor and Management Services" },
                { 220, "Farm-Product Raw Materials" },
                { 221, "Fats and Oils" },
                { 222, "Federal and Federally-Sponsored Credit Agencies" },
                { 223, "Ferroalloy Ores, except Vanadium" },
                { 224, "Field Crops, except Cash Grains" },
                { 225, "Fire, Marine, and Casualty Insurance" },
                { 226, "Fish Hatcheries and Preserves" },
                { 227, "Flat Glass" },
                { 228, "Footwear, Except Rubber" },
                { 229, "Foreign Banking and Branches and Agencies of Foreign Banks" },
                { 230, "Forest Nurseries and Gathering of Forest Products" },
                { 231, "Forestry Services" },
                { 232, "Freight Transportation on the Great Lakes-St. Lawrence Seaway" },
                { 233, "Fruit Stores and Vegetable Markets" },
                { 234, "Fruits and Tree Nuts" },
                { 235, "Fuel Dealers" },
                { 236, "Functions Related to Depository Bank" },
                { 237, "Funeral Service and Crematories" },
                { 238, "Fur Goods" },
                { 239, "Furniture and Home Furnishings" },
                { 240, "Gas Production and Distribution" },
                { 241, "Gaskets, Packing, and Sealing Devices and Rubber" },
                { 242, "Gasoline Service Stations" },
                { 243, "General Building Contractors-Nonreside" },
                { 244, "General Building Contractors-Residential" },
                { 245, "General Farms, Primarily Crop" },
                { 246, "General Farms, Primarily Livestock" },
                { 247, "General Government, not elsewhere classified" },
                { 248, "General Industrial Machinery and Equipment" },
                { 249, "Girls', Children's, and Infants' Outerwear" },
                { 250, "Glass and Glassware, Pressed or Blown" },
                { 251, "Glass Products, Made of Purchased Glass" },
                { 252, "Gold and Silver Ores" },
                { 253, "Grain Mill Products" },
                { 254, "Greeting Card" },
                { 255, "Groceries and Related Products" },
                { 256, "Grocery Stores" },
                { 257, "Guided Missiles and Space Vehicles and Parts" },
                { 258, "Handbags and other Personal Leather Goods" },
                { 259, "Hardware Stores" },
                { 260, "Hardware, and Plumbing and Heating Equipment" },
                { 261, "Hats, Caps, and Millinery" },
                { 262, "Heating Equipment, except Electric and Warm Air; and Plumbing Fixtures" },
                { 263, "Heavy Construction, except Highway" },
                { 264, "Highway and Street Construction, except Elevated Highways" },
                { 265, "Holding Offices" },
                { 266, "Home Furniture and Furnishings Stores" },
                { 267, "Home Health Care Services" },
                { 268, "Horticultural Specialties" },
                { 269, "Hospitals" },
                { 270, "Hotels and Motels" },
                { 271, "Household Appliance Stores" },
                { 272, "Household Appliances" },
                { 273, "Household Audio and Video Equipment" },
                { 274, "Household Furniture" },
                { 275, "Hunting, Trapping, Game Propagation" },
                { 276, "Individual and Family Social Services" },
                { 277, "Industrial Inorganic Chemicals" },
                { 278, "Industrial Organic Chemicals" },
                { 279, "Insurance Agents, Brokers and Service" },
                { 280, "Insurance Carriers, not elsewhere classified" },
                { 281, "Intercity and Rural Bus Transportation" },
                { 282, "International Affairs" },
                { 283, "Investment Offices" },
                { 284, "Iron and Steel Foundries" },
                { 285, "Iron Ores" },
                { 286, "Irrigation Systems" },
                { 287, "Jewelry, Silverware, and Plated Ware" },
                { 288, "Job Training and Vocational Rehabilitation" },
                { 289, "Knitting Mills" },
                { 290, "Labor Unions and Similar Labor Organizations" },
                { 291, "Laboratory Apparatus and Analytical, Optical, Measuring, and Controlling Instruments" },
                { 292, "Land Subdividers and Developers" },
                { 293, "Landscape and Horticultural Services" },
                { 294, "Laundry, Cleaning, and Garment Services" },
                { 295, "Lead and Zinc Ores" },
                { 296, "Leather Gloves and Mittens" },
                { 297, "Leather Goods, not elsewhere classified" },
                { 298, "Leather Tanning and Finishing" },
                { 299, "Legal Services" },
                { 300, "Legislative Bodies" },
                { 301, "Libraries" },
                { 302, "Life Insurance" },
                { 303, "Liquor Stores" },
                { 304, "Livestock, except Dairy, Poultry, etc." },
                { 305, "Local and Suburban Passenger Transportation" },
                { 306, "Logging" },
                { 307, "Luggage" },
                { 308, "Lumber and other Building Materials Dealers" },
                { 309, "Lumber and other Construction Materials" },
                { 310, "Machinery, Equipment, and Supplies" },
                { 311, "Mailing, Reproduction, Commercial Art and Photography, and Stenographic Services" },
                { 312, "Management and Public Relations Service" },
                { 313, "Manifold Business Forms" },
                { 314, "Masonry, Stonework, Tile Setting, and Plastering" },
                { 315, "Meat and Fish (Seafood) Markets, including Freezer Provisioners" },
                { 316, "Meat Products" },
                { 317, "Medical and Dental Laboratories" },
                { 318, "Membership Organizations, not elsewhere classified" },
                { 319, "Men's and Boys' Clothing and Accessory Stores" },
                { 320, "Men's and Boys' Furnishings, Work Clothing, and Allied Garments" },
                { 321, "Men's and Boys' Suits, Coats, and Overcoats" },
                { 322, "Metal Cans and Shipping Containers" },
                { 323, "Metal Forgings and Stampings" },
                { 324, "Metal Mining Services" },
                { 325, "Metals and Minerals, except Petroleum" },
                { 326, "Metalworking Machinery and Equipment" },
                { 327, "Millwork, Veneer, Plywood, and Structural Wood" },
                { 328, "Miscellaneous Amusement and Recreation" },
                { 329, "Miscellaneous Apparel and Accessories" },
                { 330, "Miscellaneous Apparel and Accessory Stores" },
                { 331, "Miscellaneous Business Services" },
                { 332, "Miscellaneous Chemical Products" },
                { 333, "Miscellaneous Durable Goods" },
                { 334, "Miscellaneous Electrical Machinery, Equipment, and Supplies" },
                { 335, "Miscellaneous Equipment Rental and Leasing" },
                { 336, "Miscellaneous Fabricated Metal Products" },
                { 337, "Miscellaneous Fabricated Textile Products" },
                { 338, "Miscellaneous Food Preparations" },
                { 339, "Miscellaneous Food Stores" },
                { 340, "Miscellaneous Furniture and Fixtures" },
                { 341, "Miscellaneous General Merchandise" },
                { 342, "Miscellaneous Health and Allied Services" },
                { 343, "Miscellaneous Industrial and Commercial" },
                { 344, "Miscellaneous Investing" },
                { 345, "Miscellaneous Manufacturing Industries" },
                { 346, "Miscellaneous Metal Ores" },
                { 347, "Miscellaneous Non-Durable Goods" },
                { 348, "Miscellaneous Nonmetallic Minerals, except" },
                { 349, "Miscellaneous Personal Services" },
                { 350, "Miscellaneous Plastics Products" },
                { 351, "Miscellaneous Primary Metal Products" },
                { 352, "Miscellaneous Products of Petroleum" },
                { 353, "Miscellaneous Publishing" },
                { 354, "Miscellaneous Repair Shops and Repair" },
                { 355, "Miscellaneous Services" },
                { 356, "Miscellaneous Services Incidental" },
                { 357, "Miscellaneous Shopping Goods Stores" },
                { 358, "Miscellaneous Special Trade Contract" },
                { 359, "Miscellaneous Textile Goods" },
                { 360, "Miscellaneous Transportation Equipment" },
                { 361, "Miscellaneous Wood Products" },
                { 362, "Mobile Home Dealers" },
                { 363, "Mortgage Bankers and Brokers" },
                { 364, "Motion Picture Distribution and Allied Services" },
                { 365, "Motion Picture Production and Allied Services" },
                { 366, "Motion Picture Theaters" },
                { 367, "Motor Vehicle Dealers (New and Used)" },
                { 368, "Motor Vehicle Dealers (Used Only)" },
                { 369, "Motor Vehicles and Motor Vehicle Equipment" },
                { 370, "Motor Vehicles and Motor Vehicle Parts and Supplies" },
                { 371, "Motorcycle Dealers" },
                { 372, "Motorcycles, Bicycles, and Parts" },
                { 373, "Museums and Art Galleries" },
                { 374, "Musical Instruments" },
                { 375, "Narrow Fabric and other Smallwares Mills, Cotton, Wool, Silk, and Manmade Fiber" },
                { 376, "National Security" },
                { 377, "Natural Gas Liquids" },
                { 378, "Newspapers: Publishing, or Publishing and Printing" },
                { 379, "Nonclassifiable Establishments" },
                { 380, "Nonferrous Foundries (Castings)" },
                { 381, "Nonmetallic Minerals Services, except Fuels" },
                { 382, "Nonstore Retailers" },
                { 383, "Nursing and Personal Care Facilities" },
                { 384, "Office Furniture" },
                { 385, "Offices and Clinics of Dentists" },
                { 386, "Offices and Clinics of Doctors of Medicine" },
                { 387, "Offices and Clinics of Doctors of Osteopathy" },
                { 388, "Offices and Clinics of other Health Practitioners" },
                { 389, "Oil and Gas Field Services" },
                { 390, "Operative Builders" },
                { 391, "Ophthalmic Goods" },
                { 392, "Ordnance and Accessories, except Vehicles and Guided Missiles" },
                { 393, "Organization Hotels and Lodging" },
                { 394, "Paint, Glass, and Wallpaper Stores" },
                { 395, "Painting and Paper Hanging" },
                { 396, "Paints, Varnishes, Lacquers, Enamels, and Allied Products" },
                { 397, "Paper and Paper Products" },
                { 398, "Paper Mills" },
                { 399, "Paperboard Containers and Boxes" },
                { 400, "Paperboard Mills" },
                { 401, "Partitions, Shelving, Lockers, and Office and Store Fixtures" },
                { 402, "Pens, Pencils, and other Artists Materials" },
                { 403, "Pension, Health, and Welfare Funds" },
                { 404, "Periodicals: Publishing, or Publishing and Printing" },
                { 405, "Personal Credit Institutions" },
                { 406, "Personnel Supply Services" },
                { 407, "Petroleum and Petroleum Products" },
                { 408, "Petroleum Refining" },
                { 409, "Photographic Equipment and Supplies" },
                { 410, "Photographic Studios, Portrait" },
                { 411, "Pipelines, except Natural Gas" },
                { 412, "Plastics Materials and Synthetic Resins, Synthetic Rubber" },
                { 413, "Plumbing, Heating and Air-conditioning" },
                { 414, "Political Organizations" },
                { 415, "Pottery and Related Products" },
                { 416, "Poultry and Eggs" },
                { 417, "Primary Smelting and Refining of Nonferrous" },
                { 418, "Private Households" },
                { 419, "Professional and Commercial Equipment and Supplies" },
                { 420, "Professional Membership Organizations" },
                { 421, "Public Building and Related Furniture" },
                { 422, "Public Finance, Taxation, and Monetary Policy" },
                { 423, "Public Order and Safety" },
                { 424, "Public Warehousing and Storage" },
                { 425, "Pulp Mills" },
                { 426, "Radio and Television Broadcasting Stations" },
                { 427, "Railroad Equipment" },
                { 428, "Railroads" },
                { 429, "Real Estate Agents and Managers" },
                { 430, "Real Estate Operators (except Developers) and Lessors" },
                { 431, "Recreation Vehicle Dealers" },
                { 432, "Refrigeration and Service Industry Machinery" },
                { 433, "Regulation and Administration of Communications, Electric, Gas, and other Utilities" },
                { 434, "Regulation and Administration of Transportation" },
                { 435, "Regulation of Agricultural Marketing and Commodities" },
                { 436, "Regulation, Licensing, and Inspection of Miscellaneous Commercial Sectors" },
                { 437, "Religious Organizations" },
                { 438, "Rental of Railroad Cars" },
                { 439, "Research, Development, and Testing Services" },
                { 440, "Residential Care" },
                { 441, "Retail Bakeries" },
                { 442, "Retail Nurseries, Lawn and Garden Supply Stores" },
                { 443, "Retail Stores, not elsewhere classified" },
                { 444, "Reupholstery and Furniture Repair" },
                { 445, "Rolling, Drawing, and Extruding of Nonferrous" },
                { 446, "Roofing, Siding, and Sheet Metal Work" },
                { 447, "Rooming and Boarding Houses" },
                { 448, "Rubber and Plastics Footwear" },
                { 449, "Sand and Gravel" },
                { 450, "Sanitary Services" },
                { 451, "Savings Institutions" },
                { 452, "Sawmills and Planing Mills" },
                { 453, "School Buses" },
                { 454, "Schools and Educational Services, not elsewhere classified" },
                { 455, "Screw Machine Products, and Bolts, Nuts, Screws, Rivets, and Washers" },
                { 456, "Search, Detection, Navigation, Guidance, Aeronautical, and Nautical Systems, Instruments, and Equipment" },
                { 457, "Secondary Smelting and Refining of Nonferrous" },
                { 458, "Security and Commodity Exchanges" },
                { 459, "Security Brokers, Dealers, and Flotation" },
                { 460, "Services Allied with the Exchange of Securities" },
                { 461, "Services Incidental to Water Transport" },
                { 462, "Services to Dwellings and other Buildings" },
                { 463, "Ship and Boat Building and Repairing" },
                { 464, "Shoe Repair Shops and Shoeshine Parlors" },
                { 465, "Shoe Stores" },
                { 466, "Soap, Detergents, and Cleaning Preparations; Perfumes, Cosmetics, and Other Toilet Preparations" },
                { 467, "Social Services, not elsewhere classified" },
                { 468, "Soil Preparation Services" },
                { 469, "Space Research and Technology" },
                { 470, "Special Industry Machinery, except Metalworking" },
                { 471, "Steam and Air-Conditioning Supply" },
                { 472, "Steel Works, Blast Furnaces, and Rolling and Finishing Mills" },
                { 473, "Structural Clay Products" },
                { 474, "Sugar and Confectionery Products" },
                { 475, "Surety Insurance" },
                { 476, "Surgical, Medical, and Dental Instruments and Supplies" },
                { 477, "Taxicabs" },
                { 478, "Telegraph and other Message Communications" },
                { 479, "Telephone Communications" },
                { 480, "Terminal and Joint Terminal Maintenance" },
                { 481, "Theatrical Producers (except Motion Picture)" },
                { 482, "Timber Tracts" },
                { 483, "Tires and Inner Tubes" },
                { 484, "Title Abstract Offices" },
                { 485, "Title Insurance" },
                { 486, "Tobacco Stemming and Redrying" },
                { 487, "Trucking and Courier Services, except Air" },
                { 488, "Trusts" },
                { 489, "Used Merchandise Stores" },
                { 490, "Variety Stores" },
                { 491, "Vegetables and Melons" },
                { 492, "Veterinary Services" },
                { 493, "Video Tape Rental" },
                { 494, "Vocational Schools" },
                { 495, "Watch, Clock, and Jewelry Repair" },
                { 496, "Watches, Clocks, Clockwork Operated Devices, and Parts" },
                { 497, "Water Supply" },
                { 498, "Water Transportation of Freight, not elsewhere classified" },
                { 499, "Water Transportation of Passengers" },
                { 500, "Water Well Drilling" },
                { 501, "Women's Accessory and Specialty Stores" },
                { 502, "Women's Clothing Stores" },
                { 503, "Women's, Misses', and Juniors' Outerwear" },
                { 504, "Women's, Misses', Children's, and Infants' Underwear and Nightwear" },
                { 505, "Wood Buildings and Mobile Homes" },
                { 506, "Wood Containers" },
                { 507, "Yarn and Thread Mills" },


            };

            return SectorList.ContainsValue(sector) ? SectorList.Where(item => item.Value == sector).FirstOrDefault().Key : 0;
        }
    }
}