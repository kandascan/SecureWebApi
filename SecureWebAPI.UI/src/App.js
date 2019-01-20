import React, { Component } from 'react';
import NavBar from './CommonComponent/NavBar';
import Footer from './CommonComponent/Footer';
import { BrowserRouter, Route } from 'react-router-dom';
import jwt_decode from 'jwt-decode';
import setAuthToken from './utils/setAuthToken';
import { setCurrentUser, logoutUser } from './actions/authActions';
import { Provider } from 'react-redux';
import store from './store';
import LoginForm from './AuthComponent/LoginForm';
import RegisterForm from './AuthComponent/RegisterForm';
import Home from './CommonComponent/Home';
import Backlog from './BacklogComponent/Backlog';
import CurrentSprint from './SprintComponent/CurrentSprint';
import Profile from './ProfileComponent/Profile';
import './App.css';
import '../node_modules/react-bootstrap-toggle/dist/react-bootstrap2-toggle';

if(localStorage.smToken) {
  setAuthToken(localStorage.smToken);
  const decoded = jwt_decode(localStorage.smToken);
  store.dispatch(setCurrentUser(decoded));
  const currentTime = Date.now() / 1000;
  if(decoded.exp < currentTime){
    store.dispatch(logoutUser());
    window.location.href = '/login';
  }
}

class App extends Component {
  render() {
    return (
      <Provider store={ store } >
        <BrowserRouter>
        <div>
          <NavBar/>
          <Route exact path="/" component={Home} />
          <Route exact path="/register" component={RegisterForm} />
          <Route exact path="/login" component={LoginForm} />
          <Route exact path="/backlog/:teamid" component={Backlog} />
          <Route exact path="/currentSprint/:teamid" component={CurrentSprint} />
          <Route exact path="/profile" component={Profile} />
          <Footer />
        </div>
      </BrowserRouter>
    </Provider>
    );
  }
}

export default App;
