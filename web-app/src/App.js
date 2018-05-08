import React, { Component } from 'react';
import Map from './Map';
import OptionsTrayContainer from './OptionsTray/OptionsTrayContainer';
import './App.css';

class App extends Component {
  render() {
    return (
      <div>
        <OptionsTrayContainer />
        <Map />
      </div>
    );
  }
}

export default App;
