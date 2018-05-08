import queryString from 'query-string';
import {
  CHANGE_BOX_COORDINATES,
} from '../constants/actionTypes';

export const initialState = {
  topLeftLat: 56,
};

export default function map(state = initialState, action) {
  switch (action.type) {
    case CHANGE_BOX_COORDINATES:
      return {
        ...state,
        topLeftLat: action.topLeftLat,
      };
    default:
      return state;
  }
}
