import React, { Children } from 'react'
import { Row, Col, Button } from 'mdbreact'
import { connect } from 'react-redux'
import OData from 'react-odata'
import { apiBaseURL, vmsBaseURL } from '../../config/serviceURLs.js'
import { BarChart, Bar, ResponsiveContainer, Tooltip, Legend, XAxis, YAxis } from 'recharts'
import buildQuery from 'odata-query'

//images
import popin from '../../../images/popin.png'
import { CustomFetch } from '../../globalFunctions.js';

const _gf = require('../../globalFunctions')

const mapStateToProps = (state, props) => {
  let { filterData: { statusFilter, typologyFilter, sectorFilter, regionFilter } } = state
  let { chartData: { chart1 } } = state
  let { projectData: { filteredProjectIDs } } = state
  return { statusFilter, typologyFilter, sectorFilter, regionFilter, chart1, filteredProjectIDs }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setChartData: payload => {
      dispatch({ type: "SET_CHART_1", payload })
    },
    loadProjectIDList: payload => {
      dispatch({ type: "LOAD_PROJECT_ID_LIST", payload })
    }
  }
}

class DashGraph1FullView extends React.Component {

  constructor(props) {
    super(props);
  }

  componentDidMount() {
    document.getElementById("app-content").scroll({
      top: 125,
      left: 0,
      behavior: 'smooth'
    });

    this.getChartData()
    this.getFilteredProjectIDs()
  }

  componentDidUpdate() {
    this.getFilteredProjectIDs()
  }

  async getFilteredProjectIDs() {

    let { statusFilter, typologyFilter, regionFilter, sectorFilter, filteredProjectIDs, loadProjectIDList } = this.props
    let filters = {}

    if (filteredProjectIDs.length === 0) {

      //ADD FILTERS//
      //Status//
      if (statusFilter !== 0) {
        filters.status = statusFilter
      }

      //Typology//
      if (typologyFilter !== 0) {
        filters.typology = typologyFilter
      }

      //Region//
      if (regionFilter != 0) {
        filters.region = regionFilter
      }

      //Sector//
      if (sectorFilter != 0) {
        filters.sector = sectorFilter
      }

      //GET PROJECTS FILTERED//
      try {
        let res = await CustomFetch(apiBaseURL + "Projects/Extensions.Filter?$select=ProjectId",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify(filters)
          })

        let resBody = await res.json()

        if (res.ok && resBody.value) {
          //Process resBody
          loadProjectIDList(resBody.value.map(p => p.ProjectId))
        }
        else {
          throw new Error(resBody.error.message)
        }

      }
      catch (ex) {
        console.error(ex)
      }
    }
  }

  async getChartData() {

    if (this.props.chart1.length === 0) {

      const query = buildQuery({
        select: "ProjectId, StartYear, EndYear",
        filter: {
          ProjectFunders: {
            any: [
              { Funder: { ne: null } }
            ]
          }
        },
        expand: {
          AdaptationDetails: {
            select: ["AdaptationDetailId"]
          },
          MitigationDetails: {
            select: ["MitigationDetailId"]
          }
        }
      })

      try {
        let res = await CustomFetch(apiBaseURL + `Projects${query}`)
        let resBody = await res.json()

        if (res.ok && resBody.value) {
          //Process resBody
          this.props.setChartData(resBody.value)
        }
        else {
          throw new Error(resBody.error.message)
        }
      }
      catch (ex) {
        console.error(ex)
      }
    }
  }

  transformData(data) {

    let tData = []

    let minYear = Math.min(...data.filter(p => p.StartYear > 0).map(p => p.StartYear))
    let maxYear = Math.max(...data.filter(p => p.EndYear > 0).map(p => p.EndYear))
    let currentYear = new Date().getFullYear()

    if (maxYear > currentYear) {
      maxYear = currentYear
    }

    for (let i = minYear; i <= maxYear; i++) {

      let fundedAdaptationCount = data.filter(p => p.StartYear <= i && p.EndYear >= i && p.AdaptationDetails.length > 0).length
      let fundedMitigationCount = data.filter(p => p.StartYear <= i && p.EndYear >= i && p.MitigationDetails.length > 0).length

      tData.push({
        Year: i, Adaptation: fundedAdaptationCount, Mitigation: fundedMitigationCount
      })
    }

    return tData
  }

  render() {

    let { chart1, filteredProjectIDs } = this.props
    let filteredData = chart1.filter(p => filteredProjectIDs.includes(p.ProjectId))
    let transformedData = this.transformData(filteredData)
    console.log(typeof(data))
    return (
      
      <div
        style={{
          backgroundColor: "white",
          padding: "10px 10px 0px 10px",
          borderRadius: "10px",
          border: "1px solid gainsboro",
        }}
      >

        <div
          style={{
            width: "95%",
            textAlign: "center",
            marginTop: "3px",
            marginBottom: "10px",
            paddingRight: "25px",
            color: "grey",
            fontSize: "14px",
            fontWeight: "bolder",
            display: "inline",
            float: "left"
          }}
        >
          FUNDED PROJECTS PER YEAR PER ADAPTATION / MITIGATION
        </div>

        <img
          src={popin}
          style={{
            width: "25px",
            cursor: "pointer",
            float: "right",
            display: "inline"
          }}
          onClick={() => {
            location.hash = location.hash.replace("#/chart1", "")
          }}
        />

        <br />

        <div
          style={{
            width: "100%",
            height: "550px",
            paddingTop: "5px",
            paddingLeft: "10px",
            border: "none"
          }}
        >
          {
            (transformedData.length > 0) &&
            <ResponsiveContainer key={new Date().valueOf()} width="96%" height="98%">
              <BarChart data={transformedData} >
                <XAxis dataKey="Year" />
                <YAxis />
                <Bar
                  type='monotone'
                  dataKey='Adaptation'
                  stackId="1"
                  stroke='#8884d8'
                  fill='#8884d8'
                />
                <Bar
                  type='monotone'
                  dataKey='Mitigation'
                  stackId="1"
                  stroke='#82ca9d'
                  fill='#82ca9d'
                />
                <Tooltip />
                <Legend />
              </BarChart>
            </ResponsiveContainer>
          }
        </div>
      </div>
    )
  }
}


export default connect(mapStateToProps, mapDispatchToProps)(DashGraph1FullView)