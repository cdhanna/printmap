import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { updateCoordinates } from '../actions/map';
import OptionsTrayView from './OptionsTrayView';

function OptionsTrayContainer({ topLeftLat, updateCoordinates }) {
  const setTopLeftLat = (lat) => {
    console.log(lat)
    updateCoordinates(lat);
  };
  return <OptionsTrayView topLeftLat={topLeftLat} onChangeCallback={setTopLeftLat} />;
}

const mapStateToProps = ({ mapState: { topLeftLat } }) => ({
  topLeftLat,
});

const mapDispatchToProps = dispatch => ({
  updateCoordinates: (nextLat) => {
    dispatch(updateCoordinates(nextLat));
  },
});

export default connect(mapStateToProps, mapDispatchToProps)(OptionsTrayContainer);
