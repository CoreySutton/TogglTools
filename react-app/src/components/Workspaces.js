import React, { Component } from "react";
import Workspace from "./Workspace";

class Workspaces extends Component {
  state = {};
  render() {
    return (
      <div className="container border">
        <p>Workspaces</p>
        <Workspace workspaceKey="1" name="One" />
        <Workspace workspaceKey="2" name="Two" />
        <Workspace workspaceKey="3" name="Three" />
        <button className="btn btn-lg btn-primary btn-block" type="button">
          Create Report
        </button>
      </div>
    );
  }
}

export default Workspaces;
