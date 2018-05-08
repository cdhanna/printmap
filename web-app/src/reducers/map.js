import queryString from 'query-string';
import { CHANGE_BOX_TOPLEFT, CHANGE_BOX_BOTRIGHT } from '../constants/actionTypes';


export class LatLon {
  constructor(lat, lon){
    this.lat = lat,
    this.lon = lon;
  }
}
export class MapRect {
  constructor(topLeft, botRight){
    this.topLeft = topLeft;
    this.botRight = botRight; 
  }
}

export const initialState = {
  mapRect: new MapRect( new LatLon(0,0), new LatLon(1, 1))
};

export default function map(state = initialState, action) {
  switch (action.type) {
    case CHANGE_BOX_TOPLEFT:
      return {
        ...state,
        mapRect: {
          ...state.mapRect,
          topLeft: {
            lat: action.topLeft.lat || state.mapRect.topLeft.lat,
            lon: action.topLeft.lon || state.mapRect.topLeft.lon
          }
        }
      }
    case CHANGE_BOX_BOTRIGHT:
      return {
        ...state,
        mapRect: {
          ...state.mapRect,
          botRight: {
            lat: action.botRight.lat || state.mapRect.botRight.lat,
            lon: action.botRight.lon || state.mapRect.botRight.lon
          }
        }
      }
    default:
      return state;
  }
}
