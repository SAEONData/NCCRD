'use strict'

let url

if(CONSTANTS.PRODUCTION) {
    url = 'http://app01.saeon.ac.za/nccrdapi/odata'
} else {
    url = 'http://localhost:62553/odata/'
}

export const apiBaseURL = url
