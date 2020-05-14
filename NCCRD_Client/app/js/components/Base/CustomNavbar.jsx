import React from 'react'
import { connect } from 'react-redux'
import { Button, Input, Fa } from 'mdbreact'
import {
  Navbar,
  NavItem,
  NavbarNav,
  NavbarToggler,
  Collapse,
  NavLink,
  Dropdown,
  DropdownMenu,
  DropdownItem,
  DropdownToggle,
} from 'mdbreact'
import { notification, message } from 'antd'
import userManager from '../Authentication/userManager'
import { ssoBaseURL, ndaoSiteBaseURL, ndmcBaseURL, nccisBaseURL } from '../../config/serviceURLs.js'
import { DEAGreen } from '../../config/colours.js'
import { data as NavData } from '../../../data/sideNavConfig'
import { destroyFns } from 'antd/lib/modal/Modal'
import ReactTooltip from 'react-tooltip'

const _gf = require("../../globalFunctions")
const queryString = require('query-string')


const mapStateToProps = (state, props) => {
  let user = state.oidc.user
  let { globalData: { loading, daoid, showSideNav, showSideNavButton, showNavbar } } = state
  return { user, loading, daoid, showSideNav, showSideNavButton, showNavbar }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setDAOID: async payload => {
      dispatch({ type: "SET_DAOID", payload })
      dispatch({ type: "SET_FILTERS_CHANGED", payload: true })
    },
    toggleSideNav: payload => {
      dispatch({ type: "TOGGLE_SIDENAV", payload })
    },
    showInputWizard: payload => {
      dispatch({ type: "SET_SHOW_INPUT_WIZARD", payload })
    },
    setSelectedProjectId: payload => {
      dispatch({ type: "SET_SELECTED_PROJECT_ID", payload })
    }
  }
}

class CustomNavbar extends React.Component {

  constructor(props) {
    super(props)

    this.state = {
      collapse: false,
      isWideEnough: false,
      dropdownOpen: false
    }

    this.onClick = this.onClick.bind(this)
    this.toggle = this.toggle.bind(this)
    this.Register = this.Register.bind(this)
  }

  onClick() {
    this.setState({
      collapse: !this.state.collapse,
    });
  }

