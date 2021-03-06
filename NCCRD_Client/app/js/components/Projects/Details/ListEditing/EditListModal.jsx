import React from 'react'
import { connect } from 'react-redux'
import {
  ListGroup, ListGroupItem, Input, Button, Container, Modal, ModalBody, ModalHeader, ModalFooter,
  Select, SelectInput, SelectOptions, SelectOption
} from 'mdbreact'
import { apiBaseURL } from "../../../../config/serviceURLs.js"

const _gf = require("../../../../globalFunctions")
const _ = require('lodash')
const o = require("odata")

const mapStateToProps = (state, props) => {
  let { editListModalData: { show, data, dispatch, persist, dependencies, newItemTemplate } } = state
  let user = state.oidc.user
  return { show, data, dispatch, persist, dependencies, newItemTemplate, user }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setEditList: (payload) => {
      dispatch({ type: "SET_EDIT_LIST", payload })
    },
    setLoading: (payload) => {
      dispatch({ type: "SET_LOADING", payload })
    },
    dispatchToStore: (key, payload) => {
      dispatch({ type: key, payload })
    }
  }
}

class EditListModal extends React.Component {

  constructor(props) {
    super(props)

    this.cancel = this.cancel.bind(this)
    this.renderList = this.renderList.bind(this)
    this.renderDetails = this.renderDetails.bind(this)
    this.dependencySelect = this.dependencySelect.bind(this)
    this.cloneData = this.cloneData.bind(this)

    this.state = { _data: [], selectedItemId: 0, confirmSave: false }
  }

  componentDidUpdate() {
    this.cloneData()
  }

  componentDidMount() {
    this.cloneData()
  }

  cloneData() {
    let { data, setEditList } = this.props

    if (data.length > 0) {

      let tmpData = [] //_.clone(data)
      data.map(item => {
        let clone = _.clone(item)
        delete clone.modifiedState
        tmpData.push(clone)
      })

      //Clear items from store
      setEditList({ data: [] })

      //Update local state
      this.setState({ _data: tmpData })
    }
  }

  add() {

    //Add new item
    let { newItemTemplate } = this.props
    let { _data } = this.state

    //Clone existing item
    let newItem = _.clone(newItemTemplate)

    //Clear values
    Object.keys(newItem).map(key => {
      newItem[key] = ""
    })

    //Set Id to GUID
    let newItemId = _gf.getRndInteger(1111111, 9999999)
    newItem[Object.keys(newItem)[0]] = newItemId
    newItem[Object.keys(newItem)[1]] = "ENTER VALUE HERE"
    newItem.modifiedState = true

    //Update state
    _data.splice(0, 0, newItem)
    this.setState({ _data: _data, selectedItemId: newItemId })
  }

  save() {
    //Toggle confirm save 
    this.setState({ confirmSave: true })
  }

  valueChange(id, key, e) {

    let { _data } = this.state
    let newValue = e.target.value

    //Update changed item
    let filteredItems = _data.filter(x => x[Object.keys(x)[0]] === id)
    if (filteredItems.length > 0) {

      //Update with changed value
      filteredItems[0][key.toString()] = newValue
      filteredItems[0].modifiedState = true

      //Update state
      this.setState({ _data })
    }
  }

  dependencySelect(key, value) {

    let selectedValue = 0
    let { _data, selectedItemId } = this.state
    let { dependencies } = this.props
    let depData = null

    //Get select data
    let depItem = dependencies.filter(d => d.key === key)[0]
    if (typeof depItem != 'undefined') {
      depData = depItem.value
    }

    //Convert value
    if (value !== null && depData !== null) {
      let dataItem = depData.filter(x => x[Object.keys(x)[1]] === value)[0]
      if (typeof dataItem !== 'undefined' && dataItem !== null) {
        selectedValue = parseInt(dataItem[Object.keys(dataItem)[0]])
      }
    }

    let idKey = Object.keys(_data[0])[0]
    let currentItem = _data.filter(x => x[idKey] == selectedItemId)[0]
    if (typeof currentItem !== 'undefined') {

      //Update with changed value
      currentItem[key] = selectedValue
      currentItem.modifiedState = true

      //Update state
      this.setState({ _data: _data })
    }
  }

