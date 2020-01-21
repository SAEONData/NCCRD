import React from 'react';
import { Calculator, CheckUserLevel, GenerateTable } from './Calculator/HelperFunctions.jsx';
import { Dropdown, Button, Label } from './Calculator/Controls.jsx';
import { Row } from 'mdbreact'
import { MultiSelect } from '@progress/kendo-react-dropdowns';
import { EnergyTableComponent, EmissionsTableComponent} from './Calculator/EditableTableComponent.jsx';
import DatePicker from "react-datepicker";

import "react-datepicker/dist/react-datepicker.css";
import "./Calculator/shared.css"
import "./Calculator/kendo.css"

const mapStateToProps = (state, props) => {
  let { projectData: { projectDetails } } = state
  let { lookupData: { projectStatus } } = state
  return { projectStatus, projectDetails }
}

const mapDispatchToProps = (dispatch) => {
  return {}
}

class EmissionsCalculatorStep extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
          enabled: false,
          calcType: "Energy"
      };
    }

    checkState() {
      if (this.state.calcType == "Energy")
        return <EnergyData/>;
      else if (this.state.calcType == "Emissions")
        return <EmissionsData/>;
    }

    render() {
        return (
          <>
            <div className="vertical-spacer"/>

            <Row>
              <h4>Please report annual project data below :</h4>
              <p> 1. Annual quantity of electricity generated from renewable sources (kWh)</p>
              <p> 2. Annual Emissions Reductions (CO2e)</p>
            </Row>

            <div className="vertical-spacer"/>

            <Row>
                <button onClick={ () => {this.setState({ calcType: "Energy" })} }>
                  If You Have Energy Data
                </button>
            </Row>
    
            <div className="vertical-spacer" />
                
            <Row>
                <button onClick={ () => {this.setState({ calcType: "Emissions" })}}>
                  If You Have Emissions Data
                </button>
            </Row>

            <div className="vertical-spacer" />
            {
              this.checkState()
            }
          </>
        )
      }
}

class EmissionsData extends React.Component {
    
  constructor(props) {
      super(props)
      this.state = {
        table : false,
        startDate: new Date(),
        endDate: new Date(),
        source: ['Combustion', 'Process', 'Fugitive emissions', 'Agriculture, Forestry and Fisheries', 'Waste'],
        selectedSource: '',
        emssionsSource: ['Carbon dioxide', 'Methane', 'Nitrous oxide', 'Sulfur hexafluoride', 'HFC-152a', 'HFC-32', 'HFC-125', 'HFC-143a', 'HFC-23', 'PFC-14', 'PFC-218', 'PFC-31-10', 'PFC-41-12', 'PFC-51-14', 'PFC-318', 'PFC-116'],
        selectedEmissions: [],
        txtMethodUsed: '',
        chkCanProvideData: true
      }
      this.renderHandler = this.renderHandler.bind(this);
      this.Simple = this.Simple.bind(this);
      this.Advanced = this.Advanced.bind(this);
      this.Emissions = this.Emissions.bind(this);
      // this.componentWillUpdate = this.componentDidUpdate.bind(this);
  }    

  handleChange = range => {
    this.setState({
      startDate: range.startDate.year().toString(),
      endDate: range.endDate.year().toString()
    });
  };
   
  handleStartDate = date => { this.setState({ startDate: date }); }

  handleEndDate = date => { this.setState({ endDate: date }); }

  renderHandler() {
    return(CheckUserLevel() ? <this.Advanced /> : <this.Simple />)
  }

  setTypes = (event) => {
    this.setState({
      selectedEmissions: [ ...event.target.value ]
    });
  }
  
  setSource = (event) => {
    this.setState({
      selectedSource : [ ...event.target.value ]
    });
  }

