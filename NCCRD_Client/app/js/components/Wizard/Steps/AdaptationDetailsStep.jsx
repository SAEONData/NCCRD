import React from 'react'
import { connect } from 'react-redux'
import { Button, Row, Col, Fa, Badge } from 'mdbreact'
import TextComponent from '../../Shared/TextComponent.jsx';
import TextAreaComponent from '../../Shared/TextAreaComponent.jsx';
import SelectComponent from '../../Shared/SelectComponent.jsx';
import TreeSelectComponent from '../../Shared/TreeSelectComponent.jsx';


import "./shared.css"

const mapStateToProps = (state, props) => {
  let { adaptationData: { adaptationDetails } } = state
  let { lookupData: { adaptationPurpose, sector, sectorType, typology, hazards, projectStatus } } = state
  return { adaptationDetails, adaptationPurpose, sector, sectorType, typology, hazards, projectStatus }
}

const mapDispatchToProps = (dispatch) => {
  return {
    removeAdaptationAction: payload => {
      dispatch({ type: "REMOVE_ADAPTATION_DETAILS", payload })
    },
    addAdaptationDetailsResearchDetails: payload => {
      dispatch({ type: "ADD_ADAPTATION_DETAILS_RESEARCH_DETAILS", payload })
    }
  }
}

class AdaptationDetailsStep extends React.Component {

  constructor(props) {
    super(props);

    this.onAdd = this.onAdd.bind(this)
    this.onRemove = this.onRemove.bind(this)
  }

  onAdd() {

    let { addAdaptationDetailsResearchDetails } = this.props

    //Add adaptation action
    addAdaptationDetailsResearchDetails({
      id: details.AdaptationDetailId,
      state: 'modified'
    })
  }

  onRemove() {

    let { adaptationDetails, details, removeAdaptationAction } = this.props

    //Remove adaptation action
    let actionIndex = adaptationDetails.indexOf(details)
    removeAdaptationAction(actionIndex)
  }

  render() {

    let { details, adaptationPurpose, sector, sectorType, typology, hazards, projectStatus } = this.props

    return (
      <>
        <Row>
          <TextAreaComponent
            col="col-md-12"
            label="Title:"
            id="txtAdaptationTitle"
            value={details.Title}
            setValueKey={"SET_ADAPTATION_DETAILS_TITLE"}
            parentId={details.AdaptationDetailId}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TextAreaComponent
            col="col-md-12"
            label="Description:"
            id="txtAdaptationDescription"
            value={details.Description}
            setValueKey={"SET_ADAPTATION_DETAILS_DESCR"}
            parentId={details.AdaptationDetailId}
            rows={3}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <SelectComponent
            id="selAdaptationPurpose"
            col="col-md-6"
            label="Purpose:"
            readOnly="true"
            selectedValue={details.AdaptationPurposeId}
            data={adaptationPurpose}
            setSelectedValueKey={"SET_ADAPTATION_DETAILS_PURPOSE"}
            parentId={details.AdaptationDetailId}
            dispatch={"LOAD_ADAPTATION_PURPOSE"}
            persist="AdaptationPurpose"
            allowEdit={false}
            newItemTemplate={{
              "AdaptationPurposeId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <TreeSelectComponent
            id="selAdaptationSector"
            col="col-md-6"
            label="Sector:"
            selectedValue={details.SectorId}
            data={sector}
            setSelectedValueKey={"SET_ADAPTATION_DETAILS_SECTOR"}
            parentId={details.AdaptationDetailId}
            dispatch={"LOAD_SECTOR"}
            persist="Sector"
            type="tree"
            dependencies={[
              { key: "SectorTypeId", value: sectorType, type: "std" },
              { key: "ParentSectorId", value: sector, type: "tree" },
              { key: "TypologyId", value: typology, type: "std" }
            ]}
            allowEdit={false}
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
            id="selAdaptationHazard"
            col="col-md-6"
            label="Hazard:"
            placeholder="Select..."
            selectedValue={details.HazardId} 
            data={hazards}
            setSelectedValueKey={"SET_ADAPTATION_DETAILS_HAZARD"}
            parentId={details.AdaptationDetailId}
            dispatch={"LOAD_HAZARDS"}
            type="tree"
            allowEdit={false}
          />
        </Row>

        <div className="vertical-spacer" />

        <Row>
          <SelectComponent
            id="selAdaptationActionStatus"
            col="col-md-6"
            label="Status:"
            selectedValue={details.ProjectStatusId}
            data={projectStatus}
            setSelectedValueKey={"SET_ADAPTATION_DETAILS_PROJECT_STATUS"}
            parentId={details.AdaptationDetailId}
            allowEdit={false}
            allowClear={true}
          />
        </Row>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(AdaptationDetailsStep)