import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { loginUser } from './actions/authActions';
import classnames from 'classnames';

class LoginForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      password: '',
      errors: {}
    };
  }  

  componentDidMount() {
    if(this.props.auth.isAuthenticated) {
      this.props.history.push('/backlog');
    }
  }

  componentWillReceiveProps = (nextProps) => {
    if(nextProps.auth.isAuthenticated) {
      this.props.history.push('/backlog');
    }
    if(nextProps.errors){
        this.setState({
            errors: nextProps.errors
        });
    }
  }

  handleChange(e){
    this.setState({
      [e.target.id]: e.target.value
    });
  }

  handleSubmit(e){
    e.preventDefault();    
    const user = {
      username: this.state.username,
      password: this.state.password
    };

    this.props.loginUser(user);
  }
  render() {
    const { errors } = this.state;

    return (
      <div className="landing landing-background-login">
      <div className="dark-overlay landing-inner text-light">
        <div className="container">
      <div className="form-main text-center">
        <form noValidate className="form-signin" onSubmit={this.handleSubmit.bind(this)}>
            <img className="mb-4" src="dist/bootstrap-solid.svg" alt="" width="72" height="72" />
            <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
            <label htmlFor="username" className="sr-only">User name</label> {/*utworzyc nowy komponent dla labela i inputa */}
            <input type="text" id="username" onChange={this.handleChange.bind(this)} value={this.state.username} className={classnames("form-control", {
              'is-invalid' : errors.username
            })} placeholder="Name" autoFocus/>  
            {errors.username && (<div className="invalid-feedback">{errors.username}</div>)}
            <label htmlFor="password" className="sr-only">Password</label>
            <input type="password" id="password" onChange={this.handleChange.bind(this)} value={this.state.password} className={classnames("form-control", {
              'is-invalid' : errors.password
            })} placeholder="Password" />
            {errors.password && (<div className="invalid-feedback">{errors.password}</div>)}
            {/* <div className="checkbox mb-3">
                <label>
                <input type="checkbox" value="remember-me" /> Remember me
                </label>
            </div> */}
            <button className="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
            {/* <p className="mt-5 mb-3 text-muted">&copy; 2018-2019</p> */}
        </form>
        </div></div></div></div>
    )
  }
}

LoginForm.propTypes = {
  loginUser: PropTypes.func.isRequired,
  auth: PropTypes.object.isRequired,
  errors: PropTypes.object.isRequired
}

const mapStateToProps = (state) => ({
  auth: state.auth,
  errors: state.errors
});
export default connect(mapStateToProps, { loginUser })(LoginForm);