  generateTableData() {

    const records = [];
    this.state.selectedEmissions.forEach(element => {
      for (let i = 0; i < ((this.state.endDate.getFullYear() - this.state.startDate.getFullYear()) + 1); i++) {
        records.push({
          year: (parseInt(this.state.startDate.getFullYear()) + i),
          chemical: element,
          TPY: '',
          notes: ''
        });
      }  
    });
 
    this.setState({ data: records})
    // var asdasd = JSON.parse(
    //   `[
    //     {
    //       "year":"NewData",
    //       "chemical":"CO2",
    //       "TPY":8,
    //       "notes":"relationship"
    //     }
    //   ]`);
    // this.setState({data: asdasd})
    
    //alert(`Records : ${JSON.stringify(records)}`)

    //alert(this.state.selectedEmissions);

    if (this.state.startDate != null && this.state.endDate != null) {
      this.setState({ table : true });
    }
  }

  //#region Form section
 
  Simple (){
    return ( !this.state.table ? 
              <div>
                <div className="vertical-spacer" />

                {/* <Row>
                  <Dropdown id="ddlEmissionsSource" type="textbox" list={this.state.source} headerTitle="Source : " onChange={event => this.setState({[event.target.id]: event.target.value})} />
                </Row> */}
                
                <Row>
                  <div className="example-wrapper">
                      <div>
                          <div>Can you estimate or do you record project emissions? : </div>
                          {/* <input data={this.state.source}  onChange={event => this.setState({selectedSource: [ ...event.target.value ]})} value={this.state.selectedSource} /> */}
                          <input type="checkbox" checked={this.state.chkCanProvideData} onChange={event => this.setState({chkCanProvideData: !this.state.chkCanProvideData})} />
                      </div>
                  </div>
                </Row>

                <this.Emissions />
                
              </div>
              :
              <EmissionsTableComponent data={this.state.data} selectedEmissions={this.state.selectedEmissions} userLevel={CheckUserLevel()}/>
            ); 
  }

  Emissions(){
    if (this.state.chkCanProvideData) {
      return (
        <>
        <div className="vertical-spacer" />
                <Row>
                  <div className="example-wrapper">
                      <div>
                          <div>Source : </div>
                          <MultiSelect data={this.state.source}  onChange={event => this.setState({selectedSource: [ ...event.target.value ]})} value={this.state.selectedSource} />
                      </div>
                  </div>
                </Row>

                <div className="vertical-spacer" />
                <Row>
                  <div className="example-wrapper">
                      <div>
                          <div>Applicable Emissions : </div>
                          <MultiSelect data={this.state.emssionsSource}  onChange={event => this.setState({selectedEmissions: [ ...event.target.value ]})} value={this.state.selectedEmissions} />
                      </div>
                  </div>
                </Row>

                {/* <div className="vertical-spacer" />
                <Row>
                  <DateRange id="dpProjectLifetime" onInit={this.handleChange} onChange={this.handleChange} />
                </Row>  */}

                <div className="vertical-spacer" />
                <Row>
                  <DatePicker id="dpProjectStart" peekNextMonth selected={new Date()}  selected={this.state.startDate} onChange={this.handleStartDate} showMonthDropdown showYearDropdown dropdownMode="select" />
                  <DatePicker id="dpProjectEnd" peekNextMonth selected={new Date()} selected={this.state.endDate} onChange={this.handleEndDate} showMonthDropdown showYearDropdown dropdownMode="select" />
                </Row>

                <div className="vertical-spacer" />
                <Row>
                  <div className="example-wrapper">
                    <div>
                      <div>Describe the method used to estimate the emissions reduction : </div>
                      <input type="text" value={ this.state.txtMethodUsed } onChange={ event => this.setState({txtMethodUsed: [ event.target.value ]})}/>
                    </div>
                  </div>
                </Row>

                <div className="vertical-spacer" />
                <Row>
                  <Button btnText={"Generate Table"} onClick={ () => { this.generateTableData() } } />
                </Row>
        </>
      )
    }
    else {
      return (<> </>)
    }
  }

  Advanced (){
      
      let testVar = Calculator(this.props)

      return (<div>
          Emissions Advance Form
          { testVar }
      </div>)
  }

  //#endregion

