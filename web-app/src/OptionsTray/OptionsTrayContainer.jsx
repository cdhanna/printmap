import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { updateTopLeft, updateBotRight } from '../actions/map';
import OptionsTrayView from './OptionsTrayView';
import { MapRect } from '../reducers/map';

function OptionsTrayContainer({ mapRect, updateTopLeft, updateBotRight }) {

  const onChangeTopLeft = (change) => {
    updateTopLeft(change);
  };
  const onChangeBotRight = (change) => {
    updateBotRight(change);
  };

  return (
    <div>
      <OptionsTrayView mapRect={mapRect} onChangeTopLeft={onChangeTopLeft} onChangeBotRight={onChangeBotRight} />
    </div>
  )
  
}

const mapStateToProps = ({ mapState: { mapRect } }) => ({
  mapRect
});

const mapDispatchToProps = dispatch => ({
  updateTopLeft: (coord) => {
    dispatch(updateTopLeft(coord));
  },
  updateBotRight: (coord) => {
    dispatch(updateBotRight(coord));
  }
});

export default connect(mapStateToProps, mapDispatchToProps)(OptionsTrayContainer);
