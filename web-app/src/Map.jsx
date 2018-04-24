import React, { Component } from 'react';
import ReactMapGL from 'react-map-gl';

export default class Map extends Component {
  constructor(props) {
    super(props);
    this.state = {
      width: 1000,
      height: 1000,
      latitude: 37.7577,
      longitude: -122.4376,
      zoom: 8,
    };
  }

  render() {
    return (
      <ReactMapGL
        width={this.state.width}
        height={this.state.height}
        latitude={this.state.latitude}
        longitude={this.state.longitude}
        zoom={this.state.zoom}
        mapboxApiAccessToken={process.env.REACT_APP_MAPBOX_ACCESS_TOKEN} 
        onViewportChange={(viewport) => {
          const {width, height, latitude, longitude, zoom} = viewport;
          this.setState({width, height, latitude, longitude, zoom});
        }}
      />
    );
  }
}
