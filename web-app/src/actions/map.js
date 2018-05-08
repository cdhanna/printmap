import {
  CHANGE_BOX_COORDINATES
} from '../constants/actionTypes';

export const updateCoordinates = (topLeftLat) => (dispatch) => {
  return dispatch({
    type: CHANGE_BOX_COORDINATES,
    topLeftLat,
  });
};
