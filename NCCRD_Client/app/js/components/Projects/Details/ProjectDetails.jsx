import React from 'react'
import { Row, Col, Button, Fa } from 'mdbreact'
import { Tooltip } from 'antd'
import { connect } from 'react-redux'
import OverallSummaryStep from '../../Wizard/Steps/OverallSummaryStep.jsx'
import ProjectDataLoader from '../ProjectDataLoader.jsx';
import { DEAGreen } from '../../../config/colours.js'

import './ProjectDetails.css'

const _gf = require("../../../globalFunctions.js")


const mapStateToProps = (state, props) => {
  let { globalData: { projectsFullView } } = state
  let { projectData: { projectDetails, selectedProjectId } } = state
  let { projectFundersData: { projectFunderDetails } } = state
  let { adaptationData: { adaptationDetails } } = state
  return { projectDetails, projectFunderDetails, adaptationDetails, selectedProjectId, projectsFullView }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setSelectedProjectId: payload => {
      dispatch({ type: "SET_SELECTED_PROJECT_ID", payload })
    }
  }
}

class ProjectDetails extends React.Component {

  constructor(props) {
    super(props)

    this.backToList = this.backToList.bind(this)
    this.handleScroll = this.handleScroll.bind(this);

    this.state = { showBackToTop: false }
  }

  componentDidMount() {
    this.scrollToTop(true) //window.scrollTo(0, 0);
    let pid = this.props.match.params.id
    this.props.setSelectedProjectId(pid)
    document.getElementById("app-content").addEventListener("scroll", this.handleScroll);
  }

  handleScroll() {
    this.setState({ showBackToTop: (document.getElementById("app-content").scrollTop > 500) })
  }

  componentWillUnmount() {
    document.getElementById("app-content").removeEventListener("scroll", this.handleScroll)
  }

  scrollToTop(instant) {
    document.getElementById("app-content").scroll({
      top: 0,
      left: 0,
      behavior: (instant === true ? "auto" : "smooth")
    });
  }

  backToList() {
    location.hash = location.hash.replace(
      "#/projects/" + this.props.selectedProjectId,
      this.props.projectsFullView === true ? "#/projects" : ""
    )
  }

  render() {

    let { projectDetails, projectFunderDetails, adaptationDetails, projectDescription } = this.props
    let { showBackToTop } = this.state

    {projectDetails.projectDescription = _gf.StringToHTML()}

    return (
      <div id="app-content" className="pd-container ">

        <div style={{ position: "fixed", right: "40px", bottom: "15px", zIndex: "99" }}>
          {showBackToTop &&
            <Tooltip title="Back to top" mouseEnterDelay={0.7}>
              <Button size="sm" floating color="" onClick={this.scrollToTop}
                style={{ backgroundColor: DEAGreen }}>
                <Fa icon="arrow-up" />
              </Button>
            </Tooltip>
          }
        </div>

        <Row>
          <Col>
            <ProjectDataLoader>
              <OverallSummaryStep
                header={
                  <Button style={{ margin: "0px 0px 20px -2px", backgroundColor: DEAGreen }} color="" size="sm" onClick={this.backToList}>
                    <i className="fa fa-chevron-circle-left" aria-hidden="true" style={{ marginRight: "15px" }} />
                    Back to list
                  </Button>
                }  //{<h6><i>Project Details</i></h6>}
                projectDetails={projectDetails}
                adaptationDetails={adaptationDetails}
                funderDetails={projectFunderDetails}
                errors={false}
              />
            </ProjectDataLoader>
          </Col>
        </Row>
      </div>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProjectDetails)