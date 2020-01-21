import React from 'react'
import { connect } from 'react-redux'
import { Steps, Progress, Modal, Icon, notification } from 'antd'
import { Button, Row, Col, Fa } from 'mdbreact'
import buildQuery from 'odata-query'
import { apiBaseURL, vmsBaseURL } from "../../config/serviceURLs.js"
import ProjectDetailsStep from './Steps/ProjectDetailsStep.jsx';
import DAOLinkStep from './Steps/DAOLinkStep.jsx';
import ProjectLocationStep from './Steps/ProjectLocationStep.jsx';
import ProjectManagerStep from './Steps/ProjectManagerStep.jsx';
import AdaptationDetailsStep from './Steps/AdaptationDetailsStep.jsx';
import AdaptationContactStep from './Steps/AdaptationContactStep.jsx';
import AdaptationResearchStep from './Steps/AdaptationResearchStep.jsx';
import AdaptationFundingDetailStep from './Steps/AdaptationFundingStep.jsx'
import FundingDetailStep from './Steps/MitigationFundingDetailStep.jsx';
import MitigationDetailsStep from './Steps/MitigationDetailsStep.jsx'
import MitigationFundingDetailStep from './Steps/MitigationFundingDetailStep.jsx'
import OverallSummaryStep from './Steps/OverallSummaryStep.jsx';
import ActionsOverview from './Steps/ActionsOverview.jsx';
import { UILookup } from "../../config/ui_config.js"
import { DEAGreen, DEAGreenDark } from '../../config/colours.js'
import EditListModal from '../Projects/Details/ListEditing/EditListModal.jsx'
import EditTreeModal from '../Projects/Details/ListEditing/EditTreeModal.jsx'
import ProjectVerifyStep from './Steps/ProjectVerifyStep.jsx';

import "./SteppedInputForm.css"
import { CustomFetch } from '../../globalFunctions.js';
import MitigationApproachStep from './Steps/MitigationApproachStep.jsx'
import CarbonCreditStep from './Steps/CarbonCreditStep.jsx'
import EmissionsCalculatorStep from './Steps/EmissionsCalculatorStep.jsx'

const _gf = require("../../globalFunctions")

const mapStateToProps = (state, props) => {

  let user = state.oidc.user
  let { projectData: { projectDetails, selectedProjectId } } = state
  let { projectFundersData: { projectFunderDetails } } = state
  let { adaptationData: { adaptationDetails } } = state
  let { mitigationData: { mitigationDetails } } = state
  let { emissionsData: { emissionsData } } = state
  let lookupDataLoaded = state.lookupData.loaded
  let editListModalType = state.editListModalData.type
  let editListModalShow = state.editListModalData.show

  //Sort Funder on Id
  projectFunderDetails.sort((a, b) => {
    return a.FunderId - b.FunderId;
  })

  //Sort Adaptations on Id
  adaptationDetails.sort((a, b) => {
    return a.AdaptationDetailId - b.AdaptationDetailId;
  })

  return {
    projectDetails, projectFunderDetails, adaptationDetails, mitigationDetails, emissionsData,
    lookupDataLoaded, selectedProjectId, user, editListModalType, editListModalShow
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setLoading: payload => {
      dispatch({ type: "SET_LOADING", payload })
    },
    setLinkedDAOGoalId: payload => {
      dispatch({ type: "SET_PROJECT_LINKED_DAO_GOAL_ID", payload })
    },
    setFiltersChanged: payload => {
      dispatch({ type: "SET_FILTERS_CHANGED", payload })
    }
  }
}

const Step = Steps.Step
const confirm = Modal.confirm

let steps = [] //Make steps globally accessible

class SteppedInputForm extends React.Component {

  constructor(props) {
    super(props);

    this.onClose = this.onClose.bind(this)
    this.onSubmit = this.onSubmit.bind(this)
    this.updateWindowDimensions = this.updateWindowDimensions.bind(this)
    this.stepWizard = this.stepWizard.bind(this)
    this.jumpTo = this.jumpTo.bind(this)
    this.validateInputs = this.validateInputs.bind(this)
    this.renderListEditor = this.renderListEditor.bind(this)

    this.state = {
      winWidth: 0,
      winHeight: 0,
      currentStep: 0,
      progressCompleteOverride: false
    }
  }

