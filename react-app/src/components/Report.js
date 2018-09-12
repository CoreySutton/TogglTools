import React, { Component } from "react";
import Summary from "./Summary";
import Details from "./Details";

class Report extends Component {
  state = {};
  render() {
    return (
      <div className="container">
        <Summary />
        <Details />
      </div>
    );
  }
}

export default Report;
