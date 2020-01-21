import React from 'react'
import { Row, Col, Fa, Button } from 'mdbreact'
import { Select, Checkbox } from 'antd'
import { connect } from 'react-redux'
import TextComponent from '../../Shared/TextComponent.jsx'
import SelectComponent from '../../Shared/SelectComponent.jsx'
import { Popover } from 'antd'

import "./shared.css"

const mapStateToProps = (state, props) => {
  let { lookupData: { users, fundingStatus } } = state
  let { projectFundersData: { projectFunderDetails } } = state
  let { onFundingStatSelect: { value } } = props
  return { users, fundingStatus, projectFunderDetails, onFundingStatSelect }
}

const mapDispatchToProps = (dispatch) => {
  return {
    removeFundingAction: payload => {
      dispatch({ type: "REMOVE_PROJECTFUNDER_DETAILS", payload })
    },
    addProjectFunderDetails: payload => {
      dispatch({ type: "ADD_PROJECTFUNDER_DETAILS", payload })
    }
  }
}

class AdaptationFundingDetailStep extends React.Component {

  constructor(props) {
    super(props);

    this.addFunding = this.addFunding.bind(this)

    this.onAdd = this.onAdd.bind(this)
    this.onRemove = this.onRemove.bind(this)
  }

  onFundingStatSelect(type, value, id) {
    if (type === "Adatpation") {
      if (value === "Seeking") {
        this.props.addAdaptationDetailsFundingStatus({
          id: id, 
          state: 'modified'
        })
      }
      else if (type === "Mitigation") {
        if (value === "Funded") {
          this.props.addAdaptationDetailsFundingStatus({
            id: id,
            value: "Funded",
            state: 'modified'
          })
        }
      }
      else if (type === "Mitigation") {
        if (value === "Partial") {
          this.props.addAdaptationDetailsFundingStatus({
            id: id,
            value: "Partial",
            state: 'modified'
          })
        }
      }
      else {
        this.props.removeAdaptationDetailsFundingStatus({
          id: id, 
          value: null, 
          state: 'modified'
        })
      }
    }
    else if (type === "Mitigation") {
      if (value === "Funded") {
        this.props.addMitigationDetailsFundingDetails({
          id: id,
          value: "Funded",
          state: 'modified'
        })
      }
    }
    else if (type === "Mitigation") {
      if (value === "Partial") {
        this.props.addMitigationDetailsFundingDetails({
          id: id,
          value: "Partial",
          state: 'modified'
        })
      }
    }
    else {
      this.props.removeMitigationDetailsFundingDetails({
        id: id,
        value: null,
        state: 'modified'
      })
    }
  }


  addFunding() {
    let { projectFunderDetails, addProjectFunderDetails } = this.props
    addProjectFunderDetails(projectFunderDetails.ProjectId)
  }

  onAdd() {
    let { addFundingAction, details } = this.props
    addFundingAction({
      id: details.AdaptationDetailId, 
      state: 'modified'
    })
  }


  onRemove() {
    let { removeFundingAction, details, projectFunderDetails } = this.props
    let actionIndex = projectFunderDetails.indexOf(details)
    removeFundingAction(actionIndex)
  }

  render() {

    let { details, users, fundingStatus } = this.props

    

    return (
      <>
        <Row>
          
          {/* <SelectComponent
            col="col-md-6"
            id="lblFundingStatus"
            label="Funding Status:"
            selectedValue={details.FundingStatusId}
            data={fundingStatus}
            setSelectedValueKey={"SET_ADAPTATION_FUNDING_DETAIL"}
            parentId={details.FunderId}
            dispatch={"LOAD_FUNDINGSTATUS"}
            persist="FundingStatus"
            allowEdit={false}
            newItemTemplate={{
              "Id": 0,
              "Value": "",
              "Description": ""
            }}
            editModeOverride={true}
            allowClear={true}
          /> */}

            <Select 
              col="col-md-6"
              defaultValue={onFundingStatSelect.value} 
              onChange={(value, option) => this.onFundingStatSelect(value, option, type, id)}
            >
              <Option value="Funded">Funded</Option>
              <Option value="Partial">Partial</Option>
              <Option value="Seeking">Seeking</Option>
            </Select>
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            id="lblGrantProgram"
            label="Grant or Program name:"
            value={details.GrantProgName}
            setValueKey="SET_PROJECTFUNDER_GRANTPROGNAME"
            parentId={details.FunderId}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            id="lblFundingAgency"
            label="Funding Agency:"
            value={details.FundingAgency}
            setValueKey="SET_PROJECTFUNDER_FUNDINGAGENCY"
            parentId={details.FunderId}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            id="lblPartneringDepts"
            label="Partnering Departments/Organisations:"
            value={details.PartnerDepsOrgs}
            setValueKey="SET_PROJECTFUNDER_PARTNERDEPSORGS"
            parentId={details.FunderId}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <SelectComponent
            col="col-md-6"
            id="lblProjectCoordinator"
            label="Project Coordinator:"
            readOnly={true}
            selectedValue={details.ProjectCoordinatorId}
            data={users.map(x => { return { Id: x.PersonId, Value: (x.FirstName + " " + x.Surname + " (" + x.EmailAddress + ")") } })}
            setSelectedValueKey={"SET_PROJECTFUNDERS_PROJECTCOORDINATOR"}
            parentId={details.AdaptationId}
            dispatch={"LOAD_PROJECTFUNDERS_PROJECTCOORDINATOR"}
            persist="ProjectCoordinator"
            allowEdit={true}
            newItemTemplate={{
              "Id": 0,
              "Value": "",
              "Description": ""
            }}
            editModeOverride={true}
            allowClear={true}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            id="lblTotalBudget"
            label="Total Budget:"
            value={details.TotalBudget}
            setValueKey="SET_PROJECTFUNDER_TOTALBUDGET"
            parentId={details.FunderId}
            numeric={true}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            id="lblAnnualBudget"
            label="Annual Budget:"
            value={details.AnnualBudget}
            setValueKey="SET_PROJECTFUNDER_ANNUALBUDGET"
            parentId={details.FunderId}
            numeric={true}
          />
        </Row>

        <div className="vertical-spacer" />

        {/* <Row>
          <SelectComponent
            col="col-md-6"
            id="lblFundingStatus"
            label="Funding Status:"
            selectedValue={details.FundingStatusId}
            data={fundingStatus}
            setSelectedValueKey={"SET_PROJECTFUNDERS_FUNDINGSTATUS"}
            parentId={details.FunderId}
            dispatch={"LOAD_PROJECTFUNDERS_FUNDINGSTATUS"}
            persist="FundingStatus"
            allowEdit={false}
            newItemTemplate={{
              "Id": 0,
              "Value": "",
              "Description": ""
            }}
            editModeOverride={true}
            allowClear={false}
          />
        </Row> */}

        {/* <div className="vertical-spacer" />

        <Row>
          <Col>
            <Popover content={"Remove this funding action completely"}>
              <Button className="inline-button" color="danger" onClick={this.onRemove}>
                <Fa className="button-icon" icon="minus-circle" />
                Remove funding
              </Button>
            </Popover>
          </Col>
        </Row> */}
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(AdaptationFundingDetailStep)