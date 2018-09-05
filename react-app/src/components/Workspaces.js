import React, { Component } from 'react';
import Workspace from './Workspace';

class Workspaces extends Component {
    state = {  }
    render() { 
        return ( 
            <div>
                <Workspace key="1" name="One" />
                <Workspace key="2" name="Two" />
                <Workspace key="3" name="Three" />
                <button class="btn btn-lg btn-primary btn-block" type="submit">Create Report</button>
            </div>
        );
    }
}
 
export default Workspaces;