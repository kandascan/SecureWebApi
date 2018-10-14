import React from 'react'
import { Link, NavLink } from 'react-router-dom';

export default function NavBar() {
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <Link to="/" className="navbar-brand">Scrum Manager</Link>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse mr-sm-2" id="navbarNav">
            <ul className="navbar-nav">
                <li className="nav-item">
                    <NavLink to="/register" className="nav-link">Register</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink to="/login" className="nav-link">Login</NavLink>
                </li>
            </ul>
        </div>
        </nav>
    )
}
