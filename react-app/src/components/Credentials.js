import React, { Component } from "react";

class Credentials extends Component {
  state = {
    username: "",
    apiKey: ""
  };
  render() {
    return (
      <div className="container border">
        <div className="form-group">
          <input
            className="form-control"
            type="email"
            name="email"
            placeholder="Email Address"
            required
            autoFocus
            autoComplete="on"
          />
          <input
            className="form-control"
            type="text"
            name="apiKey"
            placeholder="Toggl Api Key"
            required
            autoFocus
            autoComplete="on"
          />
          <button className="btn btn-lg btn-primary btn-block" type="button">
            Next
          </button>
        </div>
      </div>
    );
  }
}

export default Credentials;
