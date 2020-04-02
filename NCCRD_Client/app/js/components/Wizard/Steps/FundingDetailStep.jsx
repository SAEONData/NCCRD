import React from 'react'
import { Row, Col, Fa, Button } from 'mdbreact'
import { connect } from 'react-redux'
import TextComponent from '../../Shared/TextComponent.jsx';
import SelectComponent from '../../Shared/SelectComponent.jsx';
import { Popover } from 'antd'

import "./shared.css"

const mapStateToProps = (state, props) => {
  let { lookupData: { users, fundingStatus } } = state
  let { projectFundersData: { projectFunderDetails } } = state
  return { users, fundingStatus, projectFunderDetails }
}

const mapDispatchToProps = (dispatch) => {
  return {
    removeFundingAction: payload => {
      dispatch({ type: "REMOVE_PROJECTFUNDER_DETAILS", payload })
    }
  }
}

class FundingDetailStep extends React.Component {

  constructor(props) {
    super(props);

    this.onRemove = this.onRemove.bind(this)
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

        {/* <Row>
          <SelectComponent
            col="col-md-6"
            id="lblProjectCoordinator"
            label="Project Coordinator:"
            readOnly={true}
            selectedValue={details.ProjectCoordinatorId}
            data={users.map(x => { return { Id: x.PersonId} })}
            setSelectedValueKey={"SET_PROJECTFUNDERS_PROJECTCOORDINATOR"}
            parentId={details.FunderId}
            dispatch={"LOAD_PROJECTFUNDERS_PROJECTCOORDINATOR"}
            persist="ProjectCoordinator"
            allowEdit={true}
            newItemTemplate={{
              "Id": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
        </Row> */}

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

        <Row>
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
            allowClear={true}
          />
        </Row>

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

export default connect(mapStateToProps, mapDispatchToProps)(FundingDetailStep)