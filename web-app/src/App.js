import React, { Component } from 'react';
import OptionsTrayContainer from './OptionsTray/OptionsTrayContainer';
import MapContainer from './Map/MapContainer';
import './App.css';


class App extends Component {

  

  render() {
    return (
      <div className="app">
        <div className="title-view">
            
          <h1> 
            <div> 
              Welcome to the fabulous 
            </div>
            <div>
              Topo Map Generator 
            </div>
          </h1>

          <button>
            Build your Map
          </button>
          
        </div>
        <div className="map-view">
          <OptionsTrayContainer />
          <MapContainer/>
        </div>
      </div>
    );
  }
}

export default App;
