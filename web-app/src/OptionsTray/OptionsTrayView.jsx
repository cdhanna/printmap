import React, { Component } from 'react';
import TextBox from '../TextBox/TextBoxComponent';
import './OptionsTray.css'

const OptionsTrayView = ({ mapRect, onChangeTopLeft, onChangeBotRight}) => {
  return (
    <div>
      <div>
        <h1> TopLeft {mapRect.topLeft.lat} / {mapRect.topLeft.lon} </h1>
        <TextBox value={mapRect.topLeft.lat} onChange={(next) => onChangeTopLeft({lat: next})}/>
        <TextBox value={mapRect.topLeft.lon} onChange={(next) => onChangeTopLeft({lon: next})}/>    
      </div>

      {/* TODO PULL THIS OUT INTO A COMPONENT CALLED "LATLONINPUT"  */}
      <div> 
        <h1> BotRight {mapRect.botRight.lat} / {mapRect.botRight.lon} </h1>
        <TextBox value={mapRect.botRight.lat} onChange={(next) => onChangeBotRight({lat: next})}/>
        <TextBox value={mapRect.botRight.lon} onChange={(next) => onChangeBotRight({lon: next})}/>    
      </div>
    </div>
  );
}

export default OptionsTrayView;