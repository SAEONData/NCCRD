import React from 'react'
import { Row, Col, Button } from 'mdbreact'
import { connect } from 'react-redux'
import popout from '../../../images/popout.png'

const mapStateToProps = (state, props) => {
  return {}
}

const mapDispatchToProps = (dispatch) => {
  return {}
}

class DashMapPreview extends React.Component {

  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div style={{ backgroundColor: "white", padding: "10px", borderRadius: "10px", border: "1px solid gainsboro" }}>

        <h4 style={{ margin: "5px 5px 0px 19px", display: "inline-block" }}>
          <b>Map</b>
        </h4>

        <img
          src={popout}
          style={{
            width: "25px",
            float: "right",
            margin: "5px 5px 0px 0px",
            cursor: "pointer"
          }}
          onClick={() => { location.hash = "/map" }}
        />

        <hr />

        {/* temp */}
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        {/* temp */}

      </div>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(DashMapPreview)