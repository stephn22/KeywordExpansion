import { Component } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import MainContainer from './components/containers/MainContainer';
import SuggestContainer from './components/containers/SuggestContainer';
import RealTimeTrendsContainer from './components/containers/RealtimeTrendsContainer';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <BrowserRouter>
                <Routes>
                    <Route path='/main' element={<MainContainer />} />
                    <Route path='/realtime-trends' element={<RealTimeTrendsContainer />} />
                    <Route path='/suggest' element={<SuggestContainer />} />
                </Routes>
            </BrowserRouter>
        );
    }
}
