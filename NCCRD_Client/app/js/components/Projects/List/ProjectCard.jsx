import React from 'react'
import { Card, CardBody, CardText, CardTitle, Button, Fa } from 'mdbreact'
import { connect } from 'react-redux'
import { DEAGreen } from "../../../config/colours.js"
import { notification } from 'antd'
import ReactTooltip from 'react-tooltip'

const _gf = require('../../../globalFunctions')

const mapStateToProps = (state, props) => {
  let user = state.oidc.user
  let { globalData: { showListViewOption, showFavoritesOption, showDetailsInParent } } = state
  return { showListViewOption, showFavoritesOption, showDetailsInParent, user }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setScrollPos: payload => {
      dispatch({ type: "SET_PROJECT_SCROLL", payload })
    },
    showInputWizard: payload => {
      dispatch({ type: "SET_SHOW_INPUT_WIZARD", payload })
    },
    setSelectedProjectId: payload => {
      dispatch({ type: "SET_SELECTED_PROJECT_ID", payload })
    }
  }
}

class ProjectCard extends React.Component {

  constructor(props) {
    super(props);

    this.togleFavorite = this.togleFavorite.bind(this)
    this.onClick = this.onClick.bind(this)

    this.state = {
      favorite: false
    }
  }

  componentDidMount() {
    let { pid } = this.props
    let favs = this.GetFavorites()
    this.setState({ favorite: favs.includes(pid) })
  }

  onClick() {

    if (this.props.showDetailsInParent) {
      let payload = {}
      payload.action = "showDetails"
      payload.context = "NCCRD"
      payload.value = this.props.pid
      window.parent.postMessage(payload, "*")
    }
    else {
      this.props.setScrollPos(document.getElementById("app-content").scrollTop)
      let navTo = ""
      if (location.hash.includes("projects")) {
        navTo = location.hash.replace("#/projects", "#/projects/" + this.props.pid)
      }
      else {
        navTo = location.hash.replace("#/", "#/projects/" + this.props.pid)
      }

      location.hash = navTo
    }
  }

  togleFavorite() {

    let { favorite } = this.state
    let { pid } = this.props

    let newState = !favorite
    this.setState({ favorite: newState })

    //Save to cookie
    let favs = this.GetFavorites()

    if (newState) {
      //Add if not contains
      if (!favs.includes(pid)) {
        favs.push(pid)
      }
    }
    else {
      //Remove if contains
      if (favs.includes(pid)) {
        let index = favs.indexOf(pid)
        favs.splice(index, 1)
      }
    }

    this.SetFavorites(favs)
  }

  GetFavorites() {
    let strFavs = _gf.ReadCookie("NCCRD_Project_Favorites")

    if (strFavs !== null && strFavs.length > 0) {
      let favs = strFavs.split(",")

      if (favs.length > 0) {
        return favs.map(x => parseInt(x))
      }
    }

    return []
  }

  SetFavorites(favs) {

    favs = favs.filter(f => !isNaN(f))

    let strFavs = ""
    if (favs.length > 0) {
      strFavs = favs.map(x => x.toString()).join(",")
    }
    else if (false.length === 1) {
      strFavs = favs[0].toString()
    }

    _gf.CreateCookie("NCCRD_Project_Favorites", strFavs, 3650)
  }

  render() {

    let { pdes, pid, user, projectDetails, plag } = this.props
    let { favorite } = this.state

    if (pdes.length > 240) {
      pdes = pdes.substring(0, 225) + " ..."
    }

    return (
      <>
        <CardBody>

          <CardTitle style={{ fontSize: 18, fontWeight: 400 }}>
            {this.props.ptitle}
          </CardTitle >

          <CardText style={{ wordWrap: "break-word", minHeight: 75, fontSize: 16 }}>
            {pdes}
          </CardText>

          {
            this.props.showListViewOption &&
            <Button
              size="sm"
              color="white"
              style={{
                marginLeft: "0px",
                boxShadow: "none",
                border: "1px solid silver",
                borderRadius: "5px",
                padding: "3px 15px 3px 15px",
                fontSize: "14px",
                height: 33
              }}
              onClick={this.onClick.bind(this)}
            >
              <Fa icon="eye" size="lg" style={{ color: DEAGreen, marginRight: "5px" }} />
              View
          </Button>
          }

          {
            this.props.showListViewOption &&
            <Button
              size="sm"
              color="white"
              style={{
                marginLeft: "0px",
                boxShadow: "none",
                border: "1px solid silver",
                borderRadius: "5px",
                padding: "3px 15px 3px 15px",
                fontSize: "14px",
                height: 33
              }}
              onClick={() => {
                if(!user || user.expired){
                  notification.warning({
                    message: 'Please login to edit projects.'
                  })
                }
                else{
                  let dispatch = () => new Promise((resolve, reject) => {
                    this.props.setSelectedProjectId(pid)
                    resolve()
                  })     
                  dispatch().then(() => {
                    this.props.showInputWizard(true)
                  })
                }
              }}
            >
              <Fa icon="pencil" size="lg" style={{ color: DEAGreen, marginRight: "5px" }} />
              Edit
          </Button>
          }

          {
            this.props.showFavoritesOption &&
            <Button
              size="sm"
              color="white"
              style={{
                marginLeft: "0px",
                boxShadow: "none",
                border: "1px solid silver",
                borderRadius: "5px",
                padding: "3px 15px 3px 15px",
                fontSize: "14px",
                height: 33
              }}
              onClick={this.togleFavorite}
            >
              <Fa icon="star" size="lg" style={{ color: favorite ? "#fdd835" : "#D8D8D8", marginRight: "5px" }} />
              Favourite
          </Button>
          }
          {/* {
            this.props.showListViewOption &&
            <Button
              size="sm"
              color="white"
              style={{
                marginLeft:"0px",
                boxShadow:"none",
                border:"1px solid silver",
                borderRadius:"5px",
                padding:"3px 15px 3px 15px",
                fontSize: "14px",
                height: 33
              }}
              onClick={console.log('delete button works')}
            >
            <Fa icon="trash" size="lg" style={{ color: DEAGreen, marginRight: "5px" }} />
              Delete
            </Button>
          } */}
        
           
            <Button
            data-tip data-for="dlTip"
              size="sm"
              color="white"
              style={{
                marginLeft:"0px",
                boxShadow:"none",
                border:"1px solid silver",
                borderRadius:"5px",
                padding:"3px 15px 3px 15px",
                fontSize: "14px",
                height: 33
              }}
              onClick={this.onClick.bind(projectDetails)}
            >
              
            <Fa icon="download" size="lg" style={{ color: DEAGreen, marginRight: "5px" }} />
            {/* <CSVLink
             
              filename={"project.csv"}
              data={[this.props.projectDetails]}
              asyncOnClick={true}
              onClick={() => {
                console.log(this.props.projectDetails)
              }}
            >            */}
              Download
            {/* </CSVLink> */}
            
           
            </Button>
            <ReactTooltip id="dlTip" place="top" effect="solid">
              To view the contents of this download, open the file in as an Excel spreadsheet in Microsoft Excel
            </ReactTooltip>
          
          
          

        </CardBody>
        <hr style={{ margin: "0px 0px 15px 0px" }} />
      </>
    )
  }
}

//export default ProjectCard
export default connect(mapStateToProps, mapDispatchToProps)(ProjectCard)