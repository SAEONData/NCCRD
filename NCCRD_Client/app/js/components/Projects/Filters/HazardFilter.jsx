import React from 'react'
import { Button } from 'mdbreact'
import { vmsBaseURL } from "../../../config/serviceURLs.js"
import { connect } from 'react-redux'
import TreeSelectComponent from "../../Shared/TreeSelectComponent.jsx"
import { CustomFetch } from '../../../globalFunctions.js';

//AntD Tree
// import Tree from 'antd/lib/tree'
// import '../../../../css/antd.tree.css' //Overrides default antd.tree css
// const TreeNode = Tree.TreeNode

// const _gf = require("../../../globalFunctions")
// const queryString = require('query-string')
// const o = require("odata")

const mapStateToProps = (state, props) => {
  let { lookupData: { hazards } } = state
  let { filterData: { hazardFilter } } = state
  return { hazardFilter, hazards }
}

const mapDispatchToProps = (dispatch) => {
  return {
    loadHazardFilter: payload => {
      dispatch({ type: "LOAD_HAZARD_FILTER", payload })
    },
    loadHazards: payload => {
      dispatch({ type: "LOAD_HAZARDS", payload })
    }
  }
}

class HazardFilters extends React.Component {

  constructor(props) {
    super(props);
  }

  async componentDidMount() {

    let { loadHazards, hazards } = this.props

    if (hazards.length === 0) {
      //Get data
      try {
        let res = await CustomFetch(vmsBaseURL + "Hazards/flat")

        if (res.ok) {
          res = await res.json()

          if (res.items && res.items.length > 0) {
            let data = res.items
            loadHazards(data)
          }
        }
      }
      catch (ex) {
        console.error(ex)
      }
    }
  }

  render() {

    let { hazards, hazardFilter } = this.props
    // console.log(hazards)

    return (
     
      <>
        <TreeSelectComponent
          col="col-md-2"
          id="treeHazardFilter"
          label="Hazard"
          placeholder="Select..."
          // disabled
          selectedValue={hazardFilter}
          data={hazards}
          editModeOverride={true}
          labelStyle={{
            fontSize: "14px",
            fontWeight: "400",
            color: "grey"
          }}
          style={{
            marginTop: "-4px",
            marginBottom: "15px"
          }}
          setSelectedValueKey="LOAD_HAZARD_FILTER"
        />
        
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(HazardFilters)