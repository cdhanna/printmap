import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from '../reducers';

export default function configureStore(initialState) {
  const middlewares = [thunk];
  const storeEnhancers = [];

  const isDev = process.env.NODE_ENV !== 'production';

  const middlewareEnhancer = applyMiddleware(...middlewares);
  storeEnhancers.unshift(middlewareEnhancer);

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
