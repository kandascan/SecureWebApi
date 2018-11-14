import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

class Profile extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
    }
    render() {
        const { user } = this.props.auth;

        return (
            <div className="landing landing-background-profile">
                <div className="dark-overlay landing-inner text-light">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-12 text-center">
                                <h1>User info</h1>
                                <h3>{user.sub}</h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}
Profile.propTypes = {
    auth: PropTypes.object.isRequired
}
const mapStateToProps = (state) => ({
    auth: state.auth,
});
export default connect(mapStateToProps)(Profile);