import React, { Component } from 'react';
import Credentials from './components/Credentials';
import Workspaces from './components/Workspaces';

class App extends Component {
  render() {
    return (
      <div>
        <Credentials />
        <Workspaces />
      </div>
    );
  }
}

export default App;
