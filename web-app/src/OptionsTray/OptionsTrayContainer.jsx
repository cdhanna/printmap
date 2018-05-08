import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { updateCoordinates } from '../actions/map';
import OptionsTrayView from './OptionsTrayView';

function OptionsTrayContainer({ topLeftLat, updateCoordinates }) {
  return <OptionsTrayView topLeftLat={topLeftLat} onChangeCallback={(value) => updateCoordinates(value)} />;
}

const setTopLeftLat = (arg) => {
  console.log(arg.target.value)

};

const mapStateToProps = ({ mapState: { topLeftLat } }) => ({
  topLeftLat,
});

const mapDispatchToProps = dispatch => ({
  updateCoordinates: () => {
    dispatch(updateCoordinates());
  },
});

export default connect(mapStateToProps, mapDispatchToProps)(OptionsTrayContainer);
