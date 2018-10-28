import React from 'react'
import { Link, NavLink } from 'react-router-dom';

export default function NavBar() {
    return (
        // <nav className="navbar navbar-expand-lg navbar-light bg-light">
        // <Link to="/" className="navbar-brand">Scrum Manager</Link>
        // <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        //     <span className="navbar-toggler-icon"></span>
        // </button>
        // <div className="collapse navbar-collapse mr-sm-2" id="navbarNav">
        //     <ul className="navbar-nav">
        //         <li className="nav-item">
        //             <NavLink to="/register" className="nav-link">Register</NavLink>
        //         </li>
        //         <li className="nav-item">
        //             <NavLink to="/login" className="nav-link">Login</NavLink>
        //         </li>
        //     </ul>
        // </div>
        // </nav>
        <nav className="navbar navbar-expand-sm navbar-dark bg-dark mb-4">
        <div className="container">
          <Link className="navbar-brand" to="/">Scrum Manager</Link>
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#mobile-nav">
            <span className="navbar-toggler-icon"></span>
          </button>
    
          <div className="collapse navbar-collapse" id="mobile-nav">
            <ul className="navbar-nav mr-auto">
              <li className="nav-item">
                <Link className="nav-link" to="/backlog"> Backlog
                </Link>
              </li>
            </ul>
    
            <ul className="navbar-nav ml-auto">
              <li className="nav-item">
                <NavLink className="nav-link" to="/register">Sign Up</NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/login">Login</NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    )
}
