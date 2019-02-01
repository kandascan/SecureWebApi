import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { registerUser } from '../actions/authActions';
import TextFieldGroup from '../CommonComponent/TextFieldGroup';

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
        if (this.props.auth.isAuthenticated) {
            this.props.history.push('/backlog');
        }
    }

    componentWillReceiveProps = (nextProps) => {
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
                <div className="dark-overlay landing-inner ">
                    <div className="container">
                        <div className="form-main text-center">
                            <form noValidate className="form-signin" onSubmit={this.handleSubmit}>
                                <img src="logo.png" alt="" width="172" height="172" style={{ width: "auto" }} />
                                <h1 className="h3 mb-3 font-weight-normal">Please sign up</h1>
                                <TextFieldGroup
                                    type="text"
                                    id="username"
                                    onChange={this.handleChange}
                                    value={this.state.username}
                                    placeholder="Name"
                                    error={errors.username}
                                />
                                <TextFieldGroup
                                    type="email"
                                    id="email"
                                    onChange={this.handleChange}
                                    value={this.state.email}
                                    placeholder="Email"
                                    error={errors.email}
                                />
                                <TextFieldGroup
                                    type="password"
                                    id="password"
                                    onChange={this.handleChange}
                                    value={this.state.password}
                                    placeholder="Password"
                                    error={errors.password}
                                />
                                <button className="btn btn-lg btn-primary btn-block" type="submit">Sign up</button>
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