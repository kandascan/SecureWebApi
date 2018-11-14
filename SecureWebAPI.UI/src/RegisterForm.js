import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withRouter } from 'react-router-dom';
import classnames from 'classnames';
import { connect } from 'react-redux';
import { registerUser } from './actions/authActions';

class RegisterForm extends Component {
    constructor() {
        super();
        this.state = {
            username: '',
            email: '',
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
        if(nextProps.errors){
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
        const newUser = {
            username: this.state.username,
            email: this.state.email,
            password: this.state.password,
        };

        this.props.registerUser(newUser, this.props.history);
    }  

    render() {
        const { errors } = this.state;
        return (
            <div className="landing landing-background-register">
                <div className="dark-overlay landing-inner text-light">
                    <div className="container">
                        <div className="form-main text-center">
                            <form noValidate className="form-signin" onSubmit={this.handleSubmit}>
                                <img className="mb-4" src="dist/bootstrap-solid.svg" alt="" width="72" height="72" />
                                <h1 className="h3 mb-3 font-weight-normal">Please sign up</h1>
                                <label htmlFor="username" className="sr-only">User name</label> {/*utworzyc nowy komponent dla labela i inputa */}
                                <input 
                                type="text" 
                                id="username" 
                                onChange={this.handleChange} 
                                value={this.state.username} 
                                className={classnames("form-control", {
                                    'is-invalid': errors.username
                                })} 
                                placeholder="Name" 
                                autoFocus />
                                {errors.username && (<div className="invalid-feedback">{errors.username}</div>)}
                                <label htmlFor="email" className="sr-only">Email address</label>
                                <input 
                                type="email" 
                                id="email" 
                                onChange={this.handleChange} 
                                value={this.state.email} 
                                className={classnames("form-control", {
                                    'is-invalid': errors.email
                                })} 
                                placeholder="Email address" />
                                {errors.email && (<div className="invalid-feedback">{errors.email}</div>)}
                                <label htmlFor="password" className="sr-only">Password</label>
                                <input 
                                type="password" 
                                id="password" 
                                onChange={this.handleChange} 
                                value={this.state.password} className={classnames("form-control", {
                                    'is-invalid': errors.password
                                })} placeholder="Password" />
                                {errors.password && (<div className="invalid-feedback">{errors.password}</div>)}
                                <button className="btn btn-lg btn-primary btn-block" type="submit">Sign up</button>
                                {/* <p className="mt-5 mb-3 text-muted">&copy; 2018-2019</p> */}
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

RegisterForm.propTypes = {
    registerUser: PropTypes.func.isRequired,
    auth: PropTypes.object.isRequired,
    errors: PropTypes.object.isRequired
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    errors: state.errors
});

export default connect(mapStateToProps, { registerUser })(withRouter(RegisterForm));