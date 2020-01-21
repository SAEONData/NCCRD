using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using NCCRD.Services.DataV2.Database.Models;
using NCCRD.Services.DataV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Database.Contexts
{
    public class ODataModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            int maxExpandDepth = 0;

            builder.EntitySet<Project>("Projects")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectFunder>("ProjectFunders")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<Funder>("Funders")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectDetails>("ProjectDetails")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command;   

            builder.EntitySet<Lookups>("Lookups")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command;  

            builder.EntitySet<ProjectRegion>("ProjectRegions")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<AdaptationDetail>("AdaptationDetails")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<MitigationDetail>("MitigationDetails")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<MitigationEmissionsData>("MitigationEmissionsData")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ResearchDetail>("ResearchDetails")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<AdaptationPurpose>("AdaptationPurpose")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<CarbonCredit>("CarbonCredit")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<CarbonCreditMarket>("CarbonCreditMarket")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<CDMMethodology>("CDMMethodology")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<CDMStatus>("CDMStatus")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectStatus>("ProjectStatus")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectType>("ProjectType")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectSubType>("ProjectSubType")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ResearchType>("ResearchType")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<TargetAudience>("TargetAudience")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<Typology>("Typology")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<Person>("User")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ValidationStatus>("ValidationStatus")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<VoluntaryGoldStandard>("VoluntaryGoldStandard")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<VoluntaryMethodology>("VoluntaryMethodology")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectLocation>("ProjectLocations")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<Location>("Location")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ProjectDAO>("ProjectDAOs")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 

            builder.EntitySet<ResearchMaturity>("ResearchMaturity")
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand(maxExpandDepth) // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select();// Allow for the $select Command; 


            //#####################//
            // FUNCTIONS & ACTIONS //
            //#####################//

            builder.Namespace = "Extensions";

            builder.EntityType<Project>()
                .Collection
                .Action("ByPolygon")
                .ReturnsCollectionViaEntitySetPath<Project>("bindingParameter")
                .Parameter<Polygon>("polygon");

            builder.EntityType<Project>()
                .Collection
                .Action("Filter")
                .ReturnsCollectionViaEntitySetPath<Project>("bindingParameter")
                .Parameter<Filters>("filters");

            builder.
                EntityType<Project>().
                Collection.
                Function("GeoJson").
                Returns<JsonResult>();

            return builder.GetEdmModel();
        }
    }
}
