﻿using NCCRD.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCCRD.Services.Data.Models
{
    public class ProjectsViewModel
    {
        public List<Project> Projects { get; set; }
        public List<Region> Regions { get; set; }
        public List<LocationType> LocationTypes { get; set; }
        public List<ProjectRegion> ProjectRegions { get; set; }

        public ProjectsViewModel()
        {
            Projects = new List<Project>();
            Regions = new List<Region>();
            LocationTypes = new List<LocationType>();
            ProjectRegions = new List<ProjectRegion>();
        }

        public class Geometry
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }

            public Geometry()
            {
                coordinates = new List<double>();
            }
        }

        public class ProjectGeoJson
        {
            public string type { get; set; }
            public Geometry geometry { get; set; } = new Geometry();
            public Dictionary<string, string> properties { get; set; } = new Dictionary<string, string>();
        }
    }

    public class ProjectDetailsViewModel : Project
    {
        public ProjectDetailsViewModel() : base()
        {

        }

        public ProjectDetailsViewModel(Project project)
        {
            ProjectId = project.ProjectId;
            ProjectTitle = project.ProjectTitle;
            ProjectDescription = project.ProjectDescription;
            LeadAgent = project.LeadAgent;
            HostPartner = project.HostPartner;
            HostOrganisation = project.HostOrganisation;
            StartYear = project.StartYear;
            EndYear = project.EndYear;
            ReminderSent = project.ReminderSent;
            AlternativeContact = project.AlternativeContact;
            AlternativeContactEmail = project.AlternativeContactEmail;
            Link = project.Link;
            ValidationComments = project.ValidationComments;
            BudgetUpper = project.BudgetUpper;
            BudgetLower = project.BudgetLower;
            ProjectTypeId = project.ProjectTypeId;
            ProjectSubTypeId = project.ProjectSubTypeId;
            ProjectStatusId = project.ProjectStatusId;
            ProjectManagerId = project.ProjectManagerId;
            ValidationStatusId = project.ValidationStatusId;
            MAOptionId = project.MAOptionId;
        }

        public string ProjectTypeName { get; set; }
        public string ProjectSubTypeName { get; set; }
        public string ProjectStatusName { get; set; }
        public string ProjectManagerName { get; set; }
        public string ValidationStatusName { get; set; }
        public string MAOptionName { get; set; }
    }

    public class MitigationDetailsViewModel : MitigationDetail
    {
        public MitigationDetailsViewModel() : base()
        {

        }

        public MitigationDetailsViewModel(MitigationDetail data)
        {
            MitigationDetailId = data.MitigationDetailId;
            VCS = data.VCS;
            Other = data.Other;
            OtherDescription = data.OtherDescription;
            CDMProjectNumber = data.CDMProjectNumber;
            CarbonCreditId = data.CarbonCreditId;
            CarbonCreditMarketId = data.CarbonCreditMarketId;
            CDMStatusId = data.CDMStatusId;
            CDMMethodologyId = data.CDMMethodologyId;
            VoluntaryMethodologyId = data.VoluntaryMethodologyId;
            VoluntaryGoldStandardId = data.VoluntaryGoldStandardId;
            ProjectId = data.ProjectId;
            SectorId = data.SectorId;
        }

        public string CarbonCreditName { get; set; }
        public string CarbonCreditMarketName { get; set; }
        public string CDMStatusName { get; set; }
        public string CDMMethodologyName { get; set; }
        public string VoluntaryMethodologyName { get; set; }
        public string VoluntaryGoldStandardName { get; set; }
        public string SectorName { get; set; }
    }

    public class AdaptationDetailsViewModel : AdaptationDetail
    {
        public AdaptationDetailsViewModel() : base()
        {

        }

        public AdaptationDetailsViewModel(AdaptationDetail data)
        {
            AdaptationDetailId = data.AdaptationDetailId;
            Description = data.Description;
            AdaptationPurposeId = data.AdaptationPurposeId;
            ProjectId = data.ProjectId;
            SectorId = data.SectorId;
        }

        public string AdaptationPurposeName { get; set; }
        public string SectorName { get; set; }
    }

    public class ResearchDetailsViewModel : ResearchDetail
    {
        public ResearchDetailsViewModel() : base()
        {

        }

        public ResearchDetailsViewModel(ResearchDetail model)
        {
            Author = model.Author;
            PaperLink = model.PaperLink;
        }

        public string ResearchTypeName { get; set; }
        public string TargetAudienceName { get; set; }
        public string SectorName { get; set; }
    }
}