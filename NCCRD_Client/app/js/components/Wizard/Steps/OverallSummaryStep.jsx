import React from 'react'
import { connect } from 'react-redux'
import { Row, Col, Button, Fa } from 'mdbreact'
import { Collapse, Icon } from 'antd'
import { CSVLink } from 'react-csv'

const Panel = Collapse.Panel;

import './shared.css'
import "./OverallSummaryStep.css"
import { DEAGreen, DEAGreenDark } from '../../../config/colours';

const mapStateToProps = (state, props) => {
  let { lookupData: {
    projectStatus, region, users, fundingStatus, adaptationPurpose, sector, sectorType,
    typology, hazards, researchType, targetAudience, researchMaturity } } = state
  return {
    projectStatus, region, users, fundingStatus, adaptationPurpose, sector, sectorType,
    typology, hazards, researchType, targetAudience, researchMaturity
  }
}

const mapDispatchToProps = (dispatch) => {
  return {}
}

class OverallSummaryStep extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      activeKey: []
    }

    this.getProjectStatusValue = this.getProjectStatusValue.bind(this)
    this.getProjectRegionValue = this.getProjectRegionValue.bind(this)
    this.getProjectManagerValue = this.getProjectManagerValue.bind(this)
    this.getFundingStatusValue = this.getFundingStatusValue.bind(this)
    this.getAdaptationPurposeValue = this.getAdaptationPurposeValue.bind(this)
    this.getSectorValue = this.getSectorValue.bind(this)
    this.getHazardValue = this.getHazardValue.bind(this)
    this.getResearchTypeValue = this.getResearchTypeValue.bind(this)
    this.getTargetAudienceValue = this.getTargetAudienceValue.bind(this)
    this.getResearchMaturityValue = this.getResearchMaturityValue.bind(this)
    this.onChange = this.onChange.bind(this)
  }

  getProjectStatusValue(id) {

    let { projectStatus } = this.props
    let value = ""

    if (projectStatus) {
      let filtered = projectStatus.filter(ps => ps.ProjectStatusId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getProjectRegionValue(id) {

    let { region } = this.props
    let value = ""

    if (region) {
      let filtered = region.filter(r => r.Id === id.toString())
      if (filtered && filtered.length > 0) {
        value = filtered[0].Text
      }
    }

    return (<h6 key={id} className="summary-value">{value}</h6>)
  }

  getProjectManagerValue(id) {

    let { users } = this.props
    let value = ""

    if (users) {
      let filtered = users.filter(u => u.PersonId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getFundingStatusValue(id) {

    let { fundingStatus } = this.props
    let value = ""

    if (fundingStatus) {
      let filtered = fundingStatus.filter(fs => fs.FundingStatusId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getAdaptationPurposeValue(id) {

    let { adaptationPurpose } = this.props
    let value = ""

    if (adaptationPurpose) {
      let filtered = adaptationPurpose.filter(ap => ap.AdaptationPurposeId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getSectorValue(id) {

    let { sector } = this.props
    let value = ""

    if (sector) {
      let filtered = sector.filter(sec => sec.Id == id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Text
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getHazardValue(id) {

    let { hazards } = this.props
    let value = ""

    if (hazards) {
      let filtered = hazards.filter(haz => haz.Id === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Text
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getResearchTypeValue(id) {

    let { researchType } = this.props
    let value = ""

    if (researchType) {
      let filtered = researchType.filter(r => r.ResearchTypeId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getTargetAudienceValue(id) {

    let { targetAudience } = this.props
    let value = ""

    if (targetAudience) {
      let filtered = targetAudience.filter(ta => ta.TargetAudienceId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getResearchMaturityValue(id) {

    let { researchMaturity } = this.props
    let value = ""

    if (researchMaturity) {
      let filtered = researchMaturity.filter(rm => rm.ResearchMaturityId === id)
      if (filtered && filtered.length > 0) {
        value = filtered[0].Value
      }
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  onChange(key) {
    this.setState({ activeKey: key })
  }

  render() {

    let { projectDetails, funderDetails, adaptationDetails, errors, header } = this.props
    let { activeKey } = this.state

    return (
      <>
        <Row>
          <Col>
            <h6>
              <i>
                {header}
              </i>
            </h6>
          </Col>
          <Col>
            <div style={{ float: "right" }}>
            <CSVLink
              style={{ marginRight: '15px', textDecoration: 'underline', color: '#006532' }}
              filename={`${projectDetails.ProjectTitle + '.csv'}`}
              // headers = {
              //   [{label: 'project-title', key: 'projectDetails.projectTitle'}]
              // }
              data={[projectDetails]}
              asyncOnClick={true}
              onClick={() => {
                console.log(projectDetails.ProjectTitle)
              }}
            >
              {/* <Fa icon="arrow-circle-down" style={{ marginRight: 15 }} /> */}
              {/* <MDBIcon icon="arrow-circle-down" style={{ marginRight: 15 }} /> */}
              Download
            </CSVLink>
              <a
                // href="#"
                onClick={() => { this.setState({ activeKey: ["1", "2", "3", "4"] }) }}
                style={{ color: DEAGreenDark }}
              >
                <u>
                  Expand all
                </u>
              </a>
              <div className="horizontal-spacer" />
              <a
                // href="#"
                onClick={() => { this.setState({ activeKey: [] }) }}
                style={{ color: DEAGreenDark }}
              >
                <u>
                  Collapse all
              </u>
              </a>
            </div>
          </Col>
        </Row>

        {
          errors == true &&
          <div>
            <Row>
              <Col>
                <div className="error-card">
                  <h5><b><u>REQUIRED</u></b></h5>
                  <div className="vertical-spacer" />
                  <h6>
                    Some steps are missing require values (indicated by
                    <Fa icon="info-circle" size="lg" style={{ marginLeft: 5 }} />
                    ).
                    <br />
                    These steps a marked with
                    <div style={{ fontSize: '32px', marginTop: -8, marginLeft: -1 }}>
                      <Icon type="close-circle" />
                    </div>
                    <br />
                    You will not be able to submit until these requirements are fulfilled.
                    <br />
                    Click on a step-title to jump to that step.
                  </h6>
                </div>
              </Col>
            </Row>
          </div>
        }

        <h4>
          <Fa icon="map-marker" style={{ marginRight: 15, color: DEAGreenDark, fontSize: 28 }} />
          <b>
            {projectDetails.ProjectTitle}
          </b>
        </h4>

        <hr style={{ marginBottom: 0, color: "#F0F0F0", backgroundColor: "#F0F0F0" }} />
        <Collapse className="summary-collapse" bordered={false} activeKey={activeKey} onChange={this.onChange}>
          <Panel
            header={
              <h5 className="summary-panel-header" style={{ color: DEAGreenDark }}><u>PROJECT</u></h5>
            }
            key="1"
          >
            <div className="summary-panel">
              {/* <Row >
                <Col md="12">
                  <h6 className="summary-label">Title</h6>
                  <h6 className="summary-value">{projectDetails.ProjectTitle}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" /> */}

              <Row>
                <Col md="12">
                  <h6 className="summary-label">Description</h6>
                  <h6 className="summary-value">{projectDetails.ProjectDescription}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="12">
                  <h6 className="summary-label">Link</h6>
                  <h6 className="summary-value">{projectDetails.Link}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label ">Year</h6>
                  <table>
                    <tbody>
                      <tr>
                        <td width="50%">
                          <h6 className="summary-value">{projectDetails.StartYear}</h6>
                        </td>
                        <td>
                          <h6>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;</h6>
                        </td>
                        <td width="50%">
                          <h6 className="summary-value">{projectDetails.EndYear}</h6>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Status</h6>
                  {this.getProjectStatusValue(projectDetails.ProjectStatusId)}
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label ">Budget range</h6>
                  <table>
                    <tbody>
                      <tr>
                        <td>
                          <h6>
                            R&nbsp;
                          </h6>
                        </td>
                        <td width="50%">
                          <h6 className="summary-value">{projectDetails.BudgetLower}</h6>
                        </td>
                        <td>
                          <h6>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;</h6>
                        </td>
                        <td>
                          <h6>
                            R&nbsp;
                          </h6>
                        </td>
                        <td width="50%">
                          <h6 className="summary-value">{projectDetails.BudgetUpper}</h6>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Linked DAO's</h6>
                  {
                    (projectDetails.ProjectDAOs && projectDetails.ProjectDAOs.length > 0) &&
                    <h6 className="summary-value">{projectDetails.ProjectDAOs.length} linked DAO's</h6>
                  }
                  {
                    (!projectDetails.ProjectDAOs || projectDetails.ProjectDAOs.length === 0) &&
                    <h6 className="summary-value">0 linked DAO's</h6>
                  }
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Regions</h6>
                  {
                    (projectDetails.ProjectRegions && projectDetails.ProjectRegions.length > 0) &&
                    projectDetails.ProjectRegions.map(pr => {
                      return this.getProjectRegionValue(pr.RegionId)
                    })
                  }
                  {
                    (!projectDetails.ProjectRegions || projectDetails.ProjectRegions.length === 0) &&
                    <h6 className="summary-value"></h6>
                  }
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Locations</h6>
                  {
                    (projectDetails.ProjectLocations && projectDetails.ProjectLocations.length > 0) &&
                    projectDetails.ProjectLocations.map(pl => {
                      return (
                        <h6 key={pl.ProjectLocationId} className="summary-value">{pl.Location.LatCalculated}, {pl.Location.LonCalculated}</h6>
                      )
                    })
                  }
                  {
                    (!projectDetails.ProjectLocations || projectDetails.ProjectLocations.length === 0) &&
                    <h6 className="summary-value"></h6>
                  }
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Lead Organisation</h6>
                  <h6 className="summary-value">{projectDetails.LeadAgent}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Project Manager</h6>
                  {this.getProjectManagerValue(projectDetails.ProjectManagerId)}
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Host Organisation</h6>
                  <h6 className="summary-value">{projectDetails.HostOrganisation}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Host Partner</h6>
                  <h6 className="summary-value">{projectDetails.HostPartner}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              {/* <Row>
                <Col md="6">
                  <h6 className="summary-label">Contact (alt)</h6>
                  <h6 className="summary-value">{projectDetails.AlternativeContact}</h6>
                </Col>
              </Row>

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Contact Email (alt)</h6>
                  <h6 className="summary-value">{projectDetails.AlternativeContactEmail}</h6>
                </Col>
              </Row> */}

              <div className="vertical-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Project Verification State</h6>
                  <h6 className="summary-value">
                    {projectDetails.Verified ? "VERIFIED" : "NOT VERIFIED"}
                  </h6>
                </Col>
              </Row>

            </div>
          </Panel>

          <Panel
            header={
              <h5 className="summary-panel-header" style={{ color: DEAGreenDark }}><u>FUNDING</u></h5>
            }
            key="2"
          >
            <div className="summary-panel">
              {
                funderDetails.map(funder => {

                  let index = funderDetails.indexOf(funder) + 1

                  return (
                    <div key={`funder#${index}`}>
                      <div className="summary-action-panel">
                        <h6 className="summary-label"><u>FUNDING #{index}</u></h6>
                        <br />
                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Grant or Program name</h6>
                            <h6 className="summary-value">{funder.GrantProgName}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Funding Agency</h6>
                            <h6 className="summary-value">{funder.FundingAgency}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Partnering Departments/Organisations</h6>
                            <h6 className="summary-value">{funder.PartnerDepsOrgs}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Project Coordinator</h6>
                            {this.getProjectManagerValue(funder.ProjectCoordinatorId)}
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Total Budget</h6>
                            <h6 className="summary-value">{funder.TotalBudget}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Annual Budget</h6>
                            <h6 className="summary-value">{funder.AnnualBudget}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Funding Status</h6>
                            {this.getFundingStatusValue(funder.FundingStatusId)}
                          </Col>
                        </Row>
                      </div>
                      <br />
                    </div>
                  )
                })
              }
            </div>
          </Panel>

          <Panel
            header={
              <h5 className="summary-panel-header" style={{ color: DEAGreenDark }}><u>ADAPTATION</u></h5>
            }
            key="3"
          >
            <div className="summary-panel">
              {
                adaptationDetails.map(adaptation => {

                  let index = adaptationDetails.indexOf(adaptation) + 1

                  return (
                    <div key={`adaptation#${index}`}>
                      <div className="summary-action-panel">
                        <h6 className="summary-label"><u>ADAPTATION #{index}</u></h6>
                        <br />
                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Title</h6>
                            <h6 className="summary-value">{adaptation.Title}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Description</h6>
                            <h6 className="summary-value">{adaptation.Description}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Purpose</h6>
                            {this.getAdaptationPurposeValue(adaptation.AdaptationPurposeId)}
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Sector</h6>
                            {this.getSectorValue(adaptation.SectorId)}
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Hazard</h6>
                            {this.getHazardValue(adaptation.HazardId)}
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Status</h6>
                            {this.getProjectStatusValue(adaptation.ProjectStatusId)}
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Contact Name</h6>
                            <h6 className="summary-value">{adaptation.ContactName}</h6>
                          </Col>
                        </Row>

                        <div className="vertical-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Contact Email Address</h6>
                            <h6 className="summary-value">{adaptation.ContactEmail}</h6>
                          </Col>
                        </Row>

                        {
                          adaptation.ResearchDetail &&
                          <div>
                            <div className="vertical-spacer" />
                            <div className="vertical-spacer" />
                            <h6 className="summary-label"><u>RESEARCH DETAILS</u></h6>
                            <div className="vertical-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Author</h6>
                                <h6 className="summary-value">{adaptation.ResearchDetail.Author}</h6>
                              </Col>
                            </Row>

                            <div className="vertical-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Paper Link</h6>
                                <h6 className="summary-value">{adaptation.ResearchDetail.PaperLink}</h6>
                              </Col>
                            </Row>

                            <div className="vertical-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Research Type</h6>
                                {this.getResearchTypeValue(adaptation.ResearchDetail.ResearchTypeId)}
                              </Col>
                            </Row>

                            <div className="vertical-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Target audience</h6>
                                {this.getTargetAudienceValue(adaptation.ResearchDetail.TargetAudienceId)}
                              </Col>
                            </Row>

                            <div className="vertical-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Research Maturity</h6>
                                {this.getResearchMaturityValue(adaptation.ResearchDetail.ResearchMaturityId)}
                              </Col>
                            </Row>

                            <div className="vertical-spacer" />

                          </div>
                        }
                      </div>
                      <br />
                    </div>
                  )
                })
              }
            </div>
          </Panel>

          <Panel
            header={
              <h5 className="summary-panel-header" style={{ color: DEAGreenDark }}><u>MITIGATION</u></h5>
            }
            key="4"
          >
            <div className="summary-panel">
              <h6 className="summary-label">*COMING SOON*</h6>
            </div>
          </Panel>

        </Collapse>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(OverallSummaryStep)