'use strict'

import React from 'react'
import TextareaAutosize from "react-textarea-autosize"
import { connect } from 'react-redux'
import { UILookup } from "../../constants/ui_config.js"
import { Input } from "mdbreact"

const mapStateToProps = (state, props) => {
    let { globalData: { editMode } } = state
    return { editMode }
}

const mapDispatchToProps = (dispatch) => {
    return {
        setValue: (key, payload) => {
            dispatch({ type: key, payload })
        }
    }
}

class TextAreaComponent extends React.Component {

    constructor(props) {
        super(props);
    }

    fixNullOrUndefinedValue(value) {

        if (typeof value === 'undefined' || value === null) {
            value = ""
        }

        return value
    }

    getFontColour() {
        if (this.props.editMode) {
            return "#2BBBAD"
        }
        else {
            return "black"
        }
    }

    valueChange(event) {

        let { setValue, setValueKey, parentId, editMode } = this.props

        if (typeof setValueKey !== 'undefined') {
            setValue(setValueKey, { value: event.target.value, id: parentId, state: editMode === true ? "modified" : "original" })
        }
    }

    getLabelFontColour(uiconf) {
        if (typeof uiconf.required != 'undefined' && uiconf.required === true) {
            return "red"
        }
        else {
            return "black"
        }
    }

    render() {

        let { col, label, editMode, id, value } = this.props
        value = this.fixNullOrUndefinedValue(value)

        let uiconf = UILookup(id, label)

        return (
            <div className={col}>
                <label data-tip={uiconf.tooltip} style={{ fontWeight: "bold", color: this.getLabelFontColour(uiconf) }}>{uiconf.label}</label>
                <TextareaAutosize
                    readOnly={!editMode}
                    style={{
                        //borderStyle: "solid",
                        //borderWidth: "0px 0px 1px 0px",
                        //borderColor: "#b4b4b4",
                        //paddingBottom: "4px",
                        border: "1px solid lightgrey",
                        borderRadius: "5px",
                        padding: "3px 5px 3px 5px",
                        color: this.getFontColour(),
                        width: "100%"
                    }}
                    value={value}
                    onChange={this.valueChange.bind(this)}
                />
            </div>
        )

    }
}

export default connect(mapStateToProps, mapDispatchToProps)(TextAreaComponent)