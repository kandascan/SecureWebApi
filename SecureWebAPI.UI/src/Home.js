import React from 'react'
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div className="landing landing-background-home">
    <div className="dark-overlay landing-inner text-light">
      <div className="container">
        <div className="row">
          <div className="col-md-12 text-center">
            <h1 className="display-3 mb-4">Scrum Manager
            </h1>
            <p className="lead">Great and free tool for developers to smart manage their work</p>
            <hr />
            <Link to="/register" className="btn btn-lg btn-info mr-2">Sign Up</Link>
            <Link to="/login" className="btn btn-lg btn-light">Login</Link>
          </div>
        </div>
      </div>
    </div>
  </div>
  )
}
export default Home;
