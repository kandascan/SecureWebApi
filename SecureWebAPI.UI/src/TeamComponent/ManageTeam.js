import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

class ManageTeam extends Component {
    render() {
        return (
            <div className="landing landing-background-home">
                <div className="dark-overlay text-light">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-2">
                                Left panel
                            </div>
                            <div className="col-md-8 text-center">
                                <h1 className="display-4 mb-4">Manage team</h1>
                            </div>
                            <div className="col-md-2" style={{ padding: "20px" }}>
                                Right panel
                            </div>
                            <div className="container">
                                content
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

ManageTeam.propTypes = {
    team: PropTypes.object.isRequired,
    errors: PropTypes.object.isRequired,
    spinner: PropTypes.object.isRequired
}

const mapStateToProps = (state) => ({
    team: state.team,
    errors: state.errors,
    spinner: state.spinner
});

export default connect(mapStateToProps, {})(ManageTeam);