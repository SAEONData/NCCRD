import { siteBaseURL, apiBaseURL, nccisBaseURL } from '../js/config/serviceURLs.js'

export const MapConfig = {

    service: "http://192.168.105.112/nccrd/api/odata/projects/extensions.geojson",
    // service: "http://192.168.105.102/nccrd/api/odata/projects/extensions.geojson",
    domain: siteBaseURL,
    IDField: "properties.id",
    toolTipTitle: "properties.name",
    toolTipFields: [
        {
            field: "data.startYear", 
            alias: "Start Year"
        },
        {
            field: "data.endYear",
            alias: "End Year"
        },
        {
            field: "properties.id",
            alias: "Project ID"
        }
    ],
    styleField: "properties.typology",
    styles: [
        {
            value: 1,
            title: "Mitigation",
            default: true,
            icon: "http://192.168.105.112/map/blue_2.png", // 
            anchorX: 0,
            anchorY: 8
        },
        {
            value: 2,
            title: "Adaptation",
            icon: "http://192.168.105.112/map/green.png", // //
            anchorX: 0,
            anchorY: 8
        },
        {
            value: 3,
            title: "Research",
            icon:  "http://192.168.105.112/map/red.png", //
            anchorX: 0,
            anchorY: 8
        }
    ]
}