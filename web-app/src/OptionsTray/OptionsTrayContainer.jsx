import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { updateCoordinates } from '../actions/coordinates';
import OptionsTrayView from './OptionsTrayView';
import { MapRect } from '../reducers/coordinates';

function OptionsTrayContainer({ mapRect, updateTopLeft, updateBotRight, updateBox }) {

  const onChange = (change) => {
    updateBox(change);
  };

  return (
    <div>
      <OptionsTrayView mapRect={mapRect} onChange={onChange} />
    </div>
  )
  
}

const mapStateToProps = ({ mapState: { mapRect } }) => ({
  mapRect
});

const mapDispatchToProps = dispatch => ({
  updateBox: (coord) => {
    dispatch(updateCoordinates(coord));
  }
});

export default connect(mapStateToProps, mapDispatchToProps)(OptionsTrayContainer);
