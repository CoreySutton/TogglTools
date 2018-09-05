import React, { Component } from 'react';

class Workspace extends Component {
    state = {}
    render() { 
        return (  
            <div class="form-check">
                <input class="form-check-input" type="radio" name={"workspace" + this.props.key} />
                <label class="form-check-label" for={"workspace" + this.props.key}>{this.props.name}</label>
            </div>
         );
    }
}
 
export default Workspace;