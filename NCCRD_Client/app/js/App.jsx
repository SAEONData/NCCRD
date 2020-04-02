
'use strict'

//Styles - Ant.Design (has to be loaded before MDB so that MDB can replace all applicable styles)
import 'antd/lib/style/index.css'

//Styles - MDB
import 'font-awesome/css/font-awesome.min.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'mdbreact/dist/css/mdb.css'

//Components
import React from 'react'
import { connect } from 'react-redux'
import { Spinner, Container } from 'mdbreact/'
import { HashRouter as Router, Switch, Route } from 'react-router-dom'
import Projects from './components/Projects/List/Projects.jsx'
import ProjectDetails from './components/Projects/Details/ProjectDetails.jsx'
import Login from './components/Authentication/Login.jsx'
import Logout from './components/Authentication/Logout.jsx'
import CustomNavbar from './components/Base/CustomNavbar.jsx'
import CallbackPage from '../js/components/Authentication/callback.jsx'
import ReactTooltip from 'react-tooltip'
import Header from './components/Base/Header.jsx'
import Footer from './components/Base/Footer.jsx'
import userManager from './components/Authentication/userManager'
import DashLayout from './components/Dashboard/DashLayout.jsx'
import MapView from './components/Map/MapView.jsx'
import SideNav from './components/Base/SideNav.jsx'
import { data as NavData } from '../data/sideNavConfig'
import DashGraph1FullView from './components/Dashboard/DashGraph1FullView.jsx'
import DashGraph2FullView from './components/Dashboard/DashGraph2FullView.jsx'
import DashGraph3FullView from './components/Dashboard/DashGraph3FullView.jsx'
import DashGraph4FullView from './components/Dashboard/DashGraph4FullView.jsx'
import InputWizard from './components/Wizard/InputWizard.jsx';
import { DEAGreenDark, DEAGreen } from './config/colours.js';
import ReactGA from "react-ga";

const Oidc = require("oidc-client")
const _gf = require("./globalFunctions.js")
const o = require("odata")
const queryString = require('query-string')

const mapStateToProps = (state, props) => {
  let { globalData: {
    loading, showSideNav, showSideNavButton, showHeader, showNavbar, showFooter, showInputWizard
  } } = state
  let user = state.oidc.user
  return { loading, user, showSideNav, showHeader, showNavbar, showFooter, showSideNavButton, showInputWizard }
}

