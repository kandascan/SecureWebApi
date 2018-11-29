import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import SortableComponent from './Sortable';
import $ from 'jquery';
window.jQuery = window.$ = $;

class CurrentSprint extends Component {
    render() {
        return (
            <div className="landing landing-background-currentSprint">
                <div className="dark-overlay landing-inner text-light">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-12 text-center">
                                <h1>Here will be board with current task for users</h1>
                                <SortableComponent />
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
