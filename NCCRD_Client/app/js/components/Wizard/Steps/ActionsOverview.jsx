import React from 'react'
import { Button, Fa } from 'mdbreact'
import { Select, Checkbox } from 'antd'
import { connect } from 'react-redux'
import { DEAGreen } from '../../../config/colours'

import {ProjectLocationStep} from './ProjectLocationStep.jsx'


import './shared.css'
import './ActionsOverview.css'

const Option = Select.Option;

const mapStateToProps = (state, props) => {

  let { adaptationData: { adaptationDetails } } = state
  let { projectFundersData: { projectFunderDetails } } = state
  // let { locationData: { locationDetails } } = state
  let { mitigationData: { mitigationDetails } } = state

  return { projectFunderDetails, adaptationDetails, mitigationDetails }
}

const mapDispatchToProps = (dispatch) => {

  return {

    addAdaptationDetails: payload => {
      dispatch({ type: "ADD_ADAPTATION_DETAILS", payload })
    },
    removeAdaptationDetails: payload => {
      dispatch({ type: "REMOVE_ADAPTATION_DETAILS", payload })
    },
    addAdaptationDetailsResearchDetails: payload => {
      dispatch({ type: "ADD_ADAPTATION_DETAILS_RESEARCH_DETAILS", payload })
    },
    removeAdaptationDetailsResearchDetails: payload => {
      dispatch({ type: "SET_ADAPTATION_DETAILS_RESEARCH_DETAILS", payload })
    },
    addProjectFunderDetails: payload => {
      dispatch({ type: "ADD_PROJECTFUNDER_DETAILS", payload })
    },
    removeFundingAction: payload => {
      dispatch({ type: "REMOVE_PROJECTFUNDER_DETAILS", payload })
    },
    // addLocationDetails: payload => {
    //   dispatch({ type: "ADD_LOCATION_DETAILS", payload })
    // },
    // removeLocationDetails: payload => {
    //   dispatch({ type: "REMOVE_LOCATION_DETAILS", payload })
    // },
    addMitigationDetails: payload => {
      dispatch({ type: "ADD_MITIGATION_DETAILS", payload })
    },
    addMitigationDetailsResearchDetails: payload => {
      dispatch({ type: "ADD_MITIGATION_DETAILS_RESEARCH_DETAILS", payload })
    },
    removeMitigationDetails: payload => {
      dispatch({ type: "REMOVE_MITIGATION_DETAILS", payload })
    },
    removeMitigationDetailsResearchDetails: payload => {
      dispatch({ type: "SET_MITIGATION_DETAILS_RESEARCH_DETAILS", payload })
    },
  
  }
}

class ActionsOverview extends React.Component {

  constructor(props) {
    super(props);

    this.addFunding = this.addFunding.bind(this)
    this.addAdaptation = this.addAdaptation.bind(this)
    this.addMitigation = this.addMitigation.bind(this)
    // this.addLocation = this.addLocation.bind(this)
    this.constructActionsTable = this.constructActionsTable.bind(this)
    this.onImplementationChange = this.onImplementationChange.bind(this)
    this.onEdit = this.onEdit.bind(this)
  }

  addAdaptation() {
    let { addAdaptationDetails } = this.props
    addAdaptationDetails()
  }
  
  addMitigation() {
    let { addMitigationDetails } = this.props
    addMitigationDetails()
  }

  addFunding() {
    let { projectFunderDetails, addProjectFunderDetails } = this.props
    addProjectFunderDetails(projectFunderDetails.ProjectId)
  }

  // addLocation() {
  //   let { locationDetails, addLocationDetails } = this.props
  //   addLocationDetails(locationDetails.ProjectId)
  // }

  constructActionsTable() {

    let { projectFunderDetails, adaptationDetails, mitigationDetails } = this.props

    return (
      <table width="100%">
        <thead>
          <tr>
            <td className="table-cell table-side table-head">Title</td>
            <td className="table-side table-cell table-head">Type</td>
            <td className="table-side table-cell table-head">Implementation</td>
            {/* <td className="table-side table-cell table-head">Location</td> */}
            <td className="table-side table-cell table-head">
              Funding status
            {/* <br style={{ marginTop: 0, marginBottom: 0 }}/> */}
              {/* <div style={{ fontSize: "10px", marginTop: -5 }}>*coming soon*</div> */}
            </td>
            <td className="table-cell table-head">Options</td>
          </tr>
        </thead>
        <tbody>

          {/* Funding */}
          {projectFunderDetails.sort((a, b) => a.FunderId > b.FunderId ? 1 : 0).map(f => {
            let index = projectFunderDetails.indexOf(f) + 1
            return this.createTableEntry("Funding", `Funding #${index}`, "Applied", f.FunderId)
          })}

          {/* Adaptations */}
          {adaptationDetails.sort((a, b) => a.AdaptationDetailId > b.AdaptationDetailId ? 1 : 0).map(a => {
            let index = adaptationDetails.indexOf(a) + 1
            return this.createTableEntry(
              "Adaptation",
              `Adaptation #${index}`,
              a.ResearchDetail === null ? "Applied" : "Research",
              a.AdaptationDetailId
            )
          })}

          {/* Mitigation */}
          {mitigationDetails.sort((a, b) => a.MitigationDetailId > b.MitigationDetailId ? 1: 0).map(a => {
            let index = adaptationDetails.indexOf(a) + 1
            return this.createTableEntry(
              'Mitigation',
              `Mitigation #${index}`,
              a.ResearchDetail === null ? 'Applied' : 'Research',
              a.MitigationDetailId
            )
          })}

          {/* Location */}
          {/* TODO - finish integratino of locationstep to actions overview */}
          {/* {locationDetails.sort((a, b) => a.LocationDetailId > b.LocationDetailId ? 1:0).map(l => {
            let index = adaptationDetails.indexOf(a) + 1
            return this.createTableEntry(
              'Location',
              `Location #${index}`,
              l.LocationId
            )
          })} */}

        </tbody>
      </table>
    )
  }

