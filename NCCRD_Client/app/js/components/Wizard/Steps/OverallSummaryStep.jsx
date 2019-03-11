import React from 'react'
import { connect } from 'react-redux'
import { Row, Col, Button } from 'mdbreact'
import { Collapse } from 'antd';

const Panel = Collapse.Panel;

import './shared.css'
import "./OverallSummaryStep.css"

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
  }

  getProjectStatusValue(id) {

    let { projectStatus } = this.props
    let value = ""

    let filtered = projectStatus.filter(ps => ps.ProjectStatusId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getProjectRegionValue(id) {

    let { region } = this.props
    let value = ""

    let filtered = region.filter(r => r.Id === id.toString())
    if (filtered && filtered.length > 0) {
      value = filtered[0].Text
    }

    return (<h6 key={id} className="summary-value">{value}</h6>)
  }

  getProjectManagerValue(id) {

    let { users } = this.props
    let value = ""

    let filtered = users.filter(u => u.PersonId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getFundingStatusValue(id) {

    let { fundingStatus } = this.props
    let value = ""

    let filtered = fundingStatus.filter(fs => fs.FundingStatusId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getAdaptationPurposeValue(id) {

    let { adaptationPurpose } = this.props
    let value = ""

    let filtered = adaptationPurpose.filter(ap => ap.AdaptationPurposeId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getSectorValue(id) {

    let { sector } = this.props
    let value = ""

    let filtered = sector.filter(sec => sec.Id === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Text
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getHazardValue(id) {

    let { hazards } = this.props
    let value = ""

    let filtered = hazards.filter(haz => haz.Id === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Text
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getResearchTypeValue(id) {

    let { researchType } = this.props
    let value = ""

    let filtered = researchType.filter(r => r.ResearchTypeId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getTargetAudienceValue(id) {

    let { targetAudience } = this.props
    let value = ""

    let filtered = targetAudience.filter(ta => ta.TargetAudienceId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  getResearchMaturityValue(id) {

    let { researchMaturity } = this.props
    let value = ""

    let filtered = researchMaturity.filter(rm => rm.ResearchMaturityId === id)
    if (filtered && filtered.length > 0) {
      value = filtered[0].Value
    }

    return (<h6 className="summary-value">{value}</h6>)
  }

  render() {

    let { projectDetails, funderDetails, adaptationDetails } = this.props

    return (
      <>
        <h6 style={{ marginTop: -20 }}>
          <i>
            Please review before submitting
          </i>
        </h6>

        <Collapse className="summary-collapse" bordered={false}>
          <hr style={{ marginBottom: -5, color: "#F0F0F0", backgroundColor: "#F0F0F0" }} />

          <Panel
            header={
              <h5 className="summary-panel-header"><u>PROJECT</u></h5>
            }
            key="1"
          >
            <div className="summary-panel">
              <Row >
                <Col md="12">
                  <h6 className="summary-label">Title</h6>
                  <h6 className="summary-value">{projectDetails.ProjectTitle}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="12">
                  <h6 className="summary-label">Description</h6>
                  <h6 className="summary-value">{projectDetails.ProjectDescription}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="12">
                  <h6 className="summary-label">Link</h6>
                  <h6 className="summary-value">{projectDetails.Link}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

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

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Status</h6>
                  {this.getProjectStatusValue(projectDetails.ProjectStatusId)}
                </Col>
              </Row>

              <div className="horizontal-spacer" />

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

              <div className="horizontal-spacer" />

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

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Locations</h6>
                  {
                    (projectDetails.ProjectLocations && projectDetails.ProjectLocations.length > 0) &&
                    projectDetails.ProjectLocations.map(pl => {
                      return (
                        <h6 key={pl.ProjectLocationId} className="summary-value">{pl.Location.LatCalculated}, ${pl.Location.LonCalculated}</h6>
                      )
                    })
                  }
                  {
                    (!projectDetails.ProjectLocations || projectDetails.ProjectLocations.length === 0) &&
                    <h6 className="summary-value"></h6>
                  }
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Lead Organisation</h6>
                  <h6 className="summary-value">{projectDetails.LeadAgent}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Project Manager</h6>
                  {this.getProjectManagerValue(projectDetails.ProjectManagerId)}
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Host Organisation</h6>
                  <h6 className="summary-value">{projectDetails.HostOrganisation}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Host Partner</h6>
                  <h6 className="summary-value">{projectDetails.HostPartner}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Contact (alt)</h6>
                  <h6 className="summary-value">{projectDetails.AlternativeContact}</h6>
                </Col>
              </Row>

              <div className="horizontal-spacer" />

              <Row>
                <Col md="6">
                  <h6 className="summary-label">Contact Email (alt)</h6>
                  <h6 className="summary-value">{projectDetails.AlternativeContactEmail}</h6>
                </Col>
              </Row>

            </div>
          </Panel>

          <Panel
            header={
              <h5 className="summary-panel-header"><u>FUNDING</u></h5>
            }
            key="2"
          >
            <div className="summary-panel">
              {
                funderDetails.map(funder => {

                  let index = funderDetails.indexOf(funder) + 1

                  return (
                    <div>
                      <div className="summary-action-panel">
                        <h6 className="summary-label"><u>FUNDING #{index}</u></h6>
                        <br />
                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Grant or Program name</h6>
                            <h6 className="summary-value">{funder.GrantProgName}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Funding Agency</h6>
                            <h6 className="summary-value">{funder.FundingAgency}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Partnering Departments/Organisations</h6>
                            <h6 className="summary-value">{funder.PartnerDepsOrgs}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Project Coordinator</h6>
                            {this.getProjectManagerValue(funder.ProjectCoordinatorId)}
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Total Budget</h6>
                            <h6 className="summary-value">{funder.TotalBudget}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Annual Budget</h6>
                            <h6 className="summary-value">{funder.AnnualBudget}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

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
              <h5 className="summary-panel-header"><u>ADAPTATION</u></h5>
            }
            key="3"
          >
            <div className="summary-panel">
              {
                adaptationDetails.map(adaptation => {

                  let index = adaptationDetails.indexOf(adaptation) + 1

                  return (
                    <div>
                      <div className="summary-action-panel">
                        <h6 className="summary-label"><u>ADAPTATION #{index}</u></h6>
                        <br />
                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Title</h6>
                            <h6 className="summary-value">{adaptation.Title}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Description</h6>
                            <h6 className="summary-value">{adaptation.Description}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Purpose</h6>
                            {this.getAdaptationPurposeValue(adaptation.AdaptationPurposeId)}
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Sector</h6>
                            {this.getSectorValue(adaptation.SectorId)}
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Hazard</h6>
                            {this.getHazardValue(adaptation.HazardId)}
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Status</h6>
                            {this.getProjectStatusValue(adaptation.ProjectStatusId)}
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Contact Name</h6>
                            <h6 className="summary-value">{adaptation.ContactName}</h6>
                          </Col>
                        </Row>

                        <div className="horizontal-spacer" />

                        <Row>
                          <Col md="6">
                            <h6 className="summary-label">Contact Email Address</h6>
                            <h6 className="summary-value">{adaptation.ContactEmail}</h6>
                          </Col>
                        </Row>

                        {
                          adaptation.ResearchDetail &&
                          <div>
                            <div className="horizontal-spacer" />
                            <div className="horizontal-spacer" />
                            <h6 className="summary-label"><u>RESEARCH DETAILS</u></h6>
                            <div className="horizontal-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Author</h6>
                                <h6 className="summary-value">{adaptation.ResearchDetail.Author}</h6>
                              </Col>
                            </Row>

                            <div className="horizontal-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Paper Link</h6>
                                <h6 className="summary-value">{adaptation.ResearchDetail.PaperLink}</h6>
                              </Col>
                            </Row>

                            <div className="horizontal-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Research Type</h6>
                                {this.getResearchTypeValue(adaptation.ResearchDetail.ResearchTypeId)}
                              </Col>
                            </Row>

                            <div className="horizontal-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Target audience</h6>
                                {this.getTargetAudienceValue(adaptation.ResearchDetail.TargetAudienceId)}
                              </Col>
                            </Row>

                            <div className="horizontal-spacer" />

                            <Row>
                              <Col md="6">
                                <h6 className="summary-label">Research Maturity</h6>
                                {this.getResearchMaturityValue(adaptation.ResearchDetail.ResearchMaturityId)}
                              </Col>
                            </Row>

                            <div className="horizontal-spacer" />

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
              <h5 className="summary-panel-header"><u>MITIGATION</u></h5>
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