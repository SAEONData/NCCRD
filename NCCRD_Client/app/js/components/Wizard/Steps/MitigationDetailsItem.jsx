import React from 'react'
import { apiBaseURL } from "../../../config/serviceURLs.js"
import { Row, Col } from 'mdbreact'
import { connect } from 'react-redux'
import TextComponent from '../../Shared/TextComponent.jsx'
import SelectComponent from '../../Shared/SelectComponent.jsx'
import ReactTooltip from 'react-tooltip'
import TreeSelectComponent from '../../Shared/TreeSelectComponent.jsx'
import { DEAGreenDark } from '../../../config/colours.js'

const _gf = require("../../../globalFunctions")

const mapStateToProps = (state, props) => {
  let { mitigationData: { mitigationDetails } } = state
  let { globalData: { editMode } } = state
  let { lookupData: {
    researchType, targetAudience, carbonCredit, carbonCreditMarket, cdmStatus, cdmMethodology, projectStatus,
    voluntaryMethodology, voluntaryGoldStandard, sector, sectorType, typology, researchMaturity
  } } = state
  return {
    researchType, targetAudience, carbonCredit,
    carbonCreditMarket, cdmStatus, cdmMethodology, projectStatus,
    voluntaryMethodology, voluntaryGoldStandard, sector, sectorType, typology,
    mitigationDetails, editMode, researchMaturity
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setMitigationDetailsResearchDetails: payload => {
      dispatch({ type: "SET_MITIGATION_DETAILS_RESEARCH_DETAILS", payload })
    }
  }
}

class MitigationDetailsItem extends React.Component {

  constructor(props) {
    super(props)
  }

  onResearchChange(value) {

    let { details } = this.props

    if (details.ResearchDetail) {
      //Disable
      details.ResearchDetail = null
    }
    else {
      //Enable
      details.ResearchDetail = {
        ResearchDetailId: _gf.getRndInteger(1111111, 9999999),
        Author: "",
        PaperLink: "",
        ResearchTypeId: 0,
        TargetAudienceId: 0,
        ProjectId: details.ProjectId,
        SectorId: null,
        ResearchMaturityId: null
      }
    }

    //Dispatch
    this.props.setMitigationDetailsResearchDetails({
      id: details.MitigationDetailId,
      value: details.ResearchDetail,
      state: 'modified'
    })
  }

