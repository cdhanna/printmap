import React, { Component } from 'react';
import TextBox from '../TextBox/TextBoxComponent';
import './OptionsTray.css'

const OptionsTrayView = ({ topLeftLat, onChangeCallback }) => {
  return (
    <div>
      <h1> {topLeftLat} </h1>
      <TextBox value={topLeftLat} onChange={onChangeCallback}/>
    </div>
  );
  //return <h1>My top left lat: {topLeftLat}</h1>;
}

export default OptionsTrayView;