  toggle() {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen
    });
  }

  LoginLogoutClicked() {
    //Save current URL to cookie
    _gf.SaveCurrentUrl()
  }


  Register() {
    let { user } = this.props

    if (!user || user.expired) {
      return <a key="lnkRegister" className="nav-link" href={ssoBaseURL + "Account/Register"} target="_blank"><b style={{ color: "black" }}>Register</b></a>
    }
  }

  render() {

    let { user, toggleSideNav, showSideNav, showSideNavButton, showNavbar } = this.props

    return (
      <>
       <ReactTooltip id="buTip" place="top" effect="solid">
              To download this document, click on the button, in the tab that opens, on the right there is a three-dotted menu button, click on that and select the 'download' option
          </ReactTooltip>
      <Navbar
        size="sm"
        color="white"
        light
        expand="md"
        style={{
          boxShadow: "0px 15px 10px -15px gainsboro",
          borderTop: "1px solid #E8E8E8",
          paddingTop: 2,
          paddingBottom: 2
        }}
      >
        {
          !this.state.isWideEnough &&
          <NavbarToggler
            style={{ backgroundColor: "#2BBBAD" }}
            onClick={this.onClick}
          />
        }

        <Collapse isOpen={this.state.collapse} navbar>

       

          {/* LEFT */}
          <NavbarNav left>

            {
              (showSideNavButton === true && showNavbar !== "addOnly") &&
              <Button size="sm" color="grey" onClick={() => { toggleSideNav(!showSideNav) }}
                style={{ width: "45px", marginLeft: "0px", marginRight: "15px", paddingLeft: "18px" }}>
                <Fa icon="bars" />
              </Button>
            }

            <Button
              color=""
              style={{ backgroundColor: DEAGreen }}
              size="sm"
              onClick={() => {
                if (!user || user.expired) {
                  notification.warning({
                    message: 'Please login to submit projects.'
                  })
                }
                else {
                  let dispatch = () => new Promise((resolve, reject) => {
                    this.props.setSelectedProjectId(0)
                    resolve()
                  })
                  dispatch().then(() => {
                    this.props.showInputWizard(true)
                  })
                }
              }}>
              <Fa icon="plus" style={{ marginRight: 15 }} />
              Add New Project
            </Button>
            

            <Button
              data-tip data-for="buTip"
              color=""
              style={{ backgroundColor: DEAGreen }}
              size="sm"
              onClick={() => {
                if (!user || user.expired) {
                  notification.warning({
                    message: 'Please login to submit projects.'
                  })
                }
                else {
                  window.open("https://drive.google.com/file/d/1CPNX3mx_F8Y43m59NOn8YOKo4_VPdsZG/view?usp=sharing")
                }
              }}>
              <Fa icon="plus" style={{ marginRight: 15 }} />
              Bulk upload
            </Button>
        
            

            {/* Monitoring */}
            <NavItem>
              <Dropdown>
                <DropdownToggle nav caret style={{ color: "black" }}><b>Monitoring and Evaluation</b></DropdownToggle>
                <DropdownMenu>
                  <DropdownItem header style={{ marginLeft: "-16px", fontSize: "16px", color: "black" }}>
                    <b>
                      Climate Change Adaptation&nbsp;
                        <br className="d-block d-md-none" />
                      Monitoring and Evaluation
                      </b>
                  </DropdownItem>
                  <DropdownItem divider />
                  {/* <DropdownItem header style={{ marginLeft: "-16px", fontWeight: "400", fontSize: "16px", color: "black" }}>
                      Impacts:
                    </DropdownItem> */}
                  <DropdownItem href={ndaoSiteBaseURL} style={{ marginLeft: "7px" }}>
                    <b style={{ dropdownItemHoverBackgroundColor: DEAGreen }}>View Information</b>
                  </DropdownItem>
                  <DropdownItem href={ndaoSiteBaseURL + '#/ame/contribute'} style={{ marginLeft: "7px" }}>
                    <b style={{ color: "#0000000" }}>Submit evaluation on Progress</b>
                  </DropdownItem>
                  <DropdownItem href={nccisBaseURL} style={{ marginLeft: "7px" }}>
                    <b style={{ color: "#0000000" }}>NCCIS</b>
                  </DropdownItem>
                </DropdownMenu>
              </Dropdown>
            </NavItem>

        


            {/* Hazards */}
            {/* <NavItem>
              <Dropdown>
                <DropdownToggle nav caret style={{ color: "black" }}><b>Hazardous Events </b></DropdownToggle>
                <DropdownMenu>
                  <DropdownItem header style={{ marginLeft: "-16px", fontSize: "16px", color: "black" }}>
                    <b>
                      National Hazardous Events
                  </b>
                  </DropdownItem>
                  <DropdownItem divider />
                  <DropdownItem href={ndmcBaseURL} style={{ marginLeft: "7px" }}>
                    <b style={{ color: "grey" }}>National Hazardous Events Database</b>
                  </DropdownItem>
                  <DropdownItem href={ndmcBaseURL} style={{ marginLeft: "7px" }}>
                    <b style={{ color: "grey" }}>Submit Hazardous Event</b>
                  </DropdownItem>
                </DropdownMenu>
              </Dropdown>
            </NavItem> */}
          </NavbarNav>

          {/* RIGHT */}
          {
            showNavbar !== "addOnly" &&
            <NavbarNav right>

              {/* Username */}
              {(user && !user.expired) &&
                <table>
                  <tbody>
                    <tr style={{ height: "40px" }}>
                      <td valign="middle">
                        <div style={{ marginRight: "7px", color: DEAGreen }} >
                          <Fa size="2x" icon="user-circle-o" />
                        </div>
                      </td>
                      <td valign="middle">
                        <div style={{ fontSize: "17px" }} >
                          <b>{`${user.profile.FirstName} ${user.profile.Surname}`}</b>
                        </div>
                      </td>
                    </tr>
                  </tbody>
                </table>
              }

              {/* Login / Logout */}
              <NavItem style={{ marginLeft: "15px" }}>
                {(!user || user.expired) &&
                  <a className="nav-link" onClick={this.LoginLogoutClicked} href="#/login">
                    <b style={{ color: "black" }}>
                      Login
                  </b>
                  </a>
                }
                {(user && !user.expired) &&
                  <a className="nav-link" onClick={this.LoginLogoutClicked} href="#/logout">
                    <b style={{ color: "black" }}>
                      Logout
                  </b>
                  </a>
                }
              </NavItem>

              {/* Register */}
              {(!user || user.expired) &&
                <NavItem style={{ marginLeft: "15px" }}>
                  <a key="lnkRegister" className="nav-link" href={ssoBaseURL + "Account/Register"} target="_blank">
                    <b style={{ color: "black" }}>
                      Register
                  </b>
                  </a>
                </NavItem>
              }

            </NavbarNav>
          }

        </Collapse>
      </Navbar>
        
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(CustomNavbar)