import React, { Component } from 'react'
import { Link, NavLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { logoutUser } from './actions/authActions';
import { clearBacklog } from './actions/taskActions';

class NavBar extends Component {
  onLogoutClick = (e) => {
    e.preventDefault();
    this.props.clearBacklog();
    this.props.logoutUser();
    window.location.href = '/';
  }
  render() {
    const { isAuthenticated, user } = this.props.auth;
    const authLinks = (
      <div className="collapse navbar-collapse" id="mobile-nav">
        <ul className="navbar-nav mr-auto">
          {/* <li className="nav-item">
            <NavLink className="nav-link" to="/backlog">Backlog</NavLink>
          </li> */}
          <li className="nav-item">
            <NavLink className="nav-link" to="/currentSprint">Current Sprint</NavLink>
          </li>
        </ul>
        <ul className="navbar-nav ml-auto">
          <li className="nav-item">
            <NavLink className="nav-link" to="/profile">{user.sub}</NavLink>
          </li>
          <li className="nav-item">
            <a href="" onClick={this.onLogoutClick} className="nav-link" to="/login">
              <span className="oi oi-account-logout"></span>{' '}Logout
          </a>
          </li>
        </ul>
      </div>
    );

    const userLinks = (
      <div className="collapse navbar-collapse" id="mobile-nav">
        <ul className="navbar-nav ml-auto">
          <li className="nav-item">
            <NavLink className="nav-link" to="/register">Sign Up</NavLink>
          </li>
          <li className="nav-item">
            <NavLink className="nav-link" to="/login">Login</NavLink>
          </li>
        </ul>
      </div>
    );
    
    return (
      <nav className="navbar navbar-expand-sm navbar-dark bg-dark mb-4">
        <div className="container">
          <Link className="navbar-brand" to="/">Scrum Manager</Link>
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#mobile-nav">
            <span className="navbar-toggler-icon"></span>
          </button>
          {isAuthenticated ? authLinks : userLinks}
        </div>
      </nav>
    )
  }
}

NavBar.propTypes = {
  logoutUser: PropTypes.func.isRequired,
  auth: PropTypes.object.isRequired
}

const mapStateToProps = (state) => ({
  auth: state.auth
});

export default connect(mapStateToProps, { logoutUser, clearBacklog })(NavBar);
