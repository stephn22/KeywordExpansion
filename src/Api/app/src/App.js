import React, { Component } from 'react';
import Dashboard from './components/Dashboard/Dashboard';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Dashboard />
        );
    }
}
