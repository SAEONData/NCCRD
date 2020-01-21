import React from 'react'
import styled from 'styled-components'
import { useTable, usePagination } from 'react-table'
import { MultiSelect } from '@progress/kendo-react-dropdowns';

import customData from './ApiData_Experiment.json';
// import makeData from './makeData'

const Styles = styled.div`
  padding: 1rem;

  table {
    border-spacing: 0;
    border: 1px solid black;

    tr {
      :last-child {
        td {
          border-bottom: 0;
        }
      }
    }

    th,
    td {
      margin: 0;
      padding: 0.5rem;
      border-bottom: 1px solid black;
      border-right: 1px solid black;

      :last-child {
        border-right: 0;
      }

      input {
        font-size: 1rem;
        padding: 0;
        margin: 0;
        border: 0;
      }
    }
  }

  .pagination {
    padding: 0.5rem;
  }
  
  tfoot {
    tr:first-child {
      td {
        border-top: 2px solid black;
      }
    }
    tr:last-child {
      td {
        border: none !important;
        visible
      }
      border: none !important
    }
    font-weight: bolder;
  }
`

// Create an editable cell renderer
const EditableCell = ({
  cell: { value: initialValue },
  row: { index },
  column: { id },
  updateMyData, // This is a custom function that we supplied to our table instance
}) => {
  // We need to keep and update the state of the cell normally
  const [value, setValue] = React.useState(initialValue)

  const onChange = e => {
    setValue(e.target.value)
  }

  // We'll only update the external data when the input is blurred
  const onBlur = () => {
    updateMyData(index, id, value)
  }

  // If the initialValue is changed externall, sync it up with our state
  React.useEffect(() => {
    setValue(initialValue)
  }, [initialValue])

  return <input value={value} onChange={onChange} onBlur={onBlur} />
}

// Set our editable cell renderer as the default Cell renderer
const defaultColumn = {
  Cell: EditableCell,
}

