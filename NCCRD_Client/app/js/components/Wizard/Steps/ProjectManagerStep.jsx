import React from 'react'
import { connect } from 'react-redux'
import { Button, Row, Col, Fa, Badge } from 'mdbreact'
import TextComponent from '../../Shared/TextComponent.jsx';
import SelectComponent from '../../Shared/SelectComponent.jsx';

import "./shared.css"

const mapStateToProps = (state, props) => {
  let { projectData: { projectDetails } } = state
  let { lookupData: { users } } = state
  return { projectDetails, users }
}

const mapDispatchToProps = (dispatch) => {
  return {}
}

class ProjectManagerStep extends React.Component {

  constructor(props) {
    super(props);
  }

  render() {

    let { projectDetails, users } = this.props

    return (
      <>
        <Row>
          <TextComponent
            col="col-md-6"
            label="Lead organisation:"
            id="txtProjectLeadAgent"
            value={projectDetails.LeadAgent}
            setValueKey={"SET_PROJECT_DETAILS_LEAD_AGENT"}
          />
        </Row>

        {/* <div className="vertical-spacer" />

        <Row>
          <SelectComponent
            id="selProjectManager"
            col="col-md-6"
            label="Project manager:"
            selectedValue={projectDetails.ProjectManagerId}
            data={users}
            setSelectedValueKey={"SET_PROJECT_DETAILS_PROJECT_MANAGER"}
            allowEdit={true}
            dispatch={"LOAD_USERS"}
            persist="Person"
            newItemTemplate={{
              "PersonId": 0,
              "EmailAddress": "",
              "FirstName": "",
              "Surname": "",
              "Organisation": "",
              "PhoneNumber": "",
              "MobileNumber": ""
            }}
            allowClear={true}
          />
        </Row> */}

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            label="Host organisation:"
            id="txtProjectHostOrganisation"
            value={projectDetails.HostOrganisation}
            setValueKey={"SET_PROJECT_DETAILS_HOST_ORG"}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            readOnly="true"
            label="Host partner:"
            id="txtProjectHostPartner"
            value={projectDetails.HostPartner}
            setValueKey={"SET_PROJECT_DETAILS_HOST_PARTNER"}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            label="Contact (alt):"
            id="txtProjectAlternativeContact"
            value={projectDetails.AlternativeContact}
            setValueKey={"SET_PROJECT_DETAILS_ALT_CONTACT"}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextComponent
            col="col-md-6"
            label="Contact email (alt):"
            id="txtProjectAlternativeContactEmail"
            value={projectDetails.AlternativeContactEmail}
            setValueKey={"SET_PROJECT_DETAILS_ALT_CONTACT_EMAIL"}
          />
        </Row>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProjectManagerStep)