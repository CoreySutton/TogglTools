import React, { Component } from "react";
import Credentials from "./components/Credentials";
import Workspaces from "./components/Workspaces";
import Report from "./components/Report";

class App extends Component {
  render() {
    return (
      <React.Fragment>
        <Credentials />
        <Workspaces />
        <Report />
      </React.Fragment>
    );
  }
}

export default App;
