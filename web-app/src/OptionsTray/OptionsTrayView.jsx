import React, { Component } from 'react';
import TextBox from '../TextBox/TextBoxComponent';
import './OptionsTray.css'

const OptionsTrayView = ({ mapRect, onChange}) => {
  return (
    <div className="options-tray">

      <div className="step">
        <div className="stepLabel">
          Step 1
        </div>
        <div className="stepName">
          Pick An Area
        </div>
      </div>
      <div>
        {/* <h1> TopLeft {mapRect.topLeft.lat} / {mapRect.topLeft.lon} </h1> */}
        <TextBox value={mapRect.topLeft.lat} onChange={(next) => onChange({top: Number(next), left: mapRect.topLeft.lon, bot: mapRect.botRight.lat, right: mapRect.botRight.lon})}/>
        <TextBox value={mapRect.topLeft.lon} onChange={(next) => onChange({top: mapRect.topLeft.lat, left: Number(next), bot: mapRect.botRight.lat, right: mapRect.botRight.lon})}/>
      </div>

      <div> 
        {/* <h1> BotRight {mapRect.botRight.lat} / {mapRect.botRight.lon} </h1> */}
        <TextBox value={mapRect.botRight.lat} onChange={(next) => onChange({top: mapRect.topLeft.lat, left: mapRect.topLeft.lon, bot: Number(next), right: mapRect.botRight.lon})}/>
        <TextBox value={mapRect.botRight.lon} onChange={(next) => onChange({top: mapRect.topLeft.lat, left: mapRect.topLeft.lon, bot: mapRect.botRight.lat, right: Number(next)})}/>
        
      </div>


      
     <div className="step">
        <div className="stepLabel">
          Step 2
        </div>
        <div className="stepName">
          Adjust Settings
        </div>
      </div>


      
     <div className="step">
        <div className="stepLabel">
          ... and
        </div>
        <div className="stepName">
          Submit!
        </div>
      </div>
    </div>

  );
}

export default OptionsTrayView;