  createTableEntry(type, title, implementation, id) {
    return (
      <tr key={title}>
        <td className="table-side table-cell">
          {title}
        </td>
        <td className="table-side table-cell">
          {type}
        </td>
        <td className="table-side table-cell">
          {
            type === "Funding" &&
            implementation
          }
          {
            type !== "Funding" &&
            <Select defaultValue={implementation} onChange={(value, option) => this.onImplementationChange(value, option, type, id)}>
              <Option value="Applied">Applied</Option>
              <Option value="Research">Research</Option>
            </Select>
          }
        </td>
        <td className="table-side table-cell">
          <Checkbox disabled /*onChange={onChange}*/>(No)</Checkbox>
        </td>
        <td className="table-cell">
          {/* <Button
            className="table-button"
            size="sm"
            color=""
            onClick={() => { this.onEdit(title) }}
            style={{ backgroundColor: DEAGreen }}
          >
            <Fa className="button-icon" icon="pencil" />
            Edit
          </Button> */}
          <Button
            className="table-button"
            size="sm"
            color="grey"
            onClick={() => this.onActionRemove(type, id)}
          >
            <Fa className="button-icon" icon="trash" />
            Remove
          </Button>
        </td>
      </tr>
    )
  }

  onImplementationChange(value, option, type, id) {

    if (type === "Adaptation") {
      if (value === "Research") {
        this.props.addAdaptationDetailsResearchDetails({
          id: id,
          state: 'modified'
        })
      }
      else {
        this.props.removeAdaptationDetailsResearchDetails({
          id: id,
          value: null,
          state: 'modified'
        })
      }
    }
    else if (type === "Mitigation") {
      if (value === 'Research') {
        this.props.addMitigationDetailsResearchDetails({
          id: id,
          state: 'modified'
        })
      }
    }
    else {
      this.props.removeMitigationDetailsResearchDetails({
        id: id,
        value: null,
        state: 'modified'
      })
    }
  }

  onActionRemove(type, id) {
    if (type === "Funding") {
      this.props.removeFundingAction({
        id,
        state: 'modified'
      })
    }
   if (type === "Adaptation") {
      this.props.removeAdaptationDetails({
        id,
        state: 'modified'
      })
    }
    if (type === "Mitigation") {
      this.props.removeMitigationDetails({
        id,
        state: 'modified'
      })
    }
    // if (type === "Location") {
    //   this.props.removeLocationDetails({
    //     id,
    //     state: 'modified'
    //   })
    // }
  }

  onEdit(title) {
    let { jumpTo } = this.props
    if (jumpTo) {
      jumpTo(title + " - Details")
    }
  }

  render() {

    let { projectFunderDetails, adaptationDetails, mitigationDetails } = this.props

    return (
      <>
        <h5>
          Would you like to add any actions to this project?
        </h5>

        <div className="vertical-spacer" />

        {/* <Button className="inline-button add-btn" color="" onClick={this.addFunding} style={{ backgroundColor: DEAGreen }}>
          <Fa className="button-icon" icon="plus" />
          Add Funding
        </Button> */}

        <Button className="inline-button add-btn" color="" onClick={this.addAdaptation} style={{ backgroundColor: DEAGreen }}>
          <Fa className="button-icon" icon="plus" />
          Add Adaptation
        </Button>

        <Button className="inline-button add-btn-special" color="" onClick={this.addMitigation} style={{ backgroundColor: DEAGreen }}>
          <Fa className="button-icon" icon="plus" />
          Add Mitigation
          {/* <div style={{ fontSize: "10px", marginTop: -1 }}>*coming soon*</div> */}
        </Button>
{/* 
        <Button className="inline-button add-btn-special" color="" onClick={this.addLocation} style={{ backgroundColor: DEAGreen }}>
          <Fa className="button-icon" icon="plus" />
          Add Location
        </Button> */}

        <div className="vertical-spacer" />
        <div className="vertical-spacer" />

        {
          (projectFunderDetails.length + adaptationDetails.length + mitigationDetails.length ) > 0 &&
          <div>
            <h5>
              List of existing actions:
            </h5>

            <div className="vertical-spacer" />

            {this.constructActionsTable()}
          </div>
        }

      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ActionsOverview)