  render() { return (this.renderHandler(CheckUserLevel())); }
}

class EnergyData extends React.Component {
  
  constructor(props) {
      super(props)
      this.state = {
        table : false,
        startDate: new Date(),
        endDate: new Date(),
        source: ['Bio/Waste Gas', 'Geothermal', 'Hydro', 'Solar', 'Tidal', 'Wind'],
        selectedTypes: []
      }
      this.handleSubmit = this.handleSubmit.bind(this);
      
      this.Simple = this.Simple.bind(this);
      this.Advanced = this.Advanced.bind(this);
  }
  
  handleChange = range => {
    this.setState({
      startDate: range.startDate.year().toString(),
      endDate: range.endDate.year().toString()
    });
  };

  handleStartDate = date => {
    this.setState({
      startDate: date
    });
  }

  handleEndDate = date => {
    this.setState({
      endDate: date
    });
  }

  setTypes = (event) => {
    this.setState({
      selectedTypes: [ ...event.target.value ]
    });
  }

  renderHandler() {
      return(CheckUserLevel() ? <this.Advanced /> : <this.Simple />)
  }

  handleSubmit() { 
      alert(this.state.selectedTypes.toString())
  }
  
  calculate() { 

    if (this.state.startDate.getFullYear() != null && this.state.endDate.getFullYear() != null) {
      this.setState({ table : true });
    }
    
    const records = [];
    for (let i = 0; i <= (this.state.endDate.getFullYear() - this.state.startDate.getFullYear()); i++) {
      records.push({
        year: (parseInt(this.state.startDate.getFullYear()) + i),
        renewable: this.state.selectedTypes.toString().replace(',', ', '),
        notes: '',
        ATkWh: null,
        ARP: null
      });
    }

    this.setState({ data: records})
    alert(JSON.stringify(records))
  }

  //#region Form section

  Simple (){
    return ( !this.state.table ? 
              <div>
                <div className="vertical-spacer" />

                <Row>
                  <div className="example-wrapper">
                      <div>
                          <div>Renewable Types :</div>
                          <MultiSelect data={this.state.source} onChange={this.setTypes} value={this.state.selectedTypes} />
                      </div>
                  </div>
                </Row>

                <div className="vertical-spacer" />
                <Row>
                  <DatePicker id="dpProjectStart" peekNextMonth selected={new Date()} selected={this.state.startDate} onChange={this.handleStartDate} showMonthDropdown showYearDropdown dropdownMode="select" />
                  <DatePicker id="dpProjectEnd" peekNextMonth selected={new Date()} selected={this.state.endDate} onChange={this.handleEndDate} showMonthDropdown showYearDropdown dropdownMode="select" />
                </Row>

                <div className="vertical-spacer" />
                <Row>
                  <Button btnText={"Generate Table"} onClick={ () => { this.calculate() } } />
                </Row>
              </div>
              :
              <EnergyTableComponent data={this.state.data} userLevel={CheckUserLevel()} />
            );
  }

  Advanced (){
      const styleVar = {  position: 'absolute',
                          width: '300px',
                          height: '200px',
                          zindex: '15',
                          top: '50%',
                          left: '50%',
                          margin: '-100px 0 0 -150px',
                          background: 'none'
                       }

      return (<div style={ styleVar }>
                {/* <Date type="yearRange"></Date> Year Range */}
                <Dropdown type="textbox" list={this.state.source} headerTitle="Year"></Dropdown> {/* <--- Date range, 20000 - 2060 Needs to expand into a dropdown with 10 textbox's for the year range, when the first is filled the remainders should update with the same result  */}
                <Button btnText="Tonnes" disabled/>
                <Label title="Select source" headerTitle={"Gas Type"}  list={this.state.source} toggleItem={this.toggleSelected} />
                <Button btnText={"Submit"} onClick={ this.handleSubmit }/>
              </div>)
  }

  //#endregion

  render() { return (this.renderHandler(CheckUserLevel())); }
}

export default EmissionsCalculatorStep;