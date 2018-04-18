import React, { Component } from 'react';
import ReactMapGL from 'react-map-gl';

export default class Map extends Component {
  render() {
    let viewport = {
      width: 1000,
      height: 1000,
      latitude: 77.7577,
      longitude: -100.4376,
      zoom: 8,
      style: 'style'
    };

    return (
      <ReactMapGL
        {...viewport}
        mapboxApiAccessToken={process.env.REACT_APP_MAPBOX_ACCESS_TOKEN}
        onViewportChange={(viewport) => this.setState({viewport})}
      />
    );
  }
}