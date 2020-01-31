import React from 'react'
import { Button, Collapse, Fa } from 'mdbreact'
import { connect } from 'react-redux'
import { DEAGreen } from '../../../config/colours.js'

const mapStateToProps = (state, props) => {
  let { filterData: { titleFilter, statusFilter, typologyFilter, sectorFilter, regionFilter, hazardFilter, favoritesFilter, unverifiedOnlyFilter } } = state
  let { lookupData: { projectStatus, typology, sector, region, hazards } } = state
  return {
    titleFilter, statusFilter, typologyFilter, sectorFilter, regionFilter, hazardFilter, projectStatus, typology, sector, region, hazards,
    favoritesFilter, unverifiedOnlyFilter
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    clearFilters: payload => {
      dispatch({ type: "CLEAR_FILTERS", payload })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    clearTitleFilter: () => {
      dispatch({ type: "LOAD_TITLE_FILTER", payload: "" })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    clearStatusFilter: () => {
      dispatch({ type: "LOAD_STATUS_FILTER", payload: { id: 0, value: 0 } })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    clearTypologyFilter: () => {
      dispatch({ type: "LOAD_TYPOLOGY_FILTER", payload: { id: 0, value: 0 } })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    clearRegionFilter: () => {
      dispatch({ type: "LOAD_REGION_FILTER", payload: 0 })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    clearSectorFilter: () => {
      dispatch({ type: "LOAD_SECTOR_FILTER", payload: 0 })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    clearHazardFilter: () => {
      dispatch({ type: "LOAD_HAZARD_FILTER", payload: 0 })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    toggleFavorites: () => {
      dispatch({ type: "TOGGLE_FAVS_FILTER", payload: false })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    toggleUnverifiedOnly: () => {
      dispatch({ type: "TOGGLE_UNVERIFIED_ONLY_FILTER", payload: false })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    }
  }
}

class ProjectFilters extends React.Component {

  constructor(props) {
    super(props);
    this.renderFilterChips = this.renderFilterChips.bind(this)

  }

  renderFilterChips() {

    let {
      titleFilter, statusFilter, typologyFilter, sectorFilter, regionFilter, projectStatus, typology, sector, region, hazards,
      favoritesFilter, unverifiedOnlyFilter, hazardFilter
    } = this.props
    let filterChips = []

    if (titleFilter !== "" || statusFilter !== 0 || typologyFilter !== 0 || hazardFilter !== 0 || sectorFilter !== 0 || regionFilter !== 0 ||
      favoritesFilter === true || unverifiedOnlyFilter === true) {

      if (unverifiedOnlyFilter === true) {
        filterChips.push(
          <div className="chip" key="verifFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Unverified Only"}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("verif")}></i>
          </div>
        )
      }

      if (favoritesFilter === true) {
        filterChips.push(
          <div className="chip" key="favsFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Favorites"}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("favs")}></i>
          </div>
        )
      }

      if (titleFilter !== "") {
        filterChips.push(
          <div className="chip" key="titleFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Title: " + titleFilter}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("title")}></i>
          </div>
        )
      }

      if (statusFilter > 0 && projectStatus.length > 0) {
        filterChips.push(
          <div className="chip" key="statusFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Status: " + projectStatus.filter(x => x.ProjectStatusId === parseInt(statusFilter))[0].Value}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("status")}></i>
          </div>
        )
      }

      if (typologyFilter > 0 && typology.length > 0) {
        filterChips.push(
          <div className="chip" key="typologyFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Typology: " + typology.filter(x => x.TypologyId === parseInt(typologyFilter))[0].Value}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("typology")}></i>
          </div>
        )
      }

      if (regionFilter > 0 && region.length > 0) {

        filterChips.push(
          <div className="chip" key="regionFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Region: " + region.filter(x => x.Id == regionFilter)[0].Text}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("region")}></i>
          </div>
        )
      }

      if (hazardFilter > 0 && hazards.length > 0) {
        filterChips.push(
          <div className="chip" key="hazardFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Hazard: " + hazards.filter(x => x.Id == hazardFilter)[0].Text}
            <i className="close fa fa-times" onClick={() => this.deleteFilterCHip("hazard")}></i>
          </div>
        )
      }

      if (sectorFilter > 0 && sector.length > 0) {
        filterChips.push(
          <div className="chip" key="sectorFilterChip" style={{ backgroundColor: DEAGreen }}>
            {"Sector: " + sector.filter(x => x.Id == sectorFilter)[0].Text}
            <i className="close fa fa-times" onClick={() => this.deleteFilterChip("sector")}></i>
          </div>
        )
      }
    }
    else {
      filterChips.push(<p key="naf">No Filters Appied.</p>)
    }

    return filterChips
  }

  deleteFilterChip(type) {

    switch (type) {

      case "title":
        this.props.clearTitleFilter()
        break

      case "status":
        this.props.clearStatusFilter()
        break

      case "typology":
        this.props.clearTypologyFilter()
        break

      case "region":
        this.props.clearRegionFilter()
        break

      case "sector":
        this.props.clearSectorFilter()
        break

      case "hazards":
        this.props.clearHazardFilter()
        break

      case "favs":
        this.props.toggleFavorites()
        break

      case "verif":
        this.props.toggleUnverifiedOnly()
        break
    }
  }

  render() {

    return (

      <div style={{ backgroundColor: "white", padding: "10px", borderRadius: "10px", border: "1px solid gainsboro" }}>

        <h4 style={{ margin: "5px 5px 0px 19px", display: "inline-block" }}>
          <b>Current Filters</b>
        </h4>
        <Button
          size="sm"
          color="white"
          style={{
            border: "0px solid gainsboro",
            boxShadow: "none",
            borderRadius: "7px",
            float: "right",
            marginTop: "8px",
            marginRight: "15px",
            padding: "2px",
          }}
          onClick={() => { this.props.clearFilters("") }}
        >
          <Fa icon="trash-o" size="2x" style={{ color: DEAGreen }} />
        </Button>

        <hr />

        <div style={{ padding: "10px 20px 10px 20px" }}>
          {this.renderFilterChips()}
        </div>

      </div >
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProjectFilters)