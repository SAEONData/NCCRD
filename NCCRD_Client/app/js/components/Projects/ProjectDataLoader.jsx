import React from 'react'
import { connect } from 'react-redux'
import buildQuery from 'odata-query'
import { apiBaseURL, vmsBaseURL } from "../../config/serviceURLs.js"
import { CustomFetch } from '../../globalFunctions.js';
import { notification, message } from 'antd'

const _gf = require("../../globalFunctions")

const mapStateToProps = (state, props) => {
  let { globalData: { daoid } } = state
  let { projectData: { selectedProjectId } } = state
  let lookupDataLoaded = state.lookupData.loaded
  return { selectedProjectId, lookupDataLoaded, daoid }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setLoading: payload => {
      dispatch({ type: "SET_LOADING", payload })
    },
    setEditMode: payload => {
      dispatch({ type: "SET_EDIT_MODE", payload })
    },
    loadProjectDetails: payload => {
      dispatch({ type: "LOAD_PROJECT_DETAILS", payload })
    },
    loadProjectFunderDetails: payload => {
      dispatch({ type: "LOAD_PROJECTFUNDER_DETAILS", payload })
    },
    loadAdaptationDetails: payload => {
      dispatch({ type: "LOAD_ADAPTATION_DETAILS", payload })
    },
    loadMitigationDetails: payload => {
      dispatch({ type: "LOAD_MITIGATION_DETAILS", payload })
    },
    loadMitigationEmissions: payload => {
      dispatch({ type: "LOAD_MITIGATION_EMISSIONS", payload })
    },
    loadResearchDetails: payload => {
      dispatch({ type: "LOAD_RESEARCH_DETAILS", payload })
    },
    loadAdaptationPurpose: payload => {
      dispatch({ type: "LOAD_ADAPTATION_PURPOSE", payload })
    },
    loadCarbonCredit: payload => {
      dispatch({ type: "LOAD_CARBON_CREDIT", payload })
    },
    loadCarbonCreditMarket: payload => {
      dispatch({ type: "LOAD_CARBON_CREDIT_MARKET", payload })
    },
    loadCDMMethodology: payload => {
      dispatch({ type: "LOAD_CDM_METHODOLOGY", payload })
    },
    loadCDMStatus: payload => {
      dispatch({ type: "LOAD_CDM_STATUS", payload })
    },
    loadProjectStatus: payload => {
      dispatch({ type: "LOAD_PROJECT_STATUS", payload })
    },
    loadProjectTypes: payload => {
      dispatch({ type: "LOAD_PROJECT_TYPE", payload })
    },
    loadProjectSubTypes: payload => {
      dispatch({ type: "LOAD_PROJECT_SUBTYPE", payload })
    },
    loadResearchType: payload => {
      dispatch({ type: "LOAD_RESEARCH_TYPE", payload })
    },
    loadTargetAudience: payload => {
      dispatch({ type: "LOAD_TARGET_AUDIENCE", payload })
    },
    loadTypology: payload => {
      dispatch({ type: "LOAD_TYPOLOGY", payload })
    },
    loadFundingStatus: payload => {
      dispatch({ type: "LOAD_FUNDINGSTATUS", payload })
    },
    loadFundingType: payload => {
      dispatch({ type: "LOAD_FUNDINGTYPE", payload })
    },
    loadUsers: payload => {
      dispatch({ type: "LOAD_USERS", payload })
    },
    loadValidationStatus: payload => {
      dispatch({ type: "LOAD_VALIDATION_STATUS", payload })
    },
    loadVoluntaryGoldStandard: payload => {
      dispatch({ type: "LOAD_VOLUNTARY_GOLD_STANDARD", payload })
    },
    loadVoluntaryMethodology: payload => {
      dispatch({ type: "LOAD_VOLUNTARY_METHODOLOGY", payload })
    },
    loadResearchMaturity: payload => {
      dispatch({ type: "LOAD_RESEARCH_MATURITY", payload })
    },
    loadHazards: payload => {
      dispatch({ type: "LOAD_HAZARDS", payload })
    },
    loadRegions: payload => {
      dispatch({ type: "LOAD_REGION", payload })
    },
    loadSectors: payload => {
      dispatch({ type: "LOAD_SECTOR", payload })
    },
    setLookupDataLoaded: payload => {
      dispatch({ type: "SET_LOOKUPS_LOADED", payload })
    }
  }
}