  confirmSave() {

    let { dispatchToStore, dispatch, persist, setLoading, data, user, newItemTemplate } = this.props
    let { _data } = this.state

    //Update items
    setLoading(true)

    //Get changed items
    let postData = { Id: 123456 }
    let postDataItems = []
    _data.filter(item => item.modifiedState === true).forEach(item => {
      let clone = _.clone(item)

      //Remove keys not in the server model
      Object.keys(item).forEach(key => {
        if (!Object.keys(newItemTemplate).includes(key)) {
          delete clone[key]
        }
      })

      postDataItems.push(clone)
    })
    postData[persist] = postDataItems

    //Prep post params
    let url = apiBaseURL + "Lookups"

    //Handle error messages with error-config in order 
    //to get error message back and not just code
    o().config({
      error: (code, error) => {
        //Try to get & parse error message
        let errorJS = JSON.parse(error)
        let message = errorJS.value
        if (typeof message === 'undefined') message = errorJS.error.message
        if (typeof message === 'undefined') message = "(See log for error details)"

        //Log error message & details
        alert("Unable to save changes.\n\n" + message)
        console.error("Unable to save changes", code, errorJS)
      }
    })

    o(apiBaseURL + "Lookups")
      .post(postData)
      .save(
        (data) => {
          //Success
          o().config({ error: null }) //Reset error config

          //Toggle confirm save 
          this.setState({ confirmSave: false })

          //Merge changes into props
          let merged = _.merge(_data)
          let valueKey = Object.keys(merged[0]).includes("Value") ? "Value" : Object.keys(merged[0])[1].toString()
          merged = _.orderBy(merged, valueKey, 'asc'); // Use Lodash to sort array by 'Value'

          //Dispatch to store
          dispatchToStore(dispatch, merged)

          setLoading(false)

          //Close modal
          let { setEditList } = this.props
          setEditList({ show: false })
        },
        (status) => {
          //Failed
          o().config({ error: null }) //Reset error config
          setLoading(false)
        }
      )
  }

  cancelConfirm() {
    this.setState({ confirmSave: false })
  }

  cancel() {

    //Reset state
    this.setState({ selectedItemId: 0, confirmSave: false, _data: [] })

    //Close modal
    let { setEditList } = this.props
    setEditList({ show: false })
  }

  listItemClick(id, e) {
    this.setState({ selectedItemId: id })
  }

  processData(data) {

    let processedItems = []

    //Pre-process items
    data.map(item => {

      processedItems.push({
        id: item[Object.keys(item)[0]],
        value: Object.keys(item).includes("Value") ? item.Value : item[Object.keys(item)[1]],
        modifiedState: item.modifiedState
      })

    })

    return processedItems
  }

  renderSelectOptions(data, key) {

    let ar = []

    if (typeof data !== 'undefined') {

      let procData = this.processData(data)
      for (let i of procData) {
        <Option key={i.id} value={i.value}>
          {i.value}
        </Option>
      }
    }

    return ar
  }

  renderList() {

    let processedItems = this.processData(this.state._data)
    let listItems = []

    //Render standard list items
    processedItems.map(item => {
      if (item.id > 0) {
        let { selectedItemId } = this.state
        listItems.push(
          <ListGroupItem
            style={{
              cursor: "pointer",
              backgroundColor: (selectedItemId === item.id ? "#2BBBAD" : "white")
            }}
            hover={true}
            onClick={this.listItemClick.bind(this, item.id)}
            key={item.id}>
            {(item.modifiedState === true ? "* " : "") + item.value}
          </ListGroupItem>)
      }
    })

    return (<ListGroup>{listItems}</ListGroup>)
  }

