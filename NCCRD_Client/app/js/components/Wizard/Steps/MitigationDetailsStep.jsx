import React from 'react'
import { connect } from 'react-redux'
import { Button, Row, Col, Fa, Badge } from 'mdbreact'
import TextComponent from '../../Shared/TextComponent.jsx';
import TextAreaComponent from '../../Shared/TextAreaComponent.jsx';
import SelectComponent from '../../Shared/SelectComponent.jsx';
import TreeSelectComponent from '../../Shared/TreeSelectComponent.jsx';
import { Popover } from 'antd'

import "./shared.css"

const mapStateToProps = (state, props) => {
  let { mitigationData: { mitigationDetails } } = state
  let { lookupData: { mitigationPurpose, sector, sectorType, typology, hazards, projectStatus } } = state
  return { mitigationDetails, mitigationPurpose, sector, sectorType, typology, hazards, projectStatus }
}

const mapDispatchToProps = (dispatch) => {
  return {
    removeMitigationAction: payload => {
      dispatch({ type: "REMOVE_MITIGATION_DETAILS", payload })
    },
    addMitigationDetailsResearchDetails: payload => {
      dispatch({ type: "ADD_MITIGATION_DETAILS_RESEARCH_DETAILS", payload })
    }
  }
}

class MitigationDetailsStep extends React.Component {

  constructor(props) {
    super(props);

    this.onAdd = this.onAdd.bind(this)
    this.onRemove = this.onRemove.bind(this)
  }

  onAdd() {

    let { addMitigationDetailsResearchDetails, details } = this.props

    //Add mitigation action
    addMitigationDetailsResearchDetails({
      id: details.MitigationDetailId,
      state: 'modified'
    })
  }

  onRemove() {

    let { mitigationDetails, details, removeMitigationAction } = this.props

    //Remove mitigation action
    let actionIndex = mitigationDetails.indexOf(details)
    removeMitigationAction(actionIndex)
  }

  render() {

    let { details, mitigationPurpose, sector, sectorType, typology, hazards, projectStatus } = this.props

    return (
      <>
        <Row>
          <TextAreaComponent
            col="col-md-12"
            label="Title:"
            id="txtMitigationTitle"
            value={details.Title}
            setValueKey={"SET_MITIGATION_DETAILS_TITLE"}
            parentId={details.MitigationDetailId}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextAreaComponent
            col="col-md-12"
            label="Description:"
            id="txtMitigationDescription"
            value={details.Description}
            setValueKey={"SET_MITIGATION_DETAILS_DESCR"}
            parentId={details.MitigationDetailId}
            rows={3}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <SelectComponent
            id="selMitigationPurpose"
            col="col-md-6"
            label="Purpose:"
            readOnly="true"
            selectedValue={details.MitigationPurposeId}
            data={mitigationPurpose}
            setSelectedValueKey={"SET_MITIGATION_DETAILS_PURPOSE"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_MITIGATION_PURPOSE"}
            persist="MitigationPurpose"
            allowEdit={true}
            newItemTemplate={{
              "MitigationPurposeId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TreeSelectComponent
            id="selMitigationSector"
            col="col-md-6"
            label="Sector:"
            selectedValue={details.SectorId}
            data={sector}
            setSelectedValueKey={"SET_MITIGATION_DETAILS_SECTOR"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_SECTOR"}
            persist="Sector"
            type="tree"
            dependencies={[
              { key: "SectorTypeId", value: sectorType, type: "std" },
              { key: "ParentSectorId", value: sector, type: "tree" },
              { key: "TypologyId", value: typology, type: "std" }
            ]}
            allowEdit={true}
            newItemTemplate={{
              "SectorId": 0,
              "Value": "",
              "SectorTypeId": 0,
              "ParentSectorId": 0,
              "TypologyId": 0
            }}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TreeSelectComponent
            id="selMitigationHazard"
            col="col-md-6"
            label="Hazard:"
            placeholder="Select..."
            // disabled
            selectedValue={details.HazardId} 
            data={hazards}
            setSelectedValueKey={"SET_MITIGATION_DETAILS_HAZARD"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_HAZARDS"}
            type="tree"
            allowEdit={false}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <SelectComponent
            id="selMitigationActionStatus"
            col="col-md-6"
            label="Status:"
            selectedValue={details.ProjectStatusId}
            data={projectStatus}
            setSelectedValueKey={"SET_MITIGATION_DETAILS_PROJECT_STATUS"}
            parentId={details.MitigationDetailId}
            allowEdit={false}
            allowClear={true}
          />
        </Row>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MitigationDetailsStep)