class ProjectDataLoader extends React.Component {

  constructor(props) {
    super(props);

    this.loadData = this.loadData.bind(this)

    this.state = {
      currentProjectId: -1
    }
  }

  componentDidMount() {
    this.onLoad()
  }

  componentDidUpdate() {
    this.onLoad()
  }

  onLoad() {
    if (this.props.selectedProjectId !== this.state.currentProjectId) {
      this.setState({ currentProjectId: this.props.selectedProjectId }, () =>
        setTimeout(() => {
          this.loadData()
        }, 100))
    }
  }

  async loadData(overrideId) {
    this.props.setLoading(true)
    this.props.setEditMode(true)

    // LOAD PROJECT DATA //
    await this.loadProjectData(overrideId)

    if (this.props.lookupDataLoaded === false) {

      // LOAD LOOKUP DATA //
      await this.loadLookupsData()
      await this.loadHazardsData()
      await this.loadRegionsData()
      await this.loadSectorsData()

      this.props.setLookupDataLoaded(true)
    }

    if (typeof overrideId !== 'undefined') {
      this.setState({ currentProjectId: -1 })
    }
    this.props.setLoading(false)
  }

  async loadProjectData(overrideId) {

    let { currentProjectId } = this.state

    if (typeof overrideId !== 'undefined') {
      currentProjectId = overrideId
    }

    let ProjectDetails = null
    let AdaptationDetails = null
    let Funders = null
    let MitigationDetails = null
    let MitigationEmissionsData = null
    let ResearchDetails = null

    const newProjectTemplate = {
      ProjectId: _gf.getRndInteger(1111111, 9999999),
      ProjectTitle: "",
      ProjectDescription: "",
      LeadAgent: "",
      HostPartner: "",
      HostOrganisation: "",
      StartYear: 0,
      EndYear: 0,
      AlternativeContact: "",
      AlternativeContactEmail: "",
      Link: "",
      ValidationComments: "",
      BudgetLower: 0,
      BudgetUpper: 0,
      ProjectTypeId: 0,
      ProjectStatusId: 0,
      ProjectSubTypeId: 0,
      ProjectManagerId: 0,
      ValidationStatusId: 0,
      Verified: false,
      ProjectDAOs: [],
      ProjectRegions: [],
      ProjectLocations: [],
      state: "modified"
    }

    if (currentProjectId === 0) {

      //Create new project//
      ProjectDetails = newProjectTemplate
      AdaptationDetails = []
      Funders = []
      MitigationDetails = []
      MitigationEmissionsData = []
      ResearchDetails = []
    }
    else {

      //Get project details
      const query = buildQuery({
        expand: [
          "Project/ProjectRegions",
          "Project/ProjectDAOs",
          "Project/ProjectLocations/Location",
          "Funders",
          "AdaptationDetails/ResearchDetail",
          "MitigationDetails/ResearchDetail",
          "MitigationEmissionsData",
          "ResearchDetails"
        ]
      })

      try {
        let res = await CustomFetch(apiBaseURL + `ProjectDetails/${currentProjectId}${query}`)
        let resBody = await res.json()

        if (res.ok && resBody) {
          ProjectDetails = resBody.Project
          AdaptationDetails = resBody.AdaptationDetails
          Funders = resBody.Funders
          MitigationDetails = resBody.MitigationDetails
          MitigationEmissionsData = resBody.MitigationEmissionsData
          ResearchDetails = resBody.ResearchDetails
        }
        else {
          throw new Error(resBody.error.message)
        }
      }
      catch (ex) {

        //Show error message
        notification.error({
          message: 'Unable to load project details. (See log for details)'
        })

        //Reset project data
        ProjectDetails = newProjectTemplate
        AdaptationDetails = []
        Funders = []
        MitigationDetails = []
        MitigationEmissionsData = []
        ResearchDetails = []

        //Log error details
        console.error(ex)
      }
    }

    //Push DAOID from url config (if available)
    let { daoid } = this.props
    if (daoid && _gf.IsValidGuid(daoid)) {
      if (ProjectDetails.ProjectDAOs.filter(x => x.DAOId === daoid).length === 0) {
        ProjectDetails.ProjectDAOs.push({
          ProjectDAOId: 0,
          ProjectId: ProjectDetails.ProjectId,
          DAOId: daoid
        })
      }
    }

    //Dispatch all to store
    this.props.loadProjectDetails(ProjectDetails)
    this.props.loadProjectFunderDetails(Funders)
    this.props.loadAdaptationDetails(AdaptationDetails)
    this.props.loadMitigationDetails(MitigationDetails)
    this.props.loadMitigationEmissions(MitigationEmissionsData)
    this.props.loadResearchDetails(ResearchDetails)

  }

