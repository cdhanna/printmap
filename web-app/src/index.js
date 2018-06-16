import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';

// import 'leaflet/dist/leaflet.css';
// import 'leaflet/dist/leaflet.js';
import App from './App';
import { Provider } from 'react-redux';
import registerServiceWorker from './registerServiceWorker';
import configureStore from './store';


const store = configureStore({});

const rootElement = document.getElementById('root');

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  rootElement,
);

registerServiceWorker();