  render() {

    let { details, carbonCredit, carbonCreditMarket, cdmStatus, cdmMethodology, projectStatus, editMode,
      voluntaryMethodology, voluntaryGoldStandard, sector, sectorType, typology, mitigationDetails,
      researchType, targetAudience, researchMaturity } = this.props

    return (
      <>

        <Row style={{ marginBottom: 10 }}>
          <Col md="6">
            <label style={{ fontWeight: "bold" }}>
              Research project:
            </label>
            <br />
            <label className="bs-switch">
              <input disabled={!editMode} type="checkbox" checked={details.ResearchDetail !== null} onClick={this.onResearchChange.bind(this)} />
              <span className="slider round" />
            </label>
          </Col>
        </Row>

        <div className="row">
          <SelectComponent
            id="selMitigationCarbonCredit"
            col="col-md-4"
            label="Carbon credit:"
            selectedValue={details.CarbonCreditId}
            data={carbonCredit}
            setSelectedValueKey={"SET_MITIGATION_CARBON_CREDIT"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_CARBON_CREDIT"}
            persist="CarbonCredit"
            allowEdit={false}
            newItemTemplate={{
              "CarbonCreditId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
          <SelectComponent
            id="selMitigationCarbonCreditMarket"
            col="col-md-4"
            label="Carbon credit market:"
            selectedValue={details.CarbonCreditMarketId}
            data={carbonCreditMarket}
            setSelectedValueKey={"SET_MITIGATION_CARBON_CREDIT_MARKET"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_CARBON_CREDIT_MARKET"}
            persist="CarbonCreditMarket"
            allowEdit={false}
            newItemTemplate={{
              "CarbonCreditMarketId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
          <SelectComponent
            id="selMitigationCDMStatus"
            col="col-md-4"
            label="CDM status:"
            selectedValue={details.CDMStatusId}
            data={cdmStatus}
            setSelectedValueKey={"SET_MITIGATION_CDM_STATUS"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_CDM_STATUS"}
            persist="CDMStatus"
            allowEdit={false}
            newItemTemplate={{
              "CDMStatusId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
        </div>

        <br />

        <div className="row">
          <SelectComponent
            id="selMitigationCDMMethodology"
            col="col-md-4"
            label="CDM methodology:"
            selectedValue={details.CDMMethodologyId}
            data={cdmMethodology}
            setSelectedValueKey={"SET_MITIGATION_CDM_METHODOLOGY"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_CDM_METHODOLOGY"}
            persist="CDMMethodology"
            allowEdit={false}
            newItemTemplate={{
              "CDMMethodologyId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
          <SelectComponent
            id="selMitigationVoluntaryMethodology"
            col="col-md-4"
            label="Voluntary methodology:"
            selectedValue={details.VoluntaryMethodologyId}
            data={voluntaryMethodology}
            setSelectedValueKey={"SET_MITIGATION_VOLUNTARY_METHODOLOGY"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_VOLUNTARY_METHODOLOGY"}
            persist="VoluntaryMethodology"
            allowEdit={false}
            newItemTemplate={{
              "VoluntaryMethodologyId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
          <SelectComponent
            id="selMitigationVoluntaryGoldStandard"
            col="col-md-4"
            label="Voluntary gold standard:"
            selectedValue={details.VoluntaryGoldStandardId}
            data={voluntaryGoldStandard}
            setSelectedValueKey={"SET_MITIGATION_VOLUNTARY_GOLD_STANDARD"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_VOLUNTARY_GOLD_STANDARD"}
            persist="VoluntaryGoldStandard"
            allowEdit={false}
            newItemTemplate={{
              "VoluntaryGoldStandardId": 0,
              "Value": "",
              "Description": ""
            }}
            allowClear={true}
          />
        </div>

        <br />

        <div className="row">
          <TextComponent
            col="col-md-4"
            label="CDM project number:"
            id="txtMitigationCDMProjectNumber"
            value={details.CDMProjectNumber}
            setValueKey={"SET_MITIGATION_CDM_PROJECT_NUMBER"}
            parentId={details.MitigationDetailId}
          />
          <TextComponent
            col="col-md-4"
            label="Other description:"
            id="txtMitigationOtherDescription"
            value={details.OtherDescription}
            setValueKey={"SET_MITIGATION_OTHER_DESCR"}
            parentId={details.MitigationDetailId}
          />
          <TreeSelectComponent
            id="selMitigationSector"
            col="col-md-4"
            label="Sector:"
            selectedValue={details.SectorId}
            data={sector}
            setSelectedValueKey={"SET_MITIGATION_SECTOR"}
            parentId={details.MitigationDetailId}
            dispatch={"LOAD_SECTOR"}
            persist="Sector"
            type="tree"
            dependencies={[
              { key: "SectorTypeId", value: sectorType, type: "std" },
              { key: "ParentSectorId", value: sector, type: "tree" },
              { key: "TypologyId", value: typology, type: "std" }
            ]}
            allowEdit={false}
            newItemTemplate={{
              "SectorId": 0,
              "Value": "",
              "SectorTypeId": 0,
              "ParentSectorId": 0,
              "TypologyId": 0
            }}
          />
        </div>

        <br />

        <div className="row">
          <SelectComponent
            id="selProjectStatus"
            col="col-md-4"
            label="Status:"
            selectedValue={details.ProjectStatusId}
            data={this.props.projectStatus}
            setSelectedValueKey={"SET_MITIGATION_DETAILS_PROJECT_STATUS"}
            parentId={details.MitigationDetailId}
            allowEdit={false}
            allowClear={true}
          />
        </div>

        <br />

        {
          details.ResearchDetail &&
          <div>

            <Row>
              <TextComponent
                col="col-md-4"
                label="Author:"
                id="txtResearchAuthor"
                value={details.ResearchDetail.Author}
                setValueKey={"SET_MITIGATION_RESEARCH_AUTHOR"}
                parentId={details.MitigationDetailId}
              />
              <TextComponent
                col="col-md-4"
                label="Paper link:"
                id="txtResearchPaperLink"
                value={details.ResearchDetail.PaperLink}
                setValueKey={"SET_MITIGATION_RESEARCH_PAPER_LINK"}
                parentId={details.MitigationDetailId}
              />
              <SelectComponent
                id="selResearchType"
                col="col-md-4"
                label="Research type:"
                selectedValue={details.ResearchDetail.ResearchTypeId}
                data={researchType}
                setSelectedValueKey={"SET_MITIGATION_RESEARCH_RESEARCH_TYPE"}
                parentId={details.MitigationDetailId}
                dispatch={"LOAD_RESEARCH_TYPE"}
                persist="ResearchType"
                allowEdit={true}
                newItemTemplate={{
                  "ResearchTypeId": 0,
                  "Value": "",
                  "Description": ""
                }}
                allowClear={true}
              />
            </Row>

            <br />

            <Row>
              <SelectComponent
                id="selResearchTargetAudience"
                col="col-md-4"
                label="Target audience:"
                selectedValue={details.ResearchDetail.TargetAudienceId}
                data={targetAudience}
                setSelectedValueKey={"SET_MITIGATION_RESEARCH_TARGET_AUDIENCE"}
                parentId={details.MitigationDetailId}
                dispatch={"LOAD_TARGET_AUDIENCE"}
                persist="TargetAudience"
                allowEdit={true}
                newItemTemplate={{
                  "TargetAudienceId": 0,
                  "Value": "",
                  "Description": ""
                }}
                allowClear={true}
              />

              <SelectComponent
                id="selResearchResearchMaturity"
                col="col-md-4"
                label="Research Maturity:"
                selectedValue={details.ResearchDetail.ResearchMaturityId}
                data={researchMaturity}
                setSelectedValueKey={"SET_MITIGATION_RESEARCH_MATURITY"}
                parentId={details.MitigationDetailId}
                dispatch={"LOAD_RESEARCH_MATURITY"}
                persist="ResearchMaturity"
                allowEdit={false}
                newItemTemplate={{
                  "ResearchMaturityId": 0,
                  "Value": "",
                  "Description": ""
                }}
                allowClear={true}
              />
            </Row>

          </div>
        }

        {
          (mitigationDetails && mitigationDetails.length > 1) &&
          <div
            style={{
              height: "5px",
              backgroundColor: "#white",
              margin: "35px -16px 30px -16px",
              borderStyle: "double none none none",
              borderColor: "#C8C8C8"
            }}
          />
        }

        <ReactTooltip delayShow={700} />
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MitigationDetailsItem)