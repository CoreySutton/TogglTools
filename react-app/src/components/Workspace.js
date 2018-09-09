import React, { Component } from "react";

class Workspace extends Component {
  state = {};
  render() {
    return (
      <div className="form-check">
        <input
          className="form-check-input"
          type="radio"
          name={"workspace" + this.props.workspaceKey}
          id={"workspace" + this.props.workspaceKey}
        />
        <label
          className="form-check-label"
          htmlFor={"workspace" + this.props.workspaceKey}
        >
          {this.props.name}
        </label>
      </div>
    );
  }
}

export default Workspace;