// Be sure to pass our updateMyData and the disablePageResetOnDataChange option
function Table({ columns, data, updateMyData, disablePageResetOnDataChange, finalTable }) {
  // For this example, we're using pagination to illustrate how to stop
  // the current page from resetting when our data changes
  // Otherwise, nothing is different here.
  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    footerGroups,
    prepareRow,
    page,
    canPreviousPage,
    canNextPage,
    pageOptions,
    pageCount,
    gotoPage,
    nextPage,
    previousPage,
    setPageSize,
    state: { pageIndex, pageSize },
  } = useTable(
    {
      columns,
      data,
      defaultColumn,
      disablePageResetOnDataChange,
      // updateMyData isn't part of the API, but
      // anything we put into these options will
      // automatically be available on the instance.
      // That way we can call this function from our
      // cell renderer!
      updateMyData,
      finalTable
    },
    usePagination
  )

  // Render the UI for your table
  return (
    <>
      <table {...getTableProps()}>
        <thead>
          {headerGroups.map(headerGroup => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map(column => (
                <th {...column.getHeaderProps()}>{column.render('Header')}</th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody {...getTableBodyProps()}>
          {page.map(
            (row, i) => {
              prepareRow(row);
              return (
                <tr {...row.getRowProps()}>
                  {row.cells.map(cell => {
                    return (
                      <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                    )
                  })}
                </tr>
              )}
          )}
        </tbody>
          {
            finalTable ? 
              <tfoot>
              {footerGroups.map(group => (
                <tr {...group.getFooterGroupProps()}>              
                  {group.headers.map(column => (
                    <td {...column.getFooterProps()}>{column.render('Footer')}</td>
                  ))}
                </tr>
              ))}
            </tfoot>
            :
            <></>
          }
      </table>
      <div className="pagination">
        <button onClick={() => gotoPage(0)} disabled={!canPreviousPage}>
          {'<<'}
        </button>{' '}
        <button onClick={() => previousPage()} disabled={!canPreviousPage}>
          {'<'}
        </button>{' '}
        <button onClick={() => nextPage()} disabled={!canNextPage}>
          {'>'}
        </button>{' '}
        <button onClick={() => gotoPage(pageCount - 1)} disabled={!canNextPage}>
          {'>>'}
        </button>{' '}
        <span>
          Page{' '}
          <strong>
            {pageIndex + 1} of {pageOptions.length}
          </strong>{' '}
        </span>
        <span>
          | Go to page:{' '}
          <input
            type="number"
            defaultValue={pageIndex + 1}
            onChange={e => {
              const page = e.target.value ? Number(e.target.value) - 1 : 0
              gotoPage(page)
            }}
            style={{ width: '100px' }}
          />
        </span>{' '}
        <select
          value={pageSize}
          onChange={e => {
            setPageSize(Number(e.target.value))
          }}
        >
          {[10, 20, 30, 40, 50].map(pageSize => (
            <option key={pageSize} value={pageSize}>
              Show {pageSize}
            </option>
          ))}
        </select>
      </div>
    </>
  )
}

function EditableTableComponent() {
  const columns = React.useMemo(
    () => [
      {
        Header: 'Name',
        columns: [
          {
            Header: 'First Name',
            accessor: 'firstName',
          },
          {
            Header: 'Last Name',
            accessor: 'lastName',
          },
        ],
      },
      {
        Header: 'Info',
        columns: [
          {
            Header: 'Age',
            accessor: 'age',
          },
          {
            Header: 'Visits',
            accessor: 'visits',
          },
          {
            Header: 'Status',
            accessor: 'status',
          },
          {
            Header: 'Profile Progress',
            accessor: 'progress',
          },
        ],
      },
    ],
    []
  )

  // const [data, setData] = React.useState(() => makeData(20))
  const [originalData] = React.useState(data)
  const [skipPageReset, setSkipPageReset] = React.useState(false)

  // We need to keep the table from resetting the pageIndex when we
  // Update data. So we can keep track of that flag with a ref.

  // When our cell renderer calls updateMyData, we'll use
  // the rowIndex, columnID and new value to update the
  // original data
  const updateMyData = (rowIndex, columnID, value) => {
    // We also turn on the flag to not reset the page
    setSkipPageReset(true)
    setData(old =>
      old.map((row, index) => {
        if (index === rowIndex) {
          return {
            ...old[rowIndex],
            [columnID]: value,
          }
        }
        return row
      })
    )
  }

  // After data chagnes, we turn the flag back off
  // so that if data actually changes when we're not
  // editing it, the page is reset
  React.useEffect(() => {
    setSkipPageReset(false)
  }, [data])

  const SaveData = (data) => {
    alert(JSON.stringify(data));
  }

  // Let's add a data resetter/randomizer to help
  // illustrate that flow...
  const resetData = () => setData(originalData)

  const saveData = () => saveData(data)

  return (
    <Styles>
      <button onClick={resetData}>Reset Data</button><nbsp/><nbsp/><nbsp/><nbsp/><button onClick={saveData}>Save Data</button>
      <Table
        columns={columns}
        data={data}
        updateMyData={updateMyData}
        disablePageResetOnDataChange={skipPageReset}
      />
    </Styles>
  )
}

const EmissionsTableComponent = (props) => {
  const columns = React.useMemo(
    () => [
      {
        Header: 'Name',
        columns: [
          {
            Header: 'Year',
            accessor: 'year',
          },
          {
            Header: 'Chemical',
            accessor: 'chemical',
          },
          {
            Header: 'Tonnes per year',
            accessor: 'TPY'
          },
          {
            Header: 'Notes',
            accessor: 'notes'
          }
        ],
      }      
    ],
    []
  )

  // const finalColumns = React.useMemo(
  //   () => [
  //     {
  //       Header: 'Calculated',
  //       columns: [
  //         {
  //           Header: 'Emissions Input',
  //           accessor: 'EI',
  //         },
  //         {
  //           Header: 'Year',
  //           accessor: 'year',
  //         },
  //         {
  //           Header: 'Notes',
  //           accessor: 'notes'
  //         },
  //         {
  //           Header: 'CO2',
  //           accessor: 'co2'
  //         },
  //         {
  //           Header: 'CH4_CO2e (TAR 100)',
  //           accessor: 'ch4_co2e'
  //         },
  //         {
  //           Header: 'N2O_CO2e (TAR 100)',
  //           accessor: 'n2o_co2e'
  //         }
  //       ],
  //     }      
  //   ],
  //   []
  // )

  
  //const finalColumns = null;

  const [finalColumns, setFinalColumns] = React.useState();
  const [data, setData] = React.useState(() => props.data)//makeData('Emissions'))
  const [selectedEmissions] = React.useState(() => props.selectedEmissions)
  const [userLevel] = React.useState(() => props.userLevel)
  const [originalData] = React.useState(data)
  const [skipPageReset, setSkipPageReset] = React.useState(false)
  const [finalTable, setFinalTable] = React.useState(false);
  //const finalData = JSON.parse('[{"EI":"2009","year":"CO2","notes":8,"co2":"relationship"}]')
  const [finalData, setFinalData] = React.useState(); //React.useState(() => calculateFinalData());
  
  const [IPCCData] = React.useState(['SAR', 'TAR', 'AR4', 'AR5']);
  const [IPCCSelected, setIPCCSelected] = React.useState();
  
  const [ProjectedYears] = React.useState(['20', '100', '500']);
  const [SelectedProjectedYears, setSelectedProjectedYears] = React.useState();

  
  // We need to keep the table from resetting the pageIndex when we
  // Update data. So we can keep track of that flag with a ref.

  // When our cell renderer calls updateMyData, we'll use
  // the rowIndex, columnID and new value to update the
  // original data
  const updateMyData = (rowIndex, columnID, value) => {
    // We also turn on the flag to not reset the page
    setSkipPageReset(true)
    setData(old =>
      old.map((row, index) => {
        if (index === rowIndex) {
          return {
            ...old[rowIndex],
            [columnID]: value,
          }
        }
        return row
      })
    )
  }

  // After data chagnes, we turn the flag back off
  // so that if data actually changes when we're not
  // editing it, the page is reset
  React.useEffect(() => { setSkipPageReset(false) }, [data]) 

  // Let's add a data resetter/randomizer to help
  // illustrate that flow...
  // const resetData = () => setData(originalData)
 
  const locateMultiplier = (data, dynamicColumns) => {
    
    const calculatedData = [];

    //Consolited 'data' array by year and group the notes, tonnesPerYear and chemical for said year
    let consolidatedTPY = data.reduce((records, { year, TPY, chemical, notes }) => {
      if (!records[year]) records[year] = [];

      records[year].push(`{"year": "${year}", "chemical": "${chemical}", "TPY" : "${TPY}", "notes" : "${notes}"}`)

      return records
    }, {}); 
    
    consolidatedTPY = JSON.stringify(consolidatedTPY).replace(/"{/gi, '{')
    consolidatedTPY = consolidatedTPY.replace(/}"/gi, '}')
    consolidatedTPY = consolidatedTPY.replace(/\\/gi, '');
    consolidatedTPY = JSON.parse(consolidatedTPY)

    let endDataString = '[', dynamic = ''

    
    Object.keys(consolidatedTPY).forEach(function(year) {
      
      //const innerRecord = []
      let EI = '', notes = '', co2 = ''

      consolidatedTPY[year].forEach(element => {
        //console.log(element)
        if (EI != '') EI += ', '
        EI += element.chemical

        if (notes != '') notes += ', '
        notes += element.notes

        //co2 = customData[]

        dynamicColumns.forEach(e => {
            
          let multiplierValue = 0, total = 0

          // if(e.accessor === 'EI' || e.accessor === 'year' || e.accessor === 'notes' || e.accessor === 'co2') return;
          if(e.accessor === 'EI' || e.accessor === 'year' || e.accessor === 'notes') return;
          
          consolidatedTPY[year].forEach(r => {
            //find the report name for the chemical formla of e
            let reportName = ''
            customData.forEach(rec => {
              if(rec.Chemical_Formula === e.accessor) {
                reportName = rec.Report_Name
                multiplierValue = userLevel ? (IPCCSelected != null ? IPCCSelected : rec['TAR_100']) : rec['TAR_100']
              }
              return
            })

            //if(r.chemical === 'Nitrous oxide') console.log(`Nitrous oxide : ${JSON.stringify(consolidatedTPY[year])}`)

            if (r.chemical === reportName) {

              //alert(`Consolidated : ${JSON.stringify(consolidatedTPY[year])}`)
              //Consolidated : [{"year":"2018","chemical":"Methane","TPY":"2","notes":""},{"year":"2018","chemical":"Nitrous oxide","TPY":"2","notes":""}]

              if (r.TPY === undefined) r.TPY = 0

              // alert(`Chemical : ${r.chemical} | Total : ${total} | TPY : ${consolidatedTPY[year].TPY}`)
              // alert(`Consolidated : ${JSON.stringify(consolidatedTPY[year])}`)

              //if(r.chemical === 'Methane') alert(`Methane r.TPY : ${r.TPY} | Multiplier Value : ${multiplierValue} | Total : ${r.TPY * multiplierValue}/${parseInt(multiplierValue) * parseInt(r.TPY)}`)

              total = parseInt(multiplierValue) * parseInt(r.TPY)
            }
          })

          dynamic += `, "${e.accessor}" : "${ total }"`

          //console.log(`Dynamic : ${dynamic } | E.Accessor : ${e.accessor} | Total : ${total}`)

          //console.log(`consolidatedTPY[year] | ${JSON.stringify(consolidatedTPY[year])}`)
          //consolidatedTPY[year] | [{"year":"2018","chemical":"Methane","TPY":"","notes":""},{"year":"2018","chemical":"Nitrous oxide","TPY":"","notes":""}]

          //console.log(`E : ${e.accessor}`)
          // E : EI 
          // E : year
          // E : notes
          // E : co2 
          // E : CH4 
          // E : N2O

          //console.log(e)
          // Object { Header: "Emissions Input", accessor: "EI" }
          // Object { Header: "Year", accessor: "year" }
          // Object { Header: "Notes", accessor: "notes" }
          // Object { Header: "CO2", accessor: "co2" }
          // Object { Header: "CH4_CO2e (TAR100)", accessor: "CH4" }
          // Object { Header: "N2O_CO2e (TAR100)", accessor: "N2O" } 

        })

      });

      endDataString += `{ "EI":"${EI}", "year":"${year}", "notes":"${notes}", "co2":"${co2}", "CH4": "asd" ${ dynamic }},`
    })
    var pos = endDataString.lastIndexOf(',');
    endDataString = endDataString.substring(0,pos) + endDataString.substring(pos+1)
    endDataString += ']'

    console.log(endDataString)

    setFinalData(JSON.parse(endDataString))

     /* 
    setFinalData(JSON.parse(`
    [
      {
        "EI":"2009",
        "year":"CO2",
        "notes":8,
        "co2":"relationship",
        "CH4": "asd"
      },      
      {
        "EI":"2010",
        "year":"CO2",
        "notes":8,
        "co2":"relationship",
        "CH4": "asd"
      }
    ]`))
    */
  }

  const generateReport = () => {
 
    //Here we shall put the logic to calculate the final report 
    const dynamicColumns = []
    dynamicColumns.push(JSON.parse('{"Header": "Emissions Input","accessor": "EI"}'))
    dynamicColumns.push(JSON.parse('{"Header": "Year","accessor": "year"}'))
    dynamicColumns.push(JSON.parse('{"Header": "Notes","accessor": "notes"}'))
    //dynamicColumns.push(JSON.parse('{"Header": "CO2","accessor": "co2"}'))

    //Dynamically assign columns based on chosen emissions 
     selectedEmissions.forEach(e => {
      customData.forEach(a => {
        // if(e === "Carbon dioxide")
        //   return;

        if(e === a.Report_Name && !userLevel) {
          //Remember these are all "TAR100" and "TAR100" are for standard users, not admins
          dynamicColumns.push(JSON.parse(`{"Header": "${ a.Chemical_Formula + "_CO2e (TAR100)" }","accessor": "${ a.Chemical_Formula}"}`));
        }
        else if(e === a.Report_Name && userLevel) {
          //Here goes the logic to find which columns were specified by the admin
          if (IPCCSelected != null) {
            IPCCSelected.forEach(x => { SelectedYear(x, a, e, dynamicColumns) })
          } 
          else
            dynamicColumns.push(JSON.parse(`{"Header": "${ a.Chemical_Formula + "_CO2e (TAR100)" }", "accessor": "${ a.Chemical_Formula }"}`));
        }        
      })
    }); 

    setFinalColumns(JSON.parse(`[{"Header":"Calculated", "columns": ${JSON.stringify(dynamicColumns)}}]`));
    locateMultiplier(data, dynamicColumns)
    setFinalTable(true)
  }

  const SelectedYear = (IPCCSelected, customData, selectedEmission, dynamicColumns) => {

    if (SelectedProjectedYears != null) {
      //['20', '100', '500']
       SelectedProjectedYears.forEach(b => {
         customData.forEach(record => {
          //dynamicColumns.push(JSON.parse(`{"Header": "${ customData.Chemical_Formula + "_CO2e (TAR100)" }","accessor": "${ customData.Chemical_Formula}"}`));
          if (record.Report_Name === selectedEmission) {
            dynamicColumns.push(JSON.parse(`
              {
                "Header": "${ customData.Chemical_Formula + "_CO2e (" + IPCCSelected + ")" }", 
                "accessor": "${ customData.Chemical_Formula }", 
                "Footer": "${ info => 
                  {
                    const total = 'Testeeeeees'//React.useMemo(() => info.rows.reduce((sum, row) => row.values.ATkWh === null ? 0 : row.values.ATkWh + sum, 0), [info.rows])
                    
                    return <>{total}</>
                  } 
                }"}`));
            
            //dynamicColumns.push(JSON.parse(`{"Header": "${ customData.Chemical_Formula + "_CO2e (TAR100)" }","accessor": "${ customData.Chemical_Formula}"}`));
          }
         })        
      })

      
    }

    dynamicColumns.push(JSON.parse(`{"Header": "${ customData.Chemical_Formula + "_CO2e (TAR100)" }","accessor": "${ customData.Chemical_Formula}"}`));
  }

  const IPCCHandler = (event) => {
    setIPCCSelected([...event.target.value])
    console.log(IPCCSelected)
    generateReport();
  }

  const ProjectedYearsHandler = (event) => {
    setSelectedProjectedYears([ ...event.target.value ])
    console.log(SelectedProjectedYears)
    generateReport();
  }
  
  const Admin = () => {
    if (finalTable) {
      /////////////////////         ATTENTION!!!!!!!!!!!!!! the user level check is inverted here for test purposes
      return (!userLevel ? (
        <>
          <div className="example-wrapper">
              <div>
                  <div>Select an IPCC Report for CO2 Equivalents:  </div>
                  <MultiSelect data={IPCCData} onChange={IPCCHandler} value={IPCCSelected} />              
              </div>
          </div>
          <div className="example-wrapper">
              <div>
                  <div>Select a Projected Equivalence Year: </div>
                  <MultiSelect data={ProjectedYears} onChange={ProjectedYearsHandler} value={SelectedProjectedYears} />              
              </div>
          </div>
          <nbsp/><nbsp/><nbsp/><nbsp/><nbsp/><nbsp/><nbsp/><nbsp/>
        </>
      ) : <> </>)
    }
    else { 
      return (<></>)
    } 
  }  

  const TableButtons = () => {
    if (finalTable) {
      return (
        <> 
        <nbsp/><nbsp/><nbsp/><nbsp/>
        <button onClick={() => { alert(JSON.stringify(data)); }}>Update</button>
        </>
      );
    }
    else {
      return (
        <>
        <button onClick={() => { setData(originalData) }}>Reset Data</button>
        <nbsp/><nbsp/><nbsp/><nbsp/>
        <button onClick={() => { alert(JSON.stringify(data)); }}>Save Data</button>
        <nbsp/><nbsp/><nbsp/><nbsp/>
        <button onClick={() => { alert(JSON.stringify(data)); }}>Update</button>
        </>
      )
    }
  }

  const GenerateReport = () => {
    if (finalTable) {
      return (<> </>);
    }
    else {
      return (<button onClick={generateReport} >Generate Report</button>);
    }
  } 

  return (
    <Styles>
      
      <Admin />

      <TableButtons />
     
      <Table
        columns={ finalTable ? finalColumns : columns }
        data={ finalTable ? finalData : data }
        updateMyData={updateMyData}
        disablePageResetOnDataChange={skipPageReset}
      />
      <nbsp/><nbsp/><nbsp/><nbsp/>
      {/* <button onClick={generateReport} >Generate Report</button> */}
      <GenerateReport />
    </Styles>
  )
}

const EnergyTableComponent = (props) => {
  
  const columns = React.useMemo(
    () => [
      {
        Header: 'Energy Data',
        columns: [
          {
            Header: 'Year',
            accessor: 'year',
          },
          {
            Header: 'Renewable Types',
            accessor: 'renewable',
          },
          {
            Header: 'Notes',
            accessor: 'notes'
          },
          {
            Header: 'Annual Total kWh',
            accessor: 'ATkWh'
          },
          {
            Header: 'Annual Reduction in Electricity Purchased from the Grid (kWh)',
            accessor: 'ARP'
          }
        ],
      }      
    ],
    []
  )
  
  const finalColumns = React.useMemo(
    () => [
      {
        Header: 'Energy Data',
        columns: [
          {
            Header: 'Year',
            accessor: 'year',
            Footer: 'Total kWh over life of project',
          },
          {
            Header: 'Renewable Types',
            accessor: 'renewable',
          },
          {
            Header: 'Notes',
            accessor: 'notes'
          },
          {
            Header: 'Annual Total kWh',
            accessor: 'ATkWh',
            Footer: info => {
              // Only calculate total ATkWh if rows change
              
              const total = React.useMemo(
                () => info.rows.reduce((sum, row) => row.values.ATkWh === null ? 0 : row.values.ATkWh + sum, 0), [info.rows]
              )

              return <>{total}</>
            }
          },
          {
            Header: 'Annual Reduction in Electricity Purchased from the Grid (kWh)',
            accessor: 'ARP',
            Footer: info => {
              // Only calculate total ATkWh if rows change
              const total = React.useMemo(
                () =>
                  info.rows.reduce((sum, row) => row.values.ARP === null ? 0 : row.values.ARP + sum, 0),
                [info.rows]
              )

              return <>{total}</>
            }
          }
        ], 
      }      
    ],
    []
  )


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  //startDate={ this.state.startDate } endDate={ this.state.endDate } energyType={ this.state.selectedTypes } 
  //const [ startDate, endDate, energyType ] = props;

  //alert('Start Date : ' + props.startDate + ' | ' + 'End Date : ' + props.endDate + ' | ' + 'Energy Type : ' + props.energyType);
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  const [data, setData] = React.useState(() => props.data)
  const [originalData] = React.useState(data)
  const [skipPageReset, setSkipPageReset] = React.useState(false)
  const [finalTable, setFinalTable] = React.useState(false);
  //const finalData = JSON.parse('[{"EI":"2009","year":"CO2","notes":8,"co2":"relationship"}]')
  const [finalData, setFinalData] = React.useState(); //React.useState(() => calculateFinalData());

  // We need to keep the table from resetting the pageIndex when we
  // Update data. So we can keep track of that flag with a ref.

  // When our cell renderer calls updateMyData, we'll use
  // the rowIndex, columnID and new value to update the
  // original data
  const updateMyData = (rowIndex, columnID, value) => {

    // alert(`Row Index : ${rowIndex} | Column ID : ${columnID} | Value : ${value}`)
    // alert(data[rowIndex].renewable);

    // We also turn on the flag to not reset the page
    setSkipPageReset(true)
    setData(old =>
      old.map((row, index) => {
        if (index === rowIndex) {
          return {
            ...old[rowIndex],
            [columnID]: columnID === 'ARP' || columnID === 'ATkWh' ? parseInt(value) : value,
          }
        }
        return row
      })
    )

    if (columnID === 'ARP') {
      setData(old =>
        old.map((row, index) => {
          if (data[rowIndex].renewable === row.renewable) {
            return {
              ...old[index], 
              [columnID]: parseInt(value),
            }
          }
          return row
        })
      )
    }
    
    if (columnID === 'ATkWh') {
      setData(old =>
        old.map((row, index) => {
          if (data[rowIndex].renewable === row.renewable) {
            return {
              ...old[index], 
              [columnID]: parseInt(value),
            }
          }
          return row
        })
      )
    }
  }

  // After data chagnes, we turn the flag back off
  // so that if data actually changes when we're not
  // editing it, the page is reset
  React.useEffect(() => {
    setSkipPageReset(false)
  }, [data]) 

  // Let's add a data resetter/randomizer to help
  // illustrate that flow...
  const resetData = () => setData(originalData)

  const saveData = () => {
    // makeData('Energy', data)
  }

  const generateReport = () => {

    DataFinalTable();

    //This is a dummy record, delete once the functionality to calculate the final data is in place
    setFinalData(JSON.parse('[{"year":"2009","renewable":"CO2","notes":8,"ATkWh":"123213", "ARP": 967}]'))

    //Here we shall put the logic to calculate the final report 
    
    setFinalTable(true)
  }


  const DataFinalTable = () => {
    //alert(JSON.stringify(customData));

    alert(JSON.stringify(data))

  }

  const GenerateReport = () => {
    if (finalTable) {
      return (<> </>);
    }
    else {
      return (<button onClick={generateReport} >Generate Report</button>);
    }
  }

  return (
    <Styles>
      <button onClick={resetData}>Reset Data</button>
      &nbsp;&nbsp;&nbsp;&nbsp;
      <button onClick={saveData}>Save Data</button>
      <Table
        columns={finalTable ? finalColumns : columns}
        // data={ finalTable ? finalData : data}
        data={data}
        updateMyData={updateMyData}
        disablePageResetOnDataChange={skipPageReset}
        finalTable={finalTable}
      />
      &nbsp;&nbsp;&nbsp;&nbsp;
      <GenerateReport />
    </Styles>
  )
}

export { EditableTableComponent, EmissionsTableComponent, EnergyTableComponent }
