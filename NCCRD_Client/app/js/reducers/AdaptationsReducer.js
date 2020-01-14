'use strict'

const _gf = require("../globalFunctions")
const _ = require('lodash')

function extractItemAndId(array, key, value) {

    //Get item and Id
    let item = array.find(x => x[key] === value)
    let id = _.findIndex(array, (x) => x[key] === value)

    return { item, id }
}

export default function AdaptationsReducer(state = {}, action) {

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

        case "LOAD_ADAPTATION_DETAILS": {
            return { ...state, adaptationDetails: payload }
        }

        case "RESET_ADAPTATION_STATE": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", payload.AdaptationDetailId)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, state: "original" }] }
        }

        case "ADD_ADAPTATION_DETAILS": {

            let { adaptationDetails, projectDetails } = state

            let newItem = {
                AdaptationDetailId: _gf.getRndInteger(1111111, 9999999),
                Title: "",
                Description: "",
                ContactName: "",
                ContactEmail: "",
                AdaptationPurposeId: null,
                ProjectId: payload,
                SectorId: null,
                HazardId: null,
                ProjectStatusId: null,
                ResearchDetail: null,
                state: "modified"
            }

            return { ...state, adaptationDetails: [...adaptationDetails, newItem] }
        }

        case "REMOVE_ADAPTATION_DETAILS": {

            let { adaptationDetails } = state
            adaptationDetails.splice(payload, 1)

            return { ...state, adaptationDetails: [...adaptationDetails] }
        }

        case "SET_ADAPTATION_DETAILS_TITLE": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, Title: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_DESCR": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, Description: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_CONTACT_NAME": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ContactName: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_CONTACT_EMAIL": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ContactEmail: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_PURPOSE": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, AdaptationPurposeId: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_SECTOR": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, SectorId: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_HAZARD": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, HazardId: payload, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_PROJECT_STATUS": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ProjectStatusId: payload, state: modState }] }
        }

        case "ADD_ADAPTATION_DETAILS_RESEARCH_DETAILS": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //Add new research
            let newResearchDetails = {
                ResearchDetailId: _gf.getRndInteger(1111111, 9999999),
                Author: "",
                PaperLink: "",
                ResearchTypeId: null,
                TargetAudienceId: null,
                ResearchMaturityId: null
            }

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: newResearchDetails, state: modState }] }
        }

        case "SET_ADAPTATION_DETAILS_RESEARCH_DETAILS": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: payload, state: modState }] }
        }

        case "SET_ADAPTATION_RESEARCH_AUTHOR": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1);

            let researchDetail = details.item.ResearchDetail
            researchDetail.Author = payload

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_ADAPTATION_RESEARCH_PAPER_LINK": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.PaperLink = payload

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_ADAPTATION_RESEARCH_RESEARCH_TYPE": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.ResearchTypeId = payload

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_ADAPTATION_RESEARCH_TARGET_AUDIENCE": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.TargetAudienceId = payload

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_ADAPTATION_RESEARCH_MATURITY": {
            let { adaptationDetails } = state

            //Get item and Id
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            //Remove item from array
            adaptationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.ResearchMaturityId = payload

            //return updated state
            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_ADAPTATION_FUNDING_DETAIL": {
            let { adaptationDetails } = state
        
            //Get item and ID
            let details = extractItemAndId(adaptationDetails, "AdaptationDetailId", id)
            adaptationDetails.splice(details.id, 1)

            let fundingDetail = details.item.FundingDetail
            fundingDetail.FundingStatusId = payload

            return { ...state, adaptationDetails: [...adaptationDetails, { ...details.item, FundingDetail: fundingDetail, state: modState }]}
        }

        default: {
            return state
        }

    }
}
