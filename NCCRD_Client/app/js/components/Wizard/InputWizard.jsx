import React from 'react'
import { connect } from 'react-redux'
import { Drawer } from 'antd'
import SteppedInputForm from './SteppedInputForm.jsx';
import ProjectDataLoader from '../Projects/ProjectDataLoader.jsx';

const mapStateToProps = (state, props) => {
  return {}
}

const mapDispatchToProps = (dispatch) => {
  return {
    showInputWizard: payload => {
      dispatch({ type: "SET_SHOW_INPUT_WIZARD", payload })
    }
  }
}

class InputWizard extends React.Component {

  constructor(props) {
    super(props);

    this.onClose = this.onClose.bind(this)
    this.updateWindowDimensions = this.updateWindowDimensions.bind(this);

    this.state = {
      winInnerWidth: 0,
      winOuterWidth: 0,
      winInnerHeight: 0
    }
  }

  componentDidMount() {
    this.updateWindowDimensions();
    window.addEventListener('resize', this.updateWindowDimensions);
  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.updateWindowDimensions);
  }

  updateWindowDimensions() {

    this.setState({ winInnerWidth: window.innerWidth, winOuterWidth: window.outerWidth, winInnerHeight: window.innerHeight });
  }

  onClose() {
    this.props.showInputWizard(false)
  }

  render() {

    let { visible } = this.props
    let { winInnerWidth, winOuterWidth, winInnerHeight } = this.state

    return (
      <>
        <Drawer
          placement="right"
          closable={false}
          visible={visible}
          width={winInnerWidth < 1250 ? "100%" : "100%"}
        >
          <ProjectDataLoader>
            <SteppedInputForm onClose={this.onClose} visible={visible} />
          </ProjectDataLoader>
        </Drawer>
      </>
    )
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(InputWizard)