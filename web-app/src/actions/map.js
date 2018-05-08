import {
  CHANGE_BOX_TOPLEFT, CHANGE_BOX_BOTRIGHT
} from '../constants/actionTypes';
import { MapRect, LatLon } from '../reducers/map';

// export const updateCoordinates = (mapRect) => (dispatch) => {
//   return dispatch({
//     type: CHANGE_BOX_COORDINATES,
//     mapRect,
//     //mapRect: new MapRect( new LatLon(topLeft.lat, topLeft.lon), new LatLon(botRight.lat, botRight.lon))
//   });
// };


export const updateTopLeft = (topLeft) => (dispatch) => {
  return dispatch({
    type: CHANGE_BOX_TOPLEFT,
    topLeft,
  });
};

export const updateBotRight = (coord) => (dispatch) => {
  return dispatch({
    type: CHANGE_BOX_BOTRIGHT,
    botRight: coord
  });
};