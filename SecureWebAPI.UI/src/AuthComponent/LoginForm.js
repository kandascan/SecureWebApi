import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { loginUser } from '../actions/authActions';
import TextFieldGroup from '../CommonComponent/TextFieldGroup';

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
    if (this.props.auth.isAuthenticated) {
      this.props.history.push('/backlog');
    }
  }

  componentWillReceiveProps = (nextProps) => {
    if (nextProps.auth.isAuthenticated) {
      this.props.history.push('/backlog');
    }
    if (nextProps.errors) {
      this.setState({
        errors: nextProps.errors
      });
    }
  }

  handleChange = (e) => {
    this.setState({
      [e.target.id]: e.target.value
    });
  }

  handleSubmit = (e) => {
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
                <TextFieldGroup
                  type="text"
                  id="username"
                  onChange={this.handleChange}
                  value={this.state.username}
                  placeholder="Name"
                  error={errors.username}
                />
                <TextFieldGroup
                  type="password"
                  id="password"
                  onChange={this.handleChange}
                  value={this.state.password}
                  placeholder="Password"
                  error={errors.password}
                />
                <button className="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
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