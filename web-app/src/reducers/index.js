import { combineReducers } from 'redux';
import mapState from './coordinates';

const rootReducer = combineReducers({
  mapState,
});

export default rootReducer;
