import React, { Component } from "react";
import Credentials from "./components/Credentials";
import Workspaces from "./components/Workspaces";
import SummaryReport from "./components/SummaryReport";
import DetailedReport from "./components/DetailedReport";

class App extends Component {
  render() {
    return (
      <React.Fragment>
        <Credentials />
        <Workspaces />
        <SummaryReport />
        <DetailedReport />
      </React.Fragment>
    );
  }
}

export default App;
