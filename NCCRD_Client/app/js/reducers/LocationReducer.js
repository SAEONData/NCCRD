'use strict'

const _gf = require("../globalFunctions")
const _ = require('lodash')

function extractItemAndId(array, key, value) {

    //Get item and Id
    let item = array.find(x => x[key] === value)
    let id = _.findIndex(array, (x) => x[key] === value)

    return { item, id }
}

export default function LocationReducer(state = {}, action) {

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

        case "LOAD_LOCATION_DETAILS": {
            return { ...state, locationDetails: payload }
        }

        case "RESET_LOCATION_STATE": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", payload.LocationDetailId)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, state: "original" }] }
        }

        case "ADD_LOCATION_DETAILS": {

            let { locationDetails, projectDetails } = state

            let newItem = {
                LocationDetailId: _gf.getRndInteger(1111111, 9999999),
                Title: "",
                Description: "",
                ContactName: "",
                ContactEmail: "",
                LocationPurposeId: null,
                ProjectId: payload,
                SectorId: null,
                HazardId: null,
                ProjectStatusId: null,
                ResearchDetail: null,
                state: "modified"
            }

            return { ...state, locationDetails: [...locationDetails, newItem] }
        }

        case "REMOVE_LOCATION_DETAILS": {

            let { locationDetails } = state
            locationDetails.splice(payload, 1)

            return { ...state, locationDetails: [...locationDetails] }
        }

        case "SET_LOCATION_DETAILS_TITLE": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, Title: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_DESCR": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, Description: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_CONTACT_NAME": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ContactName: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_CONTACT_EMAIL": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ContactEmail: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_PURPOSE": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, LocationPurposeId: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_SECTOR": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, SectorId: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_HAZARD": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, HazardId: payload, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_PROJECT_STATUS": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ProjectStatusId: payload, state: modState }] }
        }

        case "ADD_LOCATION_DETAILS_RESEARCH_DETAILS": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

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
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: newResearchDetails, state: modState }] }
        }

        case "SET_LOCATION_DETAILS_RESEARCH_DETAILS": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: payload, state: modState }] }
        }

        case "SET_LOCATION_RESEARCH_AUTHOR": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1);

            let researchDetail = details.item.ResearchDetail
            researchDetail.Author = payload

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_LOCATION_RESEARCH_PAPER_LINK": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.PaperLink = payload

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_LOCATION_RESEARCH_RESEARCH_TYPE": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.ResearchTypeId = payload

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_LOCATION_RESEARCH_TARGET_AUDIENCE": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.TargetAudienceId = payload

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        case "SET_LOCATION_RESEARCH_MATURITY": {
            let { locationDetails } = state

            //Get item and Id
            let details = extractItemAndId(locationDetails, "LocationDetailId", id)
            //Remove item from array
            locationDetails.splice(details.id, 1)

            let researchDetail = details.item.ResearchDetail
            researchDetail.ResearchMaturityId = payload

            //return updated state
            return { ...state, locationDetails: [...locationDetails, { ...details.item, ResearchDetail: researchDetail, state: modState }] }
        }

        default: {
            return state
        }

    }
}
