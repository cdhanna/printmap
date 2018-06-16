import {
  CHANGE_BOX_TOPLEFT, CHANGE_BOX_BOTRIGHT, CHANGE_BOX
} from '../constants/actionTypes';
import { MapRect, LatLon } from '../reducers/coordinates';


export const updateCoordinates = (data) => (dispatch) => {
  return dispatch({
    type: CHANGE_BOX,
    topLeft: new LatLon(data.top, data.left),
    botRight: new LatLon(data.bot, data.right)
  })
};