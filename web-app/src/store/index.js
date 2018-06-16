import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from '../reducers';

export default function configureStore(initialState) {
  const middlewares = [thunk];
  const storeEnhancers = [];

  const devtools = window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__();

  //const store = createStore(rootReducer, initialState, devtools);

  const isDev = process.env.NODE_ENV !== 'production';

  const middlewareEnhancer = applyMiddleware(...middlewares);
  storeEnhancers.unshift(middlewareEnhancer);
  // storeEnhancers.unshift(devtools);
  storeEnhancers.push(devtools)

  const store = createStore(rootReducer, initialState, compose(...storeEnhancers));

  if (isDev && module.hot && module.hot.accept) {
    module.hot.accept('../reducers', () => {
      // eslint-disable-next-line global-require
      const nextRootReducer = require('../reducers').default;

      store.replaceReducer(nextRootReducer);
    });
  }
  return store;
}
