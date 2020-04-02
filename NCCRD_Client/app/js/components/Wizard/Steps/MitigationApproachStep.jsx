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
  let { mitigationData: { mitigationApproach } } = state
  let { lookupData: { mitigationPurpose, sector, sectorType, typology, hazards, projectStatus } } = state
  return { mitigationApproach, mitigationPurpose, sector, sectorType, typology, hazards, projectStatus }
}

const mapDispatchToProps = (dispatch) => {
  return {
    removeMitigationAction: payload => {
      dispatch({ type: "REMOVE_MITIGATION_DETAILS", payload })
    },
    addMitigationApproachResearchApproach: payload => {
      dispatch({ type: "ADD_MITIGATION_DETAILS_RESEARCH_DETAILS", payload })
    }
  }
}

class MitigationApproachStep extends React.Component {

  constructor(props) {
    super(props);

    this.onAdd = this.onAdd.bind(this)
    this.onRemove = this.onRemove.bind(this)
  }

  onAdd() {

    let { addMitigationApproachResearchApproach, details } = this.props

    //Add mitigation action
    addMitigationApproachResearchApproach({
      id: details.MitigationDetailId,
      state: 'modified'
    })
  }

  onRemove() {

    let { mitigationApproach, details, removeMitigationAction } = this.props

    //Remove mitigation action
    let actionIndex = mitigationApproach.indexOf(details)
    removeMitigationAction(actionIndex)
  }

  render() {

    let { details, mitigationPurpose, sector, sectorType, typology, hazards, projectStatus } = this.props

    return (
      <>
        <p>CALCULATOR WILL CALCULATE HERE</p>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MitigationApproachStep)