  async loadLookupsData() {

    const query = buildQuery({
      expand: [
        "AdaptationPurpose",
        "CarbonCredit",
        "CarbonCreditMarket",
        "CDMMethodology",
        "CDMStatus",
        "ProjectStatus",
        "ProjectType",
        "ProjectSubType",
        "ResearchType",
        "TargetAudience",
        "Typology",
        "Person",
        "ValidationStatus",
        "VoluntaryGoldStandard",
        "VoluntaryMethodology",
        "FundingStatus",
        "FundingType",
        "ResearchMaturity"
      ]
    })

    try {
      let res = await CustomFetch(apiBaseURL + `Lookups${query}`)
      let resBody = await res.json()

      if (res.ok && resBody) {

        //Dispatch results
        this.props.loadAdaptationPurpose(resBody.AdaptationPurpose)
        this.props.loadCarbonCredit(resBody.CarbonCredit)
        this.props.loadCarbonCreditMarket(resBody.CarbonCreditMarket)
        this.props.loadCDMMethodology(resBody.CDMMethodology)
        this.props.loadCDMStatus(resBody.CDMStatus)
        this.props.loadProjectStatus(resBody.ProjectStatus)
        this.props.loadProjectTypes(resBody.ProjectType)
        this.props.loadProjectSubTypes(resBody.ProjectSubType)
        this.props.loadResearchType(resBody.ResearchType)
        this.props.loadTargetAudience(resBody.TargetAudience)
        this.props.loadTypology(resBody.Typology)
        this.props.loadFundingStatus(resBody.FundingStatus)
        this.props.loadFundingType(resBody.FundingType)
        this.props.loadUsers(resBody.Person)
        this.props.loadValidationStatus(resBody.ValidationStatus)
        this.props.loadVoluntaryGoldStandard(resBody.VoluntaryGoldStandard)
        this.props.loadVoluntaryMethodology(resBody.VoluntaryMethodology)
        this.props.loadResearchMaturity(resBody.ResearchMaturity)
      }
      else {
        throw new Error(resBody)
      }
    }
    catch (ex) {
      console.error(ex)
    }
  }

  async loadHazardsData() {

    let { loadHazards } = this.props

    //Get (external) Hazards
    CustomFetch(`${vmsBaseURL}hazards/flat`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(res => res.json())
      .then(res => {
        loadHazards(res.items)
      })
      .catch(res => {
        console.error("Error details:", res)
      })
  }

  async loadRegionsData() {

    let { loadRegions } = this.props

    //Get (external) Regions
    CustomFetch(`${vmsBaseURL}regions/flat`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(res => res.json())
      .then(res => {
        loadRegions(res.items)
      })
      .catch(res => {
        console.error("Error details:", res)
      })
  }

  async loadSectorsData() {

    let { loadSectors } = this.props

    //Get (external) Sectors
    CustomFetch(`${vmsBaseURL}sectors/flat`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(res => res.json())
      .then(res => {
        loadSectors(res.items)
      })
      .catch(res => {
        console.error("Error details:", res)
      })
  }

  render() {
    const { children } = this.props;

    const childrenWithProps = React.Children.map(children, child =>
      React.cloneElement(child, { ...this.props, loadData: this.loadData })
    );

    return <div>{childrenWithProps}</div>
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProjectDataLoader)