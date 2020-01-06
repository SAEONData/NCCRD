'use strict'

const _gf = require("../globalFunctions")
const _ = require('lodash')

function extractItemAndId(array, key, value) {

    //Get item and Id
    let item = array.find(x => x[key] === value)
    let id = _.findIndex(array, (x) => x[key] === value)

    return { item, id }
}

export default function ProjectFundersReducer(state = {}, action) {

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

        case "LOAD_PROJECTFUNDER_DETAILS": {    
            
            payload.forEach(x => x.key = new Date().valueOf())            
            return { ...state, projectFunderDetails: payload }
        }

        case "RESET_PROJECTFUNDER_STATE": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", payload.FunderId)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, state: "original" }] }
        }

        case "ADD_PROJECTFUNDER_DETAILS": {

            let { projectFunderDetails, projectDetails } = state

            let newItem = {
                FunderId: _gf.getRndInteger(1111111, 9999999),
                FundingAgency: "",
                GrantProgName: "",
                TotalBudget: null,
                AnnualBudget: null,
                PartnerDepsOrgs: "",
                ProjectCoordinatorId: null,
                FundingStatusId: null,
                state: "modified"
            }

            return { ...state, projectFunderDetails: [...projectFunderDetails, newItem] }
        }

        case "REMOVE_PROJECTFUNDER_DETAILS": {

            let { projectFunderDetails } = state
            projectFunderDetails.splice(payload, 1)

            return { ...state, projectFunderDetails: [...projectFunderDetails] }
        }

        case "SET_PROJECTFUNDER_FUNDINGAGENCY": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, FundingAgency: payload, state: modState }] }
        }

        case "SET_PROJECTFUNDER_GRANTPROGNAME": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, GrantProgName: payload, state: modState }] }
        }

        case "SET_PROJECTFUNDER_PARTNERDEPSORGS": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, PartnerDepsOrgs: payload, state: modState }] }
        }

        case "SET_PROJECTFUNDER_TOTALBUDGET": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, TotalBudget: parseFloat(payload), state: modState }] }
        }

        case "SET_PROJECTFUNDER_ANNUALBUDGET": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, AnnualBudget: parseFloat(payload), state: modState }] }
        }

        case "SET_PROJECTFUNDERS_PROJECTCOORDINATOR": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, ProjectCoordinatorId: payload, state: modState }] }
        }

        case "SET_PROJECTFUNDERS_FUNDINGSTATUS": {
            let { projectFunderDetails } = state

            //Get item and Id
            let details = extractItemAndId(projectFunderDetails, "FunderId", id)
            //Remove item from array
            projectFunderDetails.splice(details.id, 1);

            //return updated state
            return { ...state, projectFunderDetails: [...projectFunderDetails, { ...details.item, FundingStatusId: payload, state: modState }] }
        }

        default: {
            return state
        }

    }
}
