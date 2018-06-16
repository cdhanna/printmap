import queryString from 'query-string';
import { CHANGE_BOX_TOPLEFT, CHANGE_BOX_BOTRIGHT, CHANGE_BOX } from '../constants/actionTypes';


export class LatLon {
  constructor(lat, lon){
    this.lat = lat,
    this.lon = lon;
  }
  asCoord(){
    return [this.lat, this.lon];
  }
}
export class MapRect {
  constructor(topLeft, botRight){
    this.topLeft = topLeft;
    this.botRight = botRight; 
  }
}

export const initialState = {
  mapRect: new MapRect( new LatLon(42.373836, -71.251337), new LatLon(42.369187, -71.244369))
};

export default function coordinates(state = initialState, action) {
  switch (action.type) {
    case CHANGE_BOX:
      let next = {
        ...state,
        mapRect: {
          topLeft: action.topLeft,
          botRight: action.botRight
        }
      }
      return next;
    
    default:
      return state;
  }
}
