import React, { Component } from 'react';

class Credentials extends Component {
    state = {
        username: "",
        apiKey: ""
    };
    render() {
        return (
            <div>
                <input class="form-control" type="email" name="email" placeholder="Email Address" required autofocus autocomplete/>
                <input class="form-control" type="text" name="apiKey" placeholder="Toggl Api Key" required autofocus autocomplete/>
                <button class="btn btn-lg btn-primary btn-block" type="submit">Next</button>
            </div>
        );
    }
}

export default Credentials;