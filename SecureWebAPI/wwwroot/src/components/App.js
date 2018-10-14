import React, { Component } from 'react'
import NavBar from './NavBar';
import Footer from './Footer';
import { BrowserRouter, Route } from 'react-router-dom';

import LoginForm from './LoginForm';
import RegisterForm from './RegisterForm';
import Home from './Home';

class App extends Component {
  render(){
    return (
      <BrowserRouter>
        <div>
          <NavBar/>
          <Route exact path="/" component={Home} />
          <Route exact path="/register" component={RegisterForm} />
          <Route exact path="/login" component={LoginForm} />
        </div>
      </BrowserRouter>
    )
  }  
}
export default App; 