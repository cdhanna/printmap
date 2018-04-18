import React, { Component } from 'react';
import ReactMapGL, {Marker} from 'react-map-gl';

export default class Map extends Component {
  render() {
    return (
      <ReactMapGL
        width={400}
        height={400}
        latitude={37.7577}
        longitude={-122.4376}
        zoom={8}
        mapboxApiAccessToken={process.env.REACT_APP_MAPBOX_ACCESS_TOKEN} 
        onViewportChange={(viewport) => {
          const {width, height, latitude, longitude, zoom} = viewport;
          // call `setState` and use the state to update the map.
        }}
      />
    );
  }
}