  componentDidMount() {
    this.updateWindowDimensions();
    window.addEventListener('resize', this.updateWindowDimensions);
  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.updateWindowDimensions);
  }

  updateWindowDimensions() {
    this.setState({ winWidth: window.innerWidth, winHeight: window.innerHeight });
  }

  onClose() {

    //discard changes & reset
    this.setState({ currentStep: 0, progressCompleteOverride: false })
    this.props.loadData(0)

    //close form
    this.props.onClose()
  }

  async onSubmit() {

    //do submit stuff here//
    let saveRes = await this.saveChanges()

    if (saveRes) {
      //complete process
      this.setState({ progressCompleteOverride: true }, () => {
        setTimeout(() => {
          this.onClose()

          //Re-load projects
          this.props.setFiltersChanged(true)
        }, 500)
      })
    }
  }

  async saveChanges() {

    let { user, projectDetails, adaptationDetails, mitigationDetails, projectFunderDetails, selectedProjectId: projectId } = this.props
    let result = true
    let dataObj = { Id: projectId }

    //Show loading
    this.props.setLoading(true)

    //Add Project
    //if (projectDetails.state === 'modified') {
    let projectData = _.clone(projectDetails)
    projectData.ProjectId = projectId === 'add' ? 0 : parseInt(projectId)
    delete projectData.state //OData can only bind to the original object spec which does not contain 'state'
    dataObj.Project = projectData
    //}

    //Add Funding
    if (projectFunderDetails.filter(x => x.state === 'modified').length > 0) {
    let funderData = []
    projectFunderDetails/*.filter(x => x.state === 'modified')*/.forEach(item => {
      let funderItem = _.clone(item)
      delete funderItem.ProjectId //OData can only bind to the original object spec which does not contain 'ProjectId'
      delete funderItem.state //OData can only bind to the original object spec which does not contain 'state'
      delete funderItem.key //OData can only bind to the original object spec which does not contain 'key'
      funderData.push(funderItem)
    })
    dataObj.Funders = funderData
    }

    //Add AdaptationDetails
    //if (adaptationDetails.filter(x => x.state === 'modified').length > 0) {
    let adaptationData = []
    adaptationDetails/*.filter(x => x.state === 'modified')*/.forEach(item => {
      let adaptationItem = _.clone(item)
      delete adaptationItem.state //OData can only bind to the original object spec which does not contain 'state'
      adaptationItem.ProjectId = parseInt(projectId)  //Asociate with current project

      if (adaptationItem.ResearchDetail) {
        adaptationItem.ResearchDetail.ProjectId = parseInt(projectId)  //Asociate with current project
      }

      adaptationData.push(adaptationItem)
    })
    dataObj.AdaptationDetails = adaptationData
    //}

    let res = ""
    try {
      res = await CustomFetch(apiBaseURL + "ProjectDetails",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + (user === null ? "" : user.access_token)
          },
          body: JSON.stringify(dataObj)
        })

      if (!res.ok) {
        throw new Error()
      }
    }
    catch {

      //Show error notification
      notification.error({
        duration: 15,
        message: <div>
          Unable to save project.<br />
          (See log for error details)
          <br /><br />
          Please try again in a few minutes.
          <br /><br />
          If this problem persists, please contact the system administrator.
        </div>
      })

      // console.error(ex)
      console.error(res)
      result = false
    }

    //Hide loading
    this.props.setLoading(false)

    return result
  }

  onNext() {
    const currentStep = this.state.currentStep + 1;
    this.setState({ currentStep });
  }

  onPrev() {
    const currentStep = this.state.currentStep - 1;
    this.setState({ currentStep });
  }

  stepWizard(steps) {
    let { currentStep } = this.state
    this.setState({ currentStep: (currentStep + steps) })
  }

  jumpTo(stepTitle) {

    //let steps = this.getSteps()
    let filteredSteps = steps.filter(s => s.title === stepTitle)

    if (filteredSteps && filteredSteps.length > 0) {
      let index = steps.indexOf(filteredSteps[0])
      this.setState({ currentStep: index })
    }
  }

  showConfirm(title, message, yesText, noText, yesFunction) {
    confirm({
      title: title,
      content: message,
      okText: yesText,
      okType: 'danger',
      cancelText: noText,
      centered: true,
      onOk() {
        setTimeout(() => {
          yesFunction()
        }, 500);
      },
      onCancel() { },
    });
  }

  getSteps() {

    let { projectDetails, adaptationDetails, mitigationDetails, projectFunderDetails, setLinkedDAOGoalId, user } = this.props
    steps = []

    //Project
    let title = "Project - Details"
    steps.push({
      title: title,
      content: <ProjectDetailsStep />,
      error: false
    })
    steps.push({
      title: 'Project - DAO Link',
      optional: true,
      content: <DAOLinkStep
        ProjectDAOs={projectDetails.ProjectDAOs}
        linkCallback={(id, action) => { setLinkedDAOGoalId({ value: id, action, state: 'modified' }) }}
      />,
      error: false
    })
    steps.push({
      title: 'Project - Location',
      content: <ProjectLocationStep />,
      error: false
    })
    steps.push({
      title: 'Project - Manager',
      content: <ProjectManagerStep />,
      error: false
    })

    //Actions Overview
    steps.push({
      title: 'Actions - Overview',
      optional: true,
      content: <ActionsOverview jumpTo={this.jumpTo} />,
      error: false
    })

    //Funding
    projectFunderDetails.map(action => {
      let index = projectFunderDetails.indexOf(action) + 1

      steps.push({
        title: `Funding #${index} - Details`,
        backAction: "Actions - Overview",
        content: <FundingDetailStep details={action} />,
        error: false
      })
    })

    //Adaptation
    adaptationDetails.map(action => {
      let index = adaptationDetails.indexOf(action) + 1

      steps.push({
        title: `Adaptation #${index} - Details`,
        backAction: "Actions - Overview",
        content: <AdaptationDetailsStep details={action} />,
        error: false
      })
      steps.push({
        title: `Adaptation #${index} - Contact`,
        content: <AdaptationContactStep details={action} />,
        error: false
      })

      // Optionally add Research
      if (action.ResearchDetail !== null) {
        steps.push({
          title: `Adaptation #${index} - Research`,
          content: <AdaptationResearchStep details={action} stepWizard={this.stepWizard} />,
          error: false
        })
      }

      // Optionally add Funding
      steps.push({
        title: `Adaptation #${index} - Funding`,
        content: <AdaptationFundingDetailStep details={action} />,
        error: false
      })
    })

    //Mitigation
   
    mitigationDetails.map(action => {
      let index = mitigationDetails.indexOf(action) + 1

      steps.push({
        title: `Mitigation #${index} - Details`,
        backAction: "Actions - Overview",
        content: <MitigationDetailsStep details={action} />,
        error: false
      })
      steps.push({
        title: `Mitigation #${index} - Adaptation`,
        backAction: "Actions - Overview",
        content: <AdaptationDetailsStep details={action} />,
        error: false
      })
      steps.push({
        title: `Mitigation #${index} - Approach`,
        backAction: "Actions - Overview",
        content: <MitigationApproachStep details={action} />,
        error: false
      })
      steps.push({
        title: `Mitigation #${index} - Carbon Credits`,
        backAction: "Actions - Overview",
        content: <CarbonCreditStep details={action} />,
        error: false
      })
      steps.push({
        title: `Mitigation #${index} - Location`,
        backAction: "Actions - Overview",
        content: <ProjectLocationStep details={action} />,
        error: false
      })
      steps.push({
        title: `Mitigation #${index} - Funding`,
        backAction: "Actions - Overview",
        content: <FundingDetailStep details={action} />,
        error: false
      })
      steps.push({
        title: `Mitigation #${index} - Emissions Calculator`,
        backAction: "Actions - Overview",
        content: <EmissionsCalculatorStep details={action} />,
        error: false
      })
   
    })

    //Validate inputs before summary
    this.validateInputs()

    //Verify
    if (_gf.IsReviewer(user)) {
      steps.push({
        title: 'Review - Verify',
        content: <ProjectVerifyStep />,
        error: false
      })
    }

    //Summary
    steps.push({
      title: 'Review - Summary',
      content: <OverallSummaryStep
        header={<h6><i>Please review your entry before submitting. Once the project has been submitted, it will be reviewed by the Department of Environmental Affairs prior to being added to the database and any queries will be sent to the project manger details listed on page 4. If you would like proof of submission, please download your entry by selecting the 'Download' option above.</i></h6>}
        projectDetails={projectDetails}
        adaptationDetails={adaptationDetails}
        funderDetails={projectFunderDetails}
        mitigationDetails={mitigationDetails}
        errors={steps.filter(s => s.error === true).length > 0}
      />,
      error: false
    })

    // return steps
  }

  validateInputs() {

    let { projectFunderDetails, projectDetails, adaptationDetails, mitigationDetails } = this.props
    let { currentStep } = this.state

    let uiconf = {}
    let step = {}

    //PROJECT - DETAILS//
    step = this.getStepByTitle("Project - Details")
    if (step && steps.indexOf(step) < currentStep) {
      let stepValidations = []

      stepValidations.push(this.validateRequiredInput("txtProjectTitle", projectDetails, "ProjectTitle"))
      stepValidations.push(this.validateRequiredInput("txtProjectDescription", projectDetails, "ProjectDescription"))
      stepValidations.push(this.validateRequiredInput("txtProjectLink", projectDetails, "Link"))
      stepValidations.push(this.validateRequiredInput("txtProjectYear", projectDetails, "StartYear"))
      stepValidations.push(this.validateRequiredInput("txtProjectYear", projectDetails, "EndYear"))
      stepValidations.push(this.validateRequiredInput("selProjectStatus", projectDetails, "ProjectStatusId"))
      stepValidations.push(this.validateRequiredInput("txtProjectBudget", projectDetails, "BudgetLower"))
      stepValidations.push(this.validateRequiredInput("txtProjectBudget", projectDetails, "BudgetUpper"))

      step.error = stepValidations.includes(false)
    }

    //PROJECT - LOCATION//
    step = this.getStepByTitle("Project - Location")
    if (step && steps.indexOf(step) < currentStep) {
      let stepValidations = []

      stepValidations.push(this.validateRequiredInput("lblRegions", projectDetails, "ProjectRegions"))
      stepValidations.push(this.validateRequiredInput("lblLocations", projectDetails, "ProjectLocations"))

      step.error = stepValidations.includes(false)
    }

    //PROJECT - MANAGER//
    step = this.getStepByTitle("Project - Manager")
    if (step && steps.indexOf(step) < currentStep) {
      let stepValidations = []

      stepValidations.push(this.validateRequiredInput("txtProjectLeadAgent", projectDetails, "LeadAgent"))
      stepValidations.push(this.validateRequiredInput("selProjectManager", projectDetails, "ProjectManagerId"))
      stepValidations.push(this.validateRequiredInput("txtProjectHostOrganisation", projectDetails, "HostOrganisation"))
      stepValidations.push(this.validateRequiredInput("txtProjectHostPartner", projectDetails, "HostPartner"))
      stepValidations.push(this.validateRequiredInput("txtProjectAlternativeContact", projectDetails, "AlternativeContact"))
      stepValidations.push(this.validateRequiredInput("txtProjectAlternativeContactEmail", projectDetails, "AlternativeContactEmail"))

      step.error = stepValidations.includes(false)
    }

    //FUNDING//
    projectFunderDetails.map(pfd => {
      let index = projectFunderDetails.indexOf(pfd) + 1

      //FUNDING #XX - DETAILS//
      step = this.getStepByTitle(`Funding #${index} - Details`)
      if (step && steps.indexOf(step) < currentStep) {
        let stepValidations = []

        stepValidations.push(this.validateRequiredInput("lblGrantProgram", pfd, "GrantProgName"))
        stepValidations.push(this.validateRequiredInput("lblFundingAgency", pfd, "FundingAgency"))
        stepValidations.push(this.validateRequiredInput("lblPartneringDepts", pfd, "PartnerDepsOrgs"))
        stepValidations.push(this.validateRequiredInput("lblProjectCoordinator", pfd, "ProjectCoordinatorId"))
        stepValidations.push(this.validateRequiredInput("lblTotalBudget", pfd, "TotalBudget"))
        stepValidations.push(this.validateRequiredInput("lblAnnualBudget", pfd, "AnnualBudget"))
        stepValidations.push(this.validateRequiredInput("lblFundingStatus", pfd, "FundingStatusId"))

        step.error = stepValidations.includes(false)
      }
    })

    //ADAPTATION//
    adaptationDetails.map(ad => {
      let index = adaptationDetails.indexOf(ad) + 1

      //ADAPTATION #XX - DETAILS//
      step = this.getStepByTitle(`Adaptation #${index} - Details`)
      if (step && steps.indexOf(step) < currentStep) {
        let stepValidations = []

        stepValidations.push(this.validateRequiredInput("txtAdaptationTitle", ad, "Title"))
        stepValidations.push(this.validateRequiredInput("txtAdaptationDescription", ad, "Description"))
        stepValidations.push(this.validateRequiredInput("selAdaptationPurpose", ad, "AdaptationPurposeId"))
        stepValidations.push(this.validateRequiredInput("selAdaptationSector", ad, "SectorId"))
        stepValidations.push(this.validateRequiredInput("selAdaptationHazard", ad, "HazardId"))
        stepValidations.push(this.validateRequiredInput("selAdaptationActionStatus", ad, "ProjectStatusId"))

        step.error = stepValidations.includes(false)
      }

      //ADAPTATION #XX - CONTACT//
      step = this.getStepByTitle(`Adaptation #${index} - Contact`)
      if (step && steps.indexOf(step) < currentStep) {
        let stepValidations = []

        stepValidations.push(this.validateRequiredInput("txtAdaptationContactName", ad, "ContactName"))
        stepValidations.push(this.validateRequiredInput("txtAdaptationContactEmail", ad, "ContactEmail"))

        step.error = stepValidations.includes(false)
      }
    })

    //MITIGATION//
    mitigationDetails.map(ad => {
      let index = adaptationDetails.indexOf(ad) + 1

      //Mitigation ## details //
      step = this.getStepByTitle(`Adaptation #${index} - Details`)
     
      if (step && steps.indexOf(step) < currentStep) {
        
        let stepValidations = []

        stepValidations.push(this.validateRequiredInput("txtMitigationTitle", ad, "Title"))
        stepValidations.push(this.validateRequiredInput("txtMitigationDescription", ad, "Description"))
        stepValidations.push(this.validateRequiredInput("selMitigationPurpose", ad, "MitigationPurposeId"))
        stepValidations.push(this.validateRequiredInput("selMitigationSector", ad, "SectorId"))
        stepValidations.push(this.validateRequiredInput("selMitigationHazard", ad, "HazardId"))
        stepValidations.push(this.validateRequiredInput("selMitigationActionStatus", ad, "ProjectStatusId"))

        step.error = stepValidations.includes(false)
      }
      //MITIGATION #XX - CONTACT//
      step = this.getStepByTitle(`Mitigation #${index} - Contact`)
      if (step && steps.indexOf(step) < currentStep) {
        let stepValidations = []

        stepValidations.push(this.validateRequiredInput("txtMitigationContactName", ad, "ContactName"))
        stepValidations.push(this.validateRequiredInput("txtMitigationContactEmail", ad, "ContactEmail"))

        step.error = stepValidations.includes(false)
      }
    })
  }



  validateRequiredInput(id, data, key) {
    if (id && data && key) {
      let uiconf = UILookup(id)
      if (uiconf.required === true && (_gf.isEmptyValue(data[key]) || data[key] === 0 || data[key].length === 0)) {
        return false
      }
    }
    return true
  }

  getStepByTitle(stepTitle) {
    let filteredSteps = steps.filter(s => s.title === stepTitle)
    if (filteredSteps && filteredSteps.length > 0) {
      return filteredSteps[0]
    }
  }

  renderListEditor() {

    let { editListModalType, editListModalShow } = this.props

    if (editListModalShow === true) {
      if (editListModalType === "std") {
        return <EditListModal />
      }
      else if (editListModalType === "tree") {
        return <EditTreeModal />
      }
    }
  }

  render() {

    let { winHeight, currentStep, progressCompleteOverride } = this.state
    let { projectDetails, projectFunderDetails, adaptationDetails, mitigationDetails, MitigationEmissionsData } = this.props

    this.getSteps()
    let errors = steps.filter(s => s.error === true).length > 0

    if (steps.length === 0) {
      return (
        <h6> LOADING... </h6>
      )
    }

    return (
      <div style={{ height: "93vh", overflowY: "hidden", overflowX: 'hidden' }}>
        <Row>
          <Col md="3">
            <div style={{ height: (winHeight - 200), maxHeight: (winHeight - 200), overflowY: "auto", overflowX: 'hidden' }}>
              <Steps direction="vertical" current={currentStep} >
                {steps.map(item => {
                  return <Step
                    key={item.title}
                    title={<a href="#">{item.title}</a>}
                    icon={
                      item.error ?
                        <div style={{ fontSize: '32px', color: "red", marginTop: -8, marginLeft: -1 }}>
                          <Icon type="close-circle" />
                        </div> :
                        undefined
                    }
                    onClick={() => {
                      this.setState({ currentStep: steps.indexOf(item) })
                    }}
                  />
                })}
              </Steps>
            </div>

            <br />

            <div style={{ textAlign: "center" }}>
              <Progress
                type="circle"
                percent={progressCompleteOverride ? 100 : Math.round(100 / steps.length * currentStep)}
                style={{ marginLeft: -10 }}
                strokeColor={DEAGreen}
                format={percent => {
                  if (percent < 100) {
                    return (
                      <span style={{ color: DEAGreen }}>
                        {`${percent}%`}
                      </span>
                    )
                  }
                  else {
                    return (
                      <Fa size="2x" icon="check" style={{ color: DEAGreen }} />
                    )
                  }
                }}
              />
            </div>
          </Col>

          <Col md="9" style={{
            borderLeft: "1px solid gainsboro",
          }}>

            <Row>
              <Col>
                <div className="steps-content" style={{
                  height: (winHeight - 113),
                  overflowY: "auto",
                  overflowX: 'hidden'
                }}>
                  <h3>
                    <b>{steps[currentStep].title}</b>
                  </h3>
                  {
                    steps[currentStep].optional === true &&
                    <h6 style={{ color: DEAGreen }}>
                      <i>
                        This step is optional, click 'Next' to skip
                    </i>
                    </h6>
                  }
                  {
                    steps[currentStep].backAction &&
                    <h6>
                      <a href="#" onClick={() => this.jumpTo(steps[currentStep].backAction)} style={{ color: DEAGreenDark }}>
                        <Fa size="lg" className="button-icon" icon="chevron-circle-left" />
                        <u>{steps[currentStep].backAction}</u>
                      </a>
                    </h6>
                  }
                  <div className="vertical-spacer" />
                  {steps[currentStep].content}
                </div>
              </Col>
            </Row>

            <Row style={{ borderTop: "1px solid gainsboro", paddingTop: 5 }}>
              <Col>

                <Button size="sm" color="grey" style={{ width: 120 }} onClick={() => {
                  this.showConfirm("Confirm cancel", "Are you sure you want to cancel and discard all unsaved changes?",
                    "Yes", "No", this.onClose)
                }}>
                  <Fa icon="trash" style={{ marginRight: 10 }} />
                  Cancel
                </Button>

                <div className="steps-action" style={{ float: "right" }} >
                  {
                    currentStep > 0 && (
                      <Button
                        size="sm"
                        color=""
                        style={{ width: 120, marginRight: 0, backgroundColor: DEAGreen }}
                        onClick={() => this.onPrev()}
                      >
                        <Fa icon="chevron-circle-left" style={{ marginRight: 10 }} />
                        Previous
                        </Button>
                    )
                  }

                  {
                    currentStep < steps.length - 1 &&
                    <Button
                      size="sm"
                      color=""
                      style={{ width: 120, marginRight: 0, backgroundColor: DEAGreen }}
                      onClick={() => this.onNext()}
                    >
                      <Fa icon="chevron-circle-right" style={{ marginRight: 10 }} />
                      Next
                      </Button>
                  }

                  {
                    (currentStep === steps.length - 1) &&
                    <Button
                      disabled={errors}
                      size="sm"
                      color=""
                      onClick={() => {

                        this.showConfirm("Confirm submit", "Are you sure you want to save your changes?",
                          "Yes", "No", this.onSubmit)

                        // let projectChanged = projectDetails.state === "modified"
                        // let fundersChanged = projectFunderDetails.filter(x => x.state === "modified").length > 0
                        // let adaptationsChanged = adaptationDetails.filter(x => x.state === "modified").length > 0

                        // if (projectChanged || fundersChanged || adaptationsChanged) {
                        //   this.showConfirm("Confirm submit", "Are you sure you want to save your changes?",
                        //     "Yes", "No", this.onSubmit)
                        // }
                        // else {
                        //   this.showConfirm("Confirm close", "You have not made any changes, would you like to close the form?",
                        //     "Yes", "No", this.onClose)
                        // }
                      }}
                      style={{ width: 120, marginRight: 0, backgroundColor: DEAGreenDark }}
                    >
                      <Fa icon="save" style={{ marginRight: 10 }} />
                      Submit
                    </Button>
                  }
                </div>
              </Col>
            </Row>

          </Col>
        </Row>

        {this.renderListEditor()}

      </div>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SteppedInputForm)