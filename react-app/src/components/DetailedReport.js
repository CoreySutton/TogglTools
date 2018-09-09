import React, { Component } from "react";

class DetailedReport extends Component {
  state = {};
  render() {
    return (
      <div className="container">
        <h2>Detailed Report</h2>
        <table className="table">
          <tr>
            <th scope="col">Project</th>
            <th scope="col">Mon 01/01</th>
            <th scope="col">Tue 01/01</th>
            <th scope="col">Wed 01/01</th>
            <th scope="col">Thu 01/01</th>
            <th scope="col">Fri 01/01</th>
            <th scope="col">Sat 01/01</th>
            <th scope="col">Sun 01/01</th>
          </tr>
          <tr>
            <th scope="row">Project 1</th>
            <td>
              <p className="report-task">[#1] Added base project</p>
              <p className="report-task">[#2] Updated wiki</p>
              <p className="report-task">[#3] Stand-up</p>
            </td>
            <td />
            <td />
            <td />
            <td />
            <td />
            <td />
          </tr>
          <tr>
            <th scope="row">Project 2</th>
            <td />
            <td />
            <td>3.25</td>
            <td />
            <td>8.00</td>
            <td />
            <td />
          </tr>
          <tr>
            <th scope="row">Project 3</th>
            <td>2.5</td>
            <td />
            <td />
            <td>1.75</td>
            <td />
            <td />
            <td>1.00</td>
          </tr>
        </table>
      </div>
    );
  }
}

export default DetailedReport;
