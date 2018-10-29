import React, { Component } from 'react';
import NavBar from './NavBar';
import Footer from './Footer';
import { BrowserRouter, Route } from 'react-router-dom';

import LoginForm from './LoginForm';
import RegisterForm from './RegisterForm';
import Home from './Home';
import Backlog from './backlogComponent/Backlog';
import './App.css';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
      <div>
        <NavBar/>
        <Route exact path="/" component={Home} />
        <Route exact path="/register" component={RegisterForm} />
        <Route exact path="/login" component={LoginForm} />
        <Route exact path="/backlog" component={Backlog} />
        <Footer />
      </div>
    </BrowserRouter>
    );
  }
}

export default App;
