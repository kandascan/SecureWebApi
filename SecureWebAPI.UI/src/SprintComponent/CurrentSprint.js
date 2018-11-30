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
                                <div className="container">
                                    <div className="row">
                                        <div className="col-sm ">
                                        <h6>ToDO:</h6>
                                        </div>
                                        <div className="col-sm">
                                        <h6>In Progress:</h6>
                                        </div>
                                        <div className="col-sm">
                                        <h6>QA</h6>
                                        </div>
                                        <div className="col-sm">
                                        <h6>Done:</h6>
                                        </div>
                                    </div>
                                </div>
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
