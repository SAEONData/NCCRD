let _apiBaseURL = ''
let _siteBaseURL = ''
let _vmsBaseURL = ''
let _ssoBaseURL = ''
let _nccisBaseURL = ''
let _ndaoBaseURL = ''
let _ndaoSiteBaseURL = ''
let _ndmcBaseURL = ''
let _mapServerBaseURL = ''


if (CONSTANTS.DEV) {
  _apiBaseURL = 'https://ccis.environment.gov.za/nccrd/api/odata/'
  _siteBaseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}/`
  _ndmcBaseURL = 'https://ccis.environment.gov.za/nhe/'
  _ndaoBaseURL = 'https://ccis.environment.gov.za/ndao/api/odata/'
  _ndaoSiteBaseURL = 'https://ccis.environment.gov.za/ndao/'
  _vmsBaseURL = 'https://ccis.environment.gov.za/vms/api/'
  _ssoBaseURL = 'https://identity.saeon.ac.za/'
  _mapServerBaseURL = 'https://ccis.environment.gov.za/map'
  // _apiBaseURL = 'http://192.168.105.102/nccrd/api/odata/'
  // _siteBaseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}/`
  // _ndmcBaseURL = 'http://192.168.105.102/nhe/'
  // _ndaoBaseURL = 'http://192.168.105.102/ndao/api/odata/'
  // _ndaoSiteBaseURL = 'http://192.168.105.102/ndao/'
  // _vmsBaseURL = 'http://192.168.105.102/vms/api/'
  // _ssoBaseURL = 'https://identity.saeon.ac.za/'
  // _mapServerBaseURL = 'http://192.168.105.102/map'
}
else if (CONSTANTS.TEST) {
  _apiBaseURL = 'http://192.168.105.102/nccrd/api/odata/'
  _siteBaseURL = 'http://192.168.105.102/nccrd/'
  _ndmcBaseURL = 'http://192.168.105.102/nhe/'
  _ndaoBaseURL = 'http://192.168.105.102/ndao/api/odata/'
  _ndaoSiteBaseURL = 'http://192.168.105.102/ndao/'
  _vmsBaseURL = 'http://192.168.105.102/vms/api/'
  _ssoBaseURL = 'https://identity.saeon.ac.za/'
  _mapServerBaseURL = 'http://192.168.105.102/map' //'http://app01.saeon.ac.za'
}
else if (CONSTANTS.PROD) {
  _apiBaseURL = 'https://ccis.environment.gov.za/nccrd/api/odata/'
  _siteBaseURL = 'https://ccis.environment.gov.za/nccrd/'
  _ndmcBaseURL = 'https://ccis.environment.gov.za/nhe/'
  _ndaoBaseURL = 'https://ccis.environment.gov.za/ndao/api/odata/'
  _ndaoSiteBaseURL = 'https://ccis.environment.gov.za/ndao/'
  _vmsBaseURL = 'https://ccis.environment.gov.za/vms/api/'
  _ssoBaseURL = 'https://identity.saeon.ac.za/'
  _mapServerBaseURL = 'https://ccis.environment.gov.za/map'
}

export const apiBaseURL = _apiBaseURL
export const siteBaseURL = _siteBaseURL
export const vmsBaseURL = _vmsBaseURL
export const ssoBaseURL = _ssoBaseURL
export const nccisBaseURL = _nccisBaseURL
export const ndmcBaseURL = _ndmcBaseURL
export const ndaoBaseURL = _ndaoBaseURL
export const ndaoSiteBaseURL = _ndaoSiteBaseURL
export const mapServerBaseURL = _mapServerBaseURL
