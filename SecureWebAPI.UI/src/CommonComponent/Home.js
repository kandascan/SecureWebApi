import React, { Component } from 'react'
import { Link } from 'react-router-dom';
import { PropTypes } from 'prop-types';
import { connect } from 'react-redux';
import Team from '../TeamComponent/Team';
import Spinner from '../CommonComponent/Spinner';


class Home extends Component {
  componentDidMount() {
    if (this.props.auth.isAuthenticated) {
      this.props.history.push('/');
    }
  }
  render() {
    return (
      <div className="landing landing-background-home">
        <div className="dark-overlay landing-inner text-light">
          <div className="container">
            <div className="row">
              <div className="col-md-12 text-center">
                {!this.props.auth.isAuthenticated ? (<div><h1 className="display-3 mb-4">Scrum Manager
            </h1>
                  <p className="lead">Great and free tool for developers to smart manage their work</p>
                  <hr /><Link to="/register" className="btn btn-lg btn-info mr-2">Sign Up</Link>
                  <Link to="/login" className="btn btn-lg btn-light">Login</Link></div>) : (
                    <div className="container">
                      <h1 className="display-4">You are login into Scrum Manager </h1>
                      <p className="lead">Start your work from review current Sprint or if you are here first time go to backlog and create first task.</p>
                      <Team />
                    </div>
                  )}
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }
}

Home.propTypes = {
  auth: PropTypes.object.isRequired
};

const mapStateToProps = (state) => ({
  auth: state.auth
});

export default connect(mapStateToProps)(Home);
