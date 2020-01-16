'use strict'

import { createStore, combineReducers } from 'redux'
import { routerReducer } from 'react-router-redux'
import reducers from './reducers'
import { loadUser } from 'redux-oidc'
import userManager from './components/Authentication/userManager'
import { reducer as oidcReducer } from 'redux-oidc';

const store = createStore(
    combineReducers({ oidc: oidcReducer, ...reducers, router: routerReducer }), {

        globalData: {
            loading: false,
            editMode: false,
            readOnly: false,
            daoid: true,
            showSideNavButton: true,
            showSideNav: false,
            showHeader: true,
            showNavbar: true,
            showFooter: true,
            showListExpandCollapse: true,
            showListViewOption: true,
            showFavoritesOption: true,
            showListFilterOptions: true,
            showBackToList: true,            
            showDetailsInParent: false,
            showInputWizard: false,
            projectsFullView: false
        },

        navigation: {
            locationHash: "#/"
        },

        editListModalData: {
            show: false,
            data: [],
            dispatch: "",
            persist: "",
            type: "std",
            dependencies: [],
            newItemTemplate: {}
        },

        projectData: {
            projects: [],
            projectDetails: {},
            selectedProjectId: 0,
            filteredProjectIDs: [],
            start: 0,
            end: 25,
            listScrollPos: 0
        },

        projectFundersData: {
            projectFunderDetails: [],
            adaptationDetails: [],
            mitigationDetails: []
        },

        adaptationData: {
            adaptationDetails: [],
            // projectFunderDetails: []
        },

        mitigationData: {
            mitigationDetails: [],
            projectFunderDetails: []
        },

        emissionsData: {
            emissionsData: []
        },

        lookupData: {
            projectTypes: [],
            projectSubTypes: [],
            projectStatus: [],
            users: [],
            validationStatus: [],
            typology: [],
            regionTree: [],
            region: [],
            sectorTree: [],
            sector: [],
            sectorType: [],
            maOptions: [],
            adaptationPurpose: [],
            carbonCredit: [],
            carbonCreditMarket: [],
            cdmStatus: [],
            cdmMethodology: [],
            voluntaryMethodology: [],
            voluntaryGoldStandard: [],
            researchType: [],
            targetAudience: [],
            hazards: [],
            researchMaturity: [],
            loaded: false
        },

        filterData: {
            filtersChanged: false,
            titleFilter: "",
            titleFilterInternal: "",
            statusFilter: 0,
            typologyFilter: 0,
            regionFilter: 0,
            sectorFilter: 0,
            hazardFilter: 0,
            polygonFilter: "",
            favoritesFilter: false,
            unverifiedOnlyFilter: false
        },

        chartData:{
            chart1: [],
            chart2: [],
            chart3: [],
            chart4: []
        }

    }, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
)
loadUser(store, userManager)

export default store