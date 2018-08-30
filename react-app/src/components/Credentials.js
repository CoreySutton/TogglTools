import React, { Component } from 'react';

class Credentials extends Component {
    state = {
        username: "",
        apiKey: ""
    };
    render() {
        return (
            <div>
                <div class="form-label-group">
                    <input class="form-control" type="email" name="email" placeholder="Email Address" required autofocus autocomplete/>
                </div>
                <div>
                <input class="form-control" type="text" name="apiKey" placeholder="Toggl Api Key" required autofocus autocomplete/>
                </div>
                <button class="btn btn-lg btn-primary btn-block" type="submit">Next</button>
            </div>
        );
    }
}

export default Credentials;