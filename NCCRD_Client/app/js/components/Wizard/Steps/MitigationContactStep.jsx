import React from 'react'
import { connect } from 'react-redux'
import { Button, Row, Col, Fa, Badge } from 'mdbreact'
import TextComponent from '../../Shared/TextComponent.jsx';
import TextAreaComponent from '../../Shared/TextAreaComponent.jsx';
import SelectComponent from '../../Shared/SelectComponent.jsx';
import TreeSelectComponent from '../../Shared/TreeSelectComponent.jsx';

import "./shared.css"

const mapStateToProps = (state, props) => {
  let { mitigationData: { mitigationDetails } } = state
  let { lookupData: { mitigationPurpose, sector, sectorType, typology, hazards, projectStatus } } = state
  return { mitigationDetails, mitigationPurpose, sector, sectorType, typology, hazards, projectStatus }
}

const mapDispatchToProps = (dispatch) => {
  return {}
}

class MitigationContactStep extends React.Component {

  constructor(props) {
    super(props);
  }

  render() {

    let { details } = this.props

    return (
      <>
        <Row>
        <TextComponent
            col="col-md-6"
            label="Contact Name:"
            id="txtMitigationContactName"
            value={details.ContactName}
            setValueKey={"SET_MITIGATION_DETAILS_CONTACT_NAME"}
            parentId={details.MitigationDetailId}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
        <TextComponent
            col="col-md-6"
            label="Contact Email Address:"
            id="txtMitigationContactEmail"
            value={details.ContactEmail}
            setValueKey={"SET_MITIGATION_DETAILS_CONTACT_EMAIL"}
            parentId={details.MitigationDetailId}
          />
        </Row>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MitigationContactStep)