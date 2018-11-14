import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

class CurrentSprint extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
    }
    render() {
        return (
            <div className="landing landing-background-currentSprint">
                <div className="dark-overlay landing-inner text-light">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-12 text-center">
                                <h1>Here will be board with current task for users</h1>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

CurrentSprint.propTypes = {
    auth: PropTypes.object.isRequired
}
const mapStateToProps = (state) => ({
    auth: state.auth,
});
export default connect(mapStateToProps)(CurrentSprint);
