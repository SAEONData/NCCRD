'use strict'

const _gf = require("../globalFunctions")
const _ = require('lodash')

function extractItemAndId(array, key, value) {

    //Get item and Id
    let item = array.find(x => x[key] === value)
    let id = _.findIndex(array, (x) => x[key] === value)

    return { item, id }
}

export default function MitigationsReducer(state = {}, action) {

    let { type, payload } = action
    let id = 0
    let modState = "original"

    //Check if additional data embedded in payload
    if (typeof payload !== 'undefined') {
        if (typeof payload.id !== 'undefined') {
            id = parseInt(payload.id)
        }
        if (typeof payload.state !== 'undefined') {
            modState = payload.state
        }
        if (typeof payload.value !== 'undefined') {
            payload = payload.value
        }
    }

    switch (type) {

        case "LOAD_MITIGATION_DETAILS": {
            return { ...state, mitigationDetails: payload }
        }

        case "RESET_MITIGATION_STATE": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", payload.MitigationDetailId)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, state: "original" }] }
        }

        case "ADD_MITIGATION_DETAILS": { 

            let { mitigationDetails, projectDetails } = state

            let newItem = {
                MitigationDetailId: _gf.getRndInteger(1111111, 9999999),
                VCS: 0,
                Other: 0,
                OtherDescription: "",
                CDMProjectNumber: "",
                CarbonCreditId: null,
                CarbonCreditMarketId: null,
                CDMStatusId: null,
                CDMMethodologyId: null,
                MitigationFundingId: null,
                VoluntaryMethodologyId: null,
                VoluntaryGoldStandardId: null,
                ProjectId: payload,
                SectorId: null,
                ProjectStatusId: null,
                ResearchDetail: null,
                state: "modified"
            }

            // if (mitigationDetails.length > 0) {
            //     return { ...state, mitigationDetails: [...mitigationDetails, newItem] }
            // }
            // else {
            //     return { ...state, mitigationDetails: [newItem] }
            // }

            return { ...state, mitigationDetails: [...mitigationDetails, newItem]}
        }

        case "REMOVE_MITIGATION_DETAILS": {

            let { mitigationDetails } = state
            mitigationDetails.splice(payload, 1)

            return { ...state, mitigationDetails: [...mitigationDetails] }
        }

        case "SET_MITIGATION_DETAILS_TITLE": {

            let { mitigationDetails } = state
            //get item and id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //remove item from array
            mitigationDetails.splice(details.id, 1)

            return { 
                ...state, 
                mitigationDetails: [
                    ...mitigationDetails, { 
                        ...details.id,
                        Title: payload, state: modstate
                    }
                ]
            }
        }

        case "SET_MITIGATION_CARBON_CREDIT": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)

            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, CarbonCreditId: payload, state: modState }] }
        }

        case "SET_MITIGATION_CARBON_CREDIT_MARKET": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, CarbonCreditMarketId: payload, state: modState }] }
        }

        case "SET_MITIGATION_CDM_STATUS": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, CDMStatusId: payload, state: modState }] }
        }

        case "SET_MITIGATION_CDM_METHODOLOGY": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, CDMMethodologyId: payload, state: modState }] }
        }

        case "SET_MITIGATION_FUNDING_DETAIL": {
            let { mitigationDetails } = state

            //get item and id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //remove item from arary
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [ ...mitigationDetails, { ...details.item, MitigationFundingId: payload, modState }]}
        }

        case "SET_MITIGATION_VOLUNTARY_METHODOLOGY": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, VoluntaryMethodologyId: payload, state: modState }] }
        }

        case "SET_MITIGATION_VOLUNTARY_GOLD_STANDARD": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, VoluntaryGoldStandardId: payload, state: modState }] }
        }

        case "SET_MITIGATION_CDM_PROJECT_NUMBER": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, CDMProjectNumber: payload, state: modState }] }
        }

        case "SET_MITIGATION_OTHER_DESCR": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, OtherDescription: payload, state: modState }] }
        }

        case "SET_MITIGATION_SECTOR": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, SectorId: payload, state: modState }] }
        }

        case "SET_MITIGATION_DETAILS_PROJECT_STATUS": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ProjectStatusId: payload, state: modState }] }
        }

        case "SET_MITIGATION_DETAILS_RESEARCH_DETAILS": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)

            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ResearchDetail: payload, state: modState }] }
        }

        case "SET_MITIGATION_RESEARCH_AUTHOR": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)

            //Remove item from array
            mitigationDetails.splice(details.id, 1);

            let researchDetail = details.item.ResearchDetail
            researchDetail.Author = payload

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_MITIGATION_RESEARCH_PAPER_LINK": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.PaperLink = payload

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_MITIGATION_RESEARCH_RESEARCH_TYPE": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.ResearchTypeId = payload

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_MITIGATION_RESEARCH_TARGET_AUDIENCE": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.TargetAudienceId = payload

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_MITIGATION_RESEARCH_MATURITY": {
            let { mitigationDetails } = state

            //Get item and Id
            let details = extractItemAndId(mitigationDetails, "MitigationDetailId", id)
            //Remove item from array
            mitigationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.ResearchMaturityId = payload

            //return updated state
            return { ...state, mitigationDetails: [...mitigationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        default: {
            return state
        }

    }
}