const mapDispatchToProps = (dispatch) => {
  return {
    toggleHeader: payload => {
      dispatch({ type: "TOGGLE_HEADER", payload })
    },
    toggleNavbar: payload => {
      dispatch({ type: "TOGGLE_NAVBAR", payload })
    },
    toggleFooter: payload => {
      dispatch({ type: "TOGGLE_FOOTER", payload })
    },
    toggleListExpandCollapse: payload => {
      dispatch({ type: "TOGGLE_LIST_EXPAND_COLLAPSE", payload })
    },
    toggleListView: payload => {
      dispatch({ type: "TOGGLE_LIST_VIEW", payload })
    },
    toggleListFavorites: payload => {
      dispatch({ type: "TOGGLE_LIST_FAVORITES", payload })
    },
    toggleReadOnly: payload => {
      dispatch({ type: "TOGGLE_READONLY", payload })
    },
    toggleSideNavButton: payload => {
      dispatch({ type: "TOGGLE_SIDENAV_BUTTON", payload })
    },
    toggleListFilterOptions: payload => {
      dispatch({ type: "TOGGLE_LIST_FILTER_OPTIONS", payload })
    },
    toggleBackToList: payload => {
      dispatch({ type: "TOGGLE_BACK_TO_LIST", payload })
    },
    toggleDetailsInParent: payload => {
      dispatch({ type: "TOGGLE_SHOW_DETAILS_IN_PARENT", payload })
    },
    loadRegionFilter: payload => {
      dispatch({ type: "LOAD_REGION_FILTER", payload })
    },
    loadSectorFilter: payload => {
      dispatch({ type: "LOAD_SECTOR_FILTER", payload })
    },
    loadHazardFilter: payload => {
      dispatch({ type: "LOAD_HAZARD_FILTER", payload })
    },
    loadStatusFilter: payload => {
      dispatch({ type: "LOAD_STATUS_FILTER", payload })
    },
    loadTitleFilter: payload => {
      dispatch({ type: "LOAD_TITLE_FILTER", payload })
    },
    loadTypologyFilter: payload => {
      dispatch({ type: "LOAD_TYPOLOGY_FILTER", payload })
    },
    loadPolygonFilter: payload => {
      dispatch({ type: "LOAD_POLYGON_FILTER", payload })
    },
    setDAOID: async payload => {
      dispatch({ type: "SET_DAOID", payload })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
  }
}

//Enable OIDC Logging
Oidc.Log.logger = console
Oidc.Log.level = Oidc.Log.INFO

/**
 * App
 */
class App extends React.Component {

  constructor(props) {
    super(props);
  }

  componentWillMount() {
    //this.genTestConfig()
    this.processURLConfig()
    console.log('loadedurl')
    this.processSilentSignIn()
  }

  async processSilentSignIn() {
    try {
      await userManager.signinSilent()
    }
    catch (ex) {
      console.warn("Sign-in-silent failed!", ex)
    }
  }

  genTestConfig() {
    // TEST //
    let config = {
      header: true, // true/false  >>>  toggle header on/off
      navbar: true, // true/false/'addOnly'  >>>  toggle navbar on/off
      sidenav: true, // true/false  >>>  toggle sidenav on/off
      footer: true, // true/false  >>>  toggle footer on/off
      daoid: true, // true/false/[guid]  >>>  toggle DOA functionality on details view, as well as set DAOID for auto-linking
      readOnly: false, //true/false  >>>  toggle allow editing on details view
      backToList: true, //true/false  >>>  toggle "Back To List" button in details view on/off
      filters: {
        region: 0, //number  >>>  region filter
        sector: 0, //number  >>>  sector filter
        hazard: 0, //number  >>>  hazard filter
        status: 0, //number  >>>  status filter
        title: "", //string  >>>  title filter - partial match logic
        typology: 0, //number  >>>  typology filter
        polygon: "" //string  >>>  polygon filter - WKT/POLYGON
      },
      listOptions: {
        expandCollapse: true, //true/false  >>>  allow minimize/maximize project list
        view: true, //true/false  >>>  toggle view button in project cards
        favorites: true, //true/false  >>>  toggle favorites functionality in project cards/list
        filters: true, //true/false  >>>  toggle filtering UI functionality
        detailsInParent: false //true/false  >>>  togle to show details in parent/child
      }
    }

    config = encodeURI(JSON.stringify(config))
      ("config", config)
    // TEST //    
  }

  processURLConfig() {
    try {
      const parsedHash = queryString.parse(location.hash.substring(location.hash.indexOf("?")))
      if (parsedHash.config) {

        let config = JSON.parse(parsedHash.config)

        //daoid
        if (typeof config.daoid !== 'undefined' && config.daoid !== null) {
          this.props.setDAOID(config.daoid)
        }

        //header
        if (typeof config.header === 'boolean') {
          this.props.toggleHeader(config.header)
        }

        //sidenav
        if (typeof config.sidenav === 'boolean') {
          this.props.toggleSideNavButton(config.sidenav)
        }

        //navbar
        if (typeof config.navbar === 'boolean' || typeof config.navbar === 'string') {
          this.props.toggleNavbar(config.navbar)
        }

        //footer
        if (typeof config.footer === 'boolean') {
          this.props.toggleFooter(config.footer)
        }

        //readOnly
        if (typeof config.readOnly === 'boolean') {
          this.props.toggleReadOnly(config.readOnly)
        }

        //backToList
        if (typeof config.backToList === 'boolean') {
          this.props.toggleBackToList(config.backToList)
        }

        //filters
        if (typeof config.filters !== 'undefined') {
          let filters = config.filters

          //region
          if (typeof filters.region === 'number' && filters.region > 0) {
            this.props.loadRegionFilter(filters.region)
          }

          //sector
          if (typeof filters.sector === 'number' && filters.sector > 0) {
            this.props.loadSectorFilter(filters.sector)
          }

          //hazard
          if (typeof filters.hazard === 'number' && filters.hazard > 0) {
            this.props.loadHazardFilter(filters.hazard)
          }

          //status
          if (typeof filters.status === 'number' && filters.status > 0) {
            this.props.loadStatusFilter(filters.status)
          }

          //title
          if (typeof filters.title === 'string' && filters.title !== "") {
            this.props.loadTitleFilter(filters.title)
          }

          //typology
          if (typeof filters.typology === 'number' && filters.typology > 0) {
            this.props.loadTypologyFilter(filters.typology)
          }

          //polygon
          if (typeof filters.polygon === 'string' && filters.polygon !== "") {
            this.props.loadPolygonFilter(filters.polygon)
          }
        }

        //listOptions
        if (typeof config.listOptions !== 'undefined') {
          let listOptions = config.listOptions

          //expandCollapse
          if (typeof listOptions.expandCollapse === 'boolean') {
            this.props.toggleListExpandCollapse(listOptions.expandCollapse)
          }

          //view
          if (typeof listOptions.view === 'boolean') {
            this.props.toggleListView(listOptions.view)
          }

          //favorites
          if (typeof listOptions.favorites === 'boolean') {
            this.props.toggleListFavorites(listOptions.favorites)
          }

          //filters
          if (typeof listOptions.filters === 'boolean') {
            this.props.toggleListFilterOptions(listOptions.filters)
          }

          //detailsInParent
          if (typeof listOptions.detailsInParent === 'boolean') {
            this.props.toggleDetailsInParent(listOptions.detailsInParent)
          }
        }
      }
    }
    catch (ex) {
      console.warn(ex)
    }
  }

  componentDidUpdate() {
    let { user } = this.props

    let headers = []
    headers.push({ name: "Accept", value: "application/json" })
    ReactGA.initialize('UA-145168002-1');
    ReactGA.pageview(location.href);
    console.log('url: ' + location.href)
    if (user && !user.expired) {
      //Add auth token to headers
      headers.push({ name: "Authorization", value: "Bearer " + (user === null ? "" : user.access_token) })
    }

    //Add headers to OData global config
    o().config({
      headers: headers
    })

  }

  render() {

    let loaderWidth = 300
    let loaderHeight = 165

    let { showSideNav, showSideNavButton, showHeader, showNavbar, showFooter, showInputWizard } = this.props

    return (
      <Router>
        <div id="app-content" style={{ height: '100vh', overflowY: (showInputWizard === true ? 'hidden' : 'scroll'), overflowX: 'hidden' }}>
          {(showHeader === true) && <Header />}
          {(showNavbar !== false) && <CustomNavbar />}

          <div style={{ margin: "0px 15px 0px 15px" }}>

            {
              showSideNavButton === true &&
              <SideNav data={NavData} isOpen={showSideNav} />
            }

            <InputWizard visible={showInputWizard} />

            {
              (showHeader === true || showNavbar !== false) &&
              <div style={{ height: "15px", backgroundColor: "whitesmoke" }} />
            }

            <div style={{ backgroundColor: "whitesmoke" }}>
              <div style={{ margin: "0px" }}>
                <Switch >
                  <Route path="/" component={DashLayout} exact />
                  <Route path="/projects" component={Projects} exact />
                  <Route path="/projects/:id" component={ProjectDetails} exact />
                  <Route path="/map" component={MapView} exact />
                  <Route path="/login" component={Login} exact />
                  <Route path="/logout" component={Logout} exact />
                  <Route path="/callback" component={CallbackPage} />
                  <Route path="/chart1" component={DashGraph1FullView} exact />
                  <Route path="/chart2" component={DashGraph2FullView} exact />
                  <Route path="/chart3" component={DashGraph3FullView} exact />
                  <Route path="/chart4" component={DashGraph4FullView} exact />
                </Switch>
              </div>
            </div>
          </div>

          {
            (showFooter === true) &&
            <div>
              <div style={{ height: "15px", backgroundColor: "whitesmoke" }} />
              <Footer />
            </div>
          }

          <div className="container-fluid">
            <div className="row">
              <div
                hidden={!this.props.loading}
                className="card"
                style={{ height: (loaderHeight + "px"), width: (loaderWidth + 'px'), position: "fixed", left: ((window.innerWidth / 2) - (loaderWidth / 2)), top: ((window.innerHeight / 2) - (loaderHeight / 2)), zIndex: "999999999999" }}>

                <div className="card-body">
                  <label style={{ width: "100%", textAlign: "center", fontSize: "x-large", fontWeight: "bold", color: DEAGreenDark }}>LOADING</label>
                  <br />
                  <span style={{ width: "100px", paddingLeft: ((loaderWidth / 2) - 50) }}>
                    <Spinner big multicolor />
                  </span>
                </div>
              </div>
            </div>
          </div>

          <ReactTooltip delayShow={700} />

        </div>
      </Router>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(App)

  
