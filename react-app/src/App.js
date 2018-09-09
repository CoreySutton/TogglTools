import React, { Component } from "react";
import Credentials from "./components/Credentials";
import Workspaces from "./components/Workspaces";

class App extends Component {
  render() {
    return (
      <React.Fragment>
        <Credentials />
        <Workspaces />
      </React.Fragment>
    );
  }
}

export default App;
