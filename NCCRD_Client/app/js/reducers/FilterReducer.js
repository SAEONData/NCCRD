'use strict'

export default function FilterReducer(state = {}, action) {

  let { type, payload } = action

  let id = 0
  let modState = "original"

  //Check if additional data embedded in payload
  if (typeof payload !== 'undefined') {
    if (typeof payload.id !== 'undefined') {
      id = parseInt(payload.id)
    }
    if (typeof payload.action !== 'undefined') {
      action = payload.action
    }
    if (typeof payload.state !== 'undefined') {
      modState = payload.state
    }
    if (typeof payload.value !== 'undefined') {
      payload = payload.value
    }
  }

  switch (type) {

    case "LOAD_TITLE_FILTER": {
      return { ...state, titleFilterInternal: payload, titleFilter: payload, filtersChanged: true }
    }

    case "LOAD_TITLE_FILTER_INTERNAL": {
      return { ...state, titleFilterInternal: payload }
    }

    case "LOAD_STATUS_FILTER": {
      return { ...state, statusFilter: payload, filtersChanged: true }
    }

    case "LOAD_TYPOLOGY_FILTER": {
      return { ...state, typologyFilter: payload, filtersChanged: true }
    }

    case "LOAD_REGION_FILTER": {
      return { ...state, regionFilter: payload, filtersChanged: true }
    }

    case "LOAD_SECTOR_FILTER": {
      return { ...state, sectorFilter: payload, filtersChanged: true }
    }

    case "LOAD_HAZARD_FILTER": {
      return { ...state, hazardFilter: payload, filtersChanged: true }
    }

    case "LOAD_POLYGON_FILTER": {
      return { ...state, polygonFilter: payload, filtersChanged: true }
    }

    case "TOGGLE_FAVS_FILTER": {
      return { ...state, favoritesFilter: payload, filtersChanged: true }
    }

    case "CLEAR_FILTERS": {
      return {
        ...state,
        titleFilter: "",
        statusFilter: 0,
        typologyFilter: 0,
        regionFilter: 0,
        sectorFilter: 0,
        hazardFilter: 0,
        polygonFilter: ""
      }
    }

    case 'SET_FILTERS_CHANGED': {
      return { ...state, filtersChanged: payload }
    }

    case 'TOGGLE_UNVERIFIED_ONLY_FILTER': {
      return { ...state, unverifiedOnlyFilter: payload, filtersChanged: true }
    }

    default: {
      return state
    }

  }
}