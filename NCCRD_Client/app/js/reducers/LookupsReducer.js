'use strict'

export default function LookupsReducer(state = {}, action) {

  const { type, payload } = action

  const transformVMSData = (payload) => {
    let data = [];
    payload.forEach(x => {
      let item = { Id: x.id, Text: x.value, ParentId: null }

      if (x.additionalData.length > 0) {
        let addData = x.additionalData.filter(x => x.key === "ParentId")
        if (addData.length > 0) {
          item.ParentId = addData[0].value
        }
      }

      data.push(item)
    })

    return data
  }

  switch (type) {

    case "LOAD_PROJECT_STATUS": {
      return { ...state, projectStatus: payload }
    }

    case "LOAD_FUNDERS": {
      return { ...state, funders: payload }
    }

    case "LOAD_FUNDINGSTATUS": {
      return { ...state, fundingStatus: payload }
    }

    case "LOAD_FUNDINGTYPE" : {
      return { ...state, fundingType: payload }
    }

    case "LOAD_TYPOLOGY": {
      return { ...state, typology: payload }
    }

    case "LOAD_PROJECT_TYPE": {
      return { ...state, projectTypes: payload }
    }

    case "LOAD_PROJECT_SUBTYPE": {
      return { ...state, projectSubTypes: payload }
    }

    case "LOAD_PROJECT_STATUS": {
      return { ...state, projectStatus: payload }
    }

    case "LOAD_USERS": {
      payload.forEach(x => {
        x.Value = (x.FirstName + " " + x.Surname + " (" + x.EmailAddress + ")")
      })
      payload.sort((a,b) => (a.Value > b.Value) ? 1 : ((b.Value > a.Value) ? -1 : 0)); 
      return { ...state, users: payload }
    }

    case "LOAD_VALIDATION_STATUS": {
      return { ...state, validationStatus: payload }
    }

    case "LOAD_MA_OPTIONS": {
      return { ...state, maOptions: payload }
    }

    case "LOAD_ADAPTATION_PURPOSE": {
      return { ...state, adaptationPurpose: payload }
    }

    case "LOAD_SECTOR": {
      let data = transformVMSData(payload)
      return { ...state, sector: data }
    }

    case "LOAD_SECTOR_TYPE": {
      return { ...state, sectorType: payload }
    }

    case "LOAD_REGION": {
      let data = transformVMSData(payload)
      return { ...state, region: data }
    }

    case "LOAD_CARBON_CREDIT": {
      return { ...state, carbonCredit: payload }
    }

    case "LOAD_CARBON_CREDIT_MARKET": {

      return { ...state, carbonCreditMarket: payload }
    }

    case "LOAD_CDM_STATUS": {
      return { ...state, cdmStatus: payload }
    }

    case "LOAD_CDM_METHODOLOGY": {
      return { ...state, cdmMethodology: payload }
    }

    case "LOAD_VOLUNTARY_METHODOLOGY": {
      return { ...state, voluntaryMethodology: payload }
    }

    case "LOAD_VOLUNTARY_GOLD_STANDARD": {
      return { ...state, voluntaryGoldStandard: payload }
    }

    case "LOAD_RESEARCH_TYPE": {
      return { ...state, researchType: payload }
    }

    case "LOAD_TARGET_AUDIENCE": {
      return { ...state, targetAudience: payload }
    }

    case "LOAD_HAZARDS": {
      let data = transformVMSData(payload)
      return { ...state, hazards: data }
    }

    case "LOAD_RESEARCH_MATURITY": {
      return { ...state, researchMaturity: payload }
    }

    case "SET_LOOKUPS_LOADED": {
      return { ...state, loaded: payload }
    }


    default: {
      return state
    }
  }
}