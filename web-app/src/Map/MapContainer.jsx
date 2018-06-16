import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Map, TileLayer } from 'react-leaflet'
import { updateCoordinates } from '../actions/coordinates';


import L from 'leaflet/dist/leaflet.js';
import 'leaflet/dist/leaflet.css';

import 'leaflet.pm/dist/leaflet.pm.min.js';
import 'leaflet.pm/dist/leaflet.pm.css';

import './Map.css';
import { LatLon } from '../reducers/coordinates';
// import  } from 'leaflet'



class MapContainer extends Component{

    

    constructor(props){
        super(props);

        let bounds =  [this.props.mapRect.topLeft.asCoord(), this.props.mapRect.botRight.asCoord()];
        let rect = L.rectangle(bounds, {color: "#2196f3", weight: 1})
        this.state = {
            rect: rect
        }

    }

    componentDidMount(){
        let map = L.map('map', {
            center: [42.373836, -71.251337],
            zoom: 15
        });
        // map.pm.addControls({
        //     visible:
        // });


        // optional options
        let editOptions = {
            // makes the layer draggable
            draggable: true,

            // makes the vertices snappable to other layers
            // temporarily disable snapping during drag by pressing ALT
            snappable: true,

            // distance in pixels that needs to be undercut to trigger snapping
            // default: 30
            snapDistance: 30,

            // self intersection allowed?
            allowSelfIntersection: true,

            // disable the removal of markers/vertexes via right click
            preventMarkerRemoval: true,

            // disable the possibility to edit vertexes
            preventVertexEdit: false,
        };
        this.state.rect.pm.enable(editOptions);
        L.tileLayer('http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}').addTo(map);
        
        this.state.rect.addTo(map);
        window.x = map.pm;
        map.pm.Toolbar.buttons.editMode._triggerClick();
        // map.pm.enableEdit('Poly', {})

        let fireUpdate = this.props.updateBox;
        this.state.rect.on('pm:edit', () => {
            let bounds = this.state.rect.getBounds();

            this.props.updateBox({
                top: bounds.getNorth(),
                left: bounds.getWest(),
                right: bounds.getEast(),
                bot: bounds.getSouth()
            })
        });

    }



    render(){
        let bounds =  [this.props.mapRect.topLeft.asCoord(), this.props.mapRect.botRight.asCoord()];
        this.state.rect.setBounds(bounds);
        if (this.state.rect.pm._enabled == true){
            let coords = this.state.rect.pm._findCorners();
            this.state.rect.pm._adjustAllMarkers(coords);
        }
        
        return (
            <div className="map-component">
                <div className="map-container">
                    <div id="map">
                        {/* LEAFLET POPULATES THIS */}
                    </div>
                </div>
            </div>
        )
    }


}

const mapStateToProps = ({mapState: {mapRect} }) => ({
    mapRect
});


const mapDispatchToProps = dispatch => ({
    updateBox: (coord) => {
      dispatch(updateCoordinates(coord));
    }
});


export default connect(mapStateToProps, mapDispatchToProps)(MapContainer);