import React from 'react'

export const ui_config = [

  // PROJECT DETAILS //
  {
    key: "txtProjectTitle",
    label: "Project title:",
    tooltip: "Name of project",
    required: true
  },
  {
    key: "txtProjectYear",
    label: "Year:",
    tooltip: "Start/End Dates: Dates which work begins/ends on the project (from 2000 - 2050)"
  },
  {
    key: "txtProjectDescription",
    label: "Description:",
    tooltip: "Description of the project including aims, objective, methodology used, life expectancy and outcome. The outcome should be the actual or desired outcome of the project, such as findings of research, levels of emissions or energy savings, level of resilience achieved, human capital potential, other environmental, social and economic benefits."
  },
  {
    key: "txtProjectLeadAgent",
    label: "Lead organisation:",
    tooltip: "Organisation responsible for administering the project",
    required: true
  },
  {
    key: "txtProjectHostPartner",
    label: "Host partner:",
    tooltip: "Other organisation/s responsible for conucting part of the project "
  },
  {
    key: "txtProjectHostOrganisation",
    label: "Host organisation:",
    tooltip: "Oganisation responsible for conducting the project",
    required: true
  },
  {
    key: "txtProjectAlternativeContact",
    label: "Contact (alt):",
    tooltip: "Alternative contact who would be able to answer questions regarding the details of the project"
  },
  {
    key: "txtProjectAlternativeContactEmail",
    label: "Contact email (alt):",
    tooltip: "A valid email address for the alternative contact"
  },
  {
    key: "txtProjectLink",
    label: "Link:",
    tooltip: "Link to project site or reference information"
  },
  {
    key: "txtProjectBudget",
    label: "Budget range:",
    tooltip: "The approximate range of funding for all actions listed in the project across all funding agencies"
  },

  {
    key: "selProjectStatus",
    label: "Project status:",
    tooltip: "Status of action implementation",
    tooltip2: <div>
      <b>Planned:</b> The concept for a project has been put forward but is not currently underway and may not specifically have funding.
      <br />
      <b>Adopted (Approved):</b> The project has moved from the concept stage and specifications for implementation have been established. This project may have been assigned funding.
      <br />
      <b>In Progress:</b> Infrastructure for the project is in the process of being established and the project has assigned funding.
      <br />
      <b>Implemented (Operational):</b> The project infrastructure has been completed and any remaining funding is now directed towards maintenance.
      <br />
      <b>Published:</b> The project has been completed and the outcome described in professional documentation.
      <br />
      <b>Expired:</b> The project infrastructure is no longer useful or sustainable without additional funding or funding for the project has run out.
      <br />
      <b>Discontinued:</b> The project has been abandoned.
    </div>,
    required: true
  },
  {
    key: "selProjectManager",
    label: "Project manager:",
    tooltip: "Name and contact details of the person responsible for this project and for maintaining this entry on the database.",
    required: true
  },
  {
    key: "lblLocations",
    label: "Locations:",
    tooltip: "The location data as appropriate for the actions described in this project. Add by manually inputting the GPS coordinates into the text field or by placing a pin on the map provided by selecting the globe icon. Please note that only one pin can be placed at a time to eastablish a set of coordinates. "
  },
  {
    key: "lblRegions",
    label: "Regions:",
    tooltip: "Select all regions at the level that best describes the locations where this project will take place (province, metro or district municipality, local municipality)"
  },
  {
    key: "btnVerify",
    label: "Project verification status:",
    tooltip: "Click the button below to verify this project"
  },
  {
    key: "txtProjectValidationComments",
    label: "Validation comments:",
    tooltip: "Validation comments"
  },
  {
    key: "selProjectType",
    label: "Project type:",
    tooltip: "Project type",
    required: true
  },
  {
    key: "selProjectSubType",
    label: "Project sub-type:",
    tooltip: "Project sub-type"
  },
  {
    key: "selProjectValidationStatus",
    label: "Validation status:",
    tooltip: "Validation status"
  },
  {
    key: "selProjectMAOptions",
    label: "M.A. Options:",
    tooltip: "Mitigation & Adaptation options"
  },


  //FUNDING
  {
    key: "lblGrantProgram",
    label: "Grant or program name:",
    tooltip: "The name of the grant or program funding the project"
  },
  {
    key: "lblFundingAgency",
    label: "Funding agency:",
    tooltip: "The name of the agency funding the project",
    required: true
  },
  {
    key: "lblPartneringDepts",
    label: "Partnering departments/organisations:",
    tooltip: "The name of any partnering deparments or organisations assisting with funding the project"
  },
  {
    key: "lblProjectCoordinator",
    label: "Project coordinator:",
    tooltip: "The name of the project coordinator from the funding agency"
  },
  {
    key: "lblTotalBudget",
    label: "Total budget:",
    tooltip: "The total budget from this grant or program"
  },
  {
    key: "lblAnnualBudget",
    label: "Annual budget:",
    tooltip: "The annual budget from this grant or program"
  },
  {
    key: "lblFundingStatus",
    label: "Funding status:",
    tooltip: "The current funding status of the project"
  },
  {
    key: "lblFundingStatusTable",
    // label: "",
    // tooltip: "Select current funding status for the project"
  },

 
  // ADAPTATION DETAILS //
  {
    key: "txtAdaptationTitle",
    label: "Title:",
    tooltip: "Name of action"
  },
  {
    key: "txtAdaptationDescription",
    label: "Description:",
    tooltip: "The general details of the action including aims, objective, methodology used, life expectancy and outcome. The outcome should be the actual or desired outcome of the project, such as findings of research, level of resilience achieved, human capital potential, other environmental, social and economic benefits."
  },
  {
    key: "selAdaptationPurpose",
    label: "Purpose:",
    tooltip: "Loss Prevention: Reduce vulnerability. Loss Sharing: Spread risk. Behaviour Modification: Eliminate activity or behaviour. Relocation: Move away from hazard",
    required: true
  },
  {
    key: "selAdaptationSector",
    label: "Sector:",
    tooltip: "Sector in which project is taking place"
  },
  {
    key: "selAdaptationHazard",
    label: "Hazard:",
    tooltip: "All hazards this action is designed to adapt to"
  },
  {
    key: "selAdaptationActionStatus",
    label: "Project status:",
    tooltip: "Status of action implementation",
    required: true
  },
  {
    key: "txtAdaptationContactName",
    label: "Name:",
    tooltip: "Name of the person in charge of managing this action"
  },
  {
    key: "txtAdaptationContactEmail",
    label: "Email Address :",
    tooltip: "Email of person responsible for managing this action"
  },


  // MITIGATION DETAILS //
  {
    key: "selMitigationCarbonCredit",
    label: "Carbon credit:",
    tooltip: "Carbon credit",
    required: true
  },
  {
    key: "selMitigationCarbonCreditMarket",
    label: "Carbon credit market:",
    tooltip: "Carbon credit market"
  },
  {
    key: "selMitigationCDMStatus",
    label: "CDM status:",
    tooltip: "CDM status"
  },
  {
    key: "selMitigationCDMMethodology",
    label: "CDM methodology:",
    tooltip: "CDM methodology"
  },
  {
    key: "selMitigationVoluntaryMethodology",
    label: "Voluntary methodology:",
    tooltip: "Voluntary methodology"
  },
  {
    key: "selMitigationVoluntaryGoldStandard",
    label: "Voluntary gold standard:",
    tooltip: "Voluntary gold standard"
  },
  {
    key: "txtMitigationCDMProjectNumber",
    label: "CDM project number:",
    tooltip: "CDM project number"
  },
  {
    key: "txtMitigationOtherDescription",
    label: "Other description:",
    tooltip: "Other description"
  },
  {
    key: "selMitigationSector",
    label: "Sector:",
    tooltip: "Sector"
  },


  // EMISSIONS DETAILS //
  {
    key: "txtEmissionsYear",
    label: "Year:",
    tooltip: "Year",
    required: true
  },
  {
    key: "txtEmissionsCO2",
    label: "CO2:",
    tooltip: "CO2"
  },
  {
    key: "txtEmissionsCH4",
    label: "CH4:",
    tooltip: "CH4"
  },
  {
    key: "txtEmissionsCH4_CO2e",
    label: "CH4_CO2e:",
    tooltip: "CH4_CO2e"
  },
  {
    key: "txtEmissionsN2O",
    label: "N2O:",
    tooltip: "N2O"
  },
  {
    key: "txtEmissionsN2O_CO2e",
    label: "N2O_CO2e:",
    tooltip: "N2O_CO2e"
  },
  {
    key: "txtEmissionsHFC",
    label: "HFC:",
    tooltip: "HFC"
  },
  {
    key: "txtEmissionsHFC_CO2e",
    label: "HFC_CO2e:",
    tooltip: "HFC_CO2e"
  },
  {
    key: "txtEmissionsPFC",
    label: "PFC:",
    tooltip: "PFC"
  },
  {
    key: "txtEmissionsPFC_CO2e",
    label: "PFC_CO2e:",
    tooltip: "PFC_CO2e"
  },
  {
    key: "txtEmissionsSF6",
    label: "SF6:",
    tooltip: "SF6"
  },
  {
    key: "txtEmissionsSF6_CO2e",
    label: "SF6_CO2e:",
    tooltip: "SF6_CO2e"
  },
  {
    key: "txtEmissionsHydro",
    label: "Hydro:",
    tooltip: "Hydro"
  },
  {
    key: "txtEmissionsHydro_CO2e",
    label: "Hydro_CO2e:",
    tooltip: "Hydro_CO2e"
  },
  {
    key: "txtEmissionsTidal",
    label: "Tidal:",
    tooltip: "Tidal"
  },
  {
    key: "txtEmissionsTidal_CO2e",
    label: "Tidal_CO2e:",
    tooltip: "Tidal_CO2e"
  },
  {
    key: "txtEmissionsWind",
    label: "Wind:",
    tooltip: "Wind"
  },
  {
    key: "txtEmissionsWind_CO2e",
    label: "Wind_CO2e:",
    tooltip: "Wind_CO2e"
  },
  {
    key: "txtEmissionsSolar",
    label: "Solar:",
    tooltip: "Solar"
  },
  {
    key: "txtEmissionsSolar_CO2e",
    label: "Solar_CO2e:",
    tooltip: "Solar_CO2e"
  },
  {
    key: "txtEmissionsFossilFuelElecRed",
    label: "FossilFuelElecRed:",
    tooltip: "FossilFuelElecRed"
  },
  {
    key: "txtEmissionsFossilFuelElecRed_CO2e",
    label: "FossilFuelElecRed_CO2e:",
    tooltip: "FossilFuelElecRed_CO2e"
  },
  {
    key: "txtEmissionsBioWaste",
    label: "BioWaste:",
    tooltip: "BioWaste"
  },
  {
    key: "txtEmissionsBioWaste_CO2e",
    label: "BioWaste_CO2e:",
    tooltip: "BioWaste_CO2e"
  },
  {
    key: "txtEmissionsGeothermal",
    label: "Geothermal:",
    tooltip: "Geothermal"
  },
  {
    key: "txtEmissionsGeothermal_CO2e",
    label: "Geothermal_CO2e:",
    tooltip: "Geothermal_CO2e"
  },


  // RESEARCH DETAILS //
  {
    key: "txtResearchAuthor",
    label: "Author:",
    tooltip: "Person(s) carrying out research/analysis",
    required: true
  },
  {
    key: "txtResearchPaperLink",
    label: "Paper link:",
    tooltip: "A website link or a digital object identifier (DOI) to the associated research"
  },
  {
    key: "selResearchType",
    label: "Research type:",
    tooltip: "Scientific: e.g. climatic impacts. R&D: e.g. industry and technology. Economic and market impacts: e.g. effect of carbon tax. Social: e.g. impact of climate change on communities in x region",
    required: true
  },
  {
    key: "selResearchTargetAudience",
    label: "Target audience:",
    tooltip: "Primary audience for the research",
    required: true
  },
  {
    key: "selResearchSector",
    label: "Sector:",
    tooltip: "The sector this action falls under"
  },
  {
    key: "selResearchResearchMaturity",
    label: "Research Maturity:",
    tooltip: "Published Research: The mitigation or adaptation action has been researched for the region of interest and is linked to a publication (‘Published’ in the original list). Feasible Option: The action has been evaluated in terms of technical, economic, social, and environmental impact and assessed to be feasible. Pre-Design: Preliminary engineering design has been completed and published, detailed feasibility report has been published."
  },


  // FILTERS //
  {
    key: "txtTitleFilter",
    label: "Title"
  },
  {
    key: "selStatusFilter",
    label: "Status"
  },
  {
    key: "selTypologyFilter",
    label: "Typology"
  },
  {
    key: "treeRegionFilter",
    label: "Region"
  },
  {
    key: "treeSectorFilter",
    label: "Sector"
  }
]

export function UILookup(key, defaultLabel) {

  let searchConfig = ui_config.filter(x => x.key === key)

  if (searchConfig.length > 0) {
    return searchConfig[0]
  }
  else {
    return {
      key: key,
      label: typeof defaultLabel === 'undefined' ? "" : defaultLabel,
      tooltip: "",
      required: false
    }
  }
}