  renderDetails() {

    let { type, dependencies } = this.props
    let { selectedItemId, _data } = this.state

    let filteredItems = _data.filter(x => x[Object.keys(x)[0]] === selectedItemId)
    let editDetails = []
    let detailElements = []

    //Fill "editDetail"
    if (filteredItems.length > 0) {
      let item = filteredItems[0]
      Object.keys(item).map(key => {
        editDetails.push({ id: selectedItemId, key: key, value: item[key] })
      })
    }

    //Render "editDetails"
    editDetails.filter(x => x.key !== 'modifiedState').map(item => {
      if (item.key !== editDetails[0].key) {

        //Fix nulls
        if (item.value === null) {
          item.value = ""
        }

        //Look for dependencies
        let deps = dependencies.filter(d => d.key === item.key)
        if (deps.length > 0) {

          //If dependency found - render select
          detailElements.push(<label key={_gf.GetUID()} style={{ fontSize: "smaller" }}>{item.key.toString()}</label>)

          //Convert value
          let displayValue = "Select..."
          if (item.value > 0) {
            let dataItem = depItem.value.filter(x => x[Object.keys(x)[0]] === item.value)[0]
            if (typeof dataItem !== 'undefined' && dataItem !== null && typeof dataItem[Object.keys(dataItem)[1]] !== 'undefined') {
              displayValue = dataItem[Object.keys(dataItem)[1]]
            }
          }

          detailElements.push(
            <Select
              key={item.id + "_" + item.key + "_select"}
              style={{ width: "100%", marginBottom: "25px" }}
              onChange={this.dependencySelect.bind(this, item.key)}
              value={displayValue}
            >
              {this.renderSelectOptions(depItem.value, item.key)}
            </Select>
          )
        }
        else {

          //If no dependency found - render input
          detailElements.push(
            <div key={item.id + "_" + item.key + "_input"} style={{ marginRight: "15px" }}>
              <label>{item.key.toString()}</label>
              <Input value={item.value.toString()}
                style={{ marginTop: "-25px", border: "1px solid lightgrey", borderRadius: "5px", padding: "5px" }}
                onChange={this.valueChange.bind(this, item.id, item.key)} />
            </div>
          )
        }
      }
    })

    return detailElements
  }

  render() {

    let { show } = this.props
    let { confirmSave, editDetails, _data } = this.state

    return (
      <>
        <Container>
          <Modal isOpen={show} toggle={this.cancel} size="fluid" style={{ width: "80%" }} >

            <ModalHeader toggle={this.cancel}>Edit list values</ModalHeader>

            <ModalBody height="80%">
              <div className="row">
                <div className="col-md-4" style={{ overflowY: "auto", height: "65vh" }}>
                  <h5 style={{ marginBottom: "15px", textDecoration: "underline" }}>Select item to edit:</h5>
                  {this.renderList()}
                </div>

                <div className="col-md-8" style={{ borderLeft: "solid 1px", overflowY: "auto", height: "65vh" }}>
                  <h5 style={{ marginBottom: "25px", textDecoration: "underline" }}>Edit item details:</h5>
                  {this.renderDetails()}
                </div>
              </div>
            </ModalBody>

            <ModalFooter>
              <div className="col-md-2" hidden={confirmSave}>
                <Button size="sm" color="default" onClick={this.add.bind(this)}>&nbsp;&nbsp;Add&nbsp;&nbsp;</Button>
              </div>

              <div className="col-md-10">
                <div hidden={confirmSave} style={{ float: "right" }}>
                  <Button size="sm" color="warning" onClick={this.save.bind(this)}>&nbsp;&nbsp;Save&nbsp;&nbsp;</Button>
                  <Button size="sm" color="secondary" onClick={this.cancel.bind(this)}>Cancel</Button>
                </div>
                <div hidden={!confirmSave} style={{ float: "right" }}>
                  <label>Please confirm to save changes?&nbsp;</label>
                  <Button size="sm" color="warning" onClick={this.confirmSave.bind(this)}>Confirm</Button>
                  <Button size="sm" color="secondary" onClick={this.cancelConfirm.bind(this)}>Cancel</Button>
                </div>
              </div>
            </ModalFooter>
          </Modal>
        </Container>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(EditListModal)