import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { getCurrentSprint, createSprint } from '../actions/sprintActions';
import { getTaskById } from '../actions/backlogActions';
import { currentTeam } from '../actions/teamActions';
import { isTeamMember } from '../actions/authActions';
import Spinner from '../CommonComponent/Spinner';
import EditTask from '../BacklogComponent/EditTask';
import SortableComponent from './Sortable';
import $ from 'jquery';
window.jQuery = window.$ = $;

class CurrentSprint extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
        if (this.props.match.params.teamid) {
            this.props.isTeamMember(this.props.match.params.teamid, this.props.history);
            //this.props.currentTeam(this.props.match.params.teamid);
            this.props.getCurrentSprint(this.props.match.params.teamid);
        }
    }

    onCreateSprint = () => {
        const newSprint = {
            TeamId: this.props.match.params.teamid
        }
        this.props.createSprint(newSprint);
    }

    render() {
        const { sprint } = this.props;
        const { showSpinner } = this.props.spinner;
        let view = null;
        if (sprint != null && sprint.tasks != null) {
            if (sprint.tasks.length > 0) {
                view = sprint.tasks.map(t => (
                    <div className="col-sm ">
                        <h6>{t.columnName}:</h6>
                    </div>
                )
                );
            }
        }

        let sprintView;
        if (sprint.sprintId === 0) {
            sprintView = (<div>
                <h3>There is no active sprint yet for team: {this.props.match.params.teamid}</h3>
                <button onClick={this.onCreateSprint} className="btn btn-info" >Create first Sprint</button>
            </div>)
        } else {
            sprintView = (<div className="col-md-12 text-center">
                <div className="container">
                    <h2 style={{ marginTop: "-50px" }}>{sprint.sprint.sprintName}</h2>
                    <br />
                    <div className="row">
                        {view}
                    </div>
                </div>
                <SortableComponent />
                <EditTask teamid={sprint.teamId} />
            </div>)
        }
        return (
            <div className="landing landing-background-currentSprint">
                <div className="dark-overlay landing-inner text-light">
                    <div className="container">
                        <div className="row">
                            {sprintView}
                            <div>{showSpinner ? (<Spinner />) : (null)}</div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

CurrentSprint.propTypes = {
    auth: PropTypes.object.isRequired,
    sprint: PropTypes.object.isRequired,
    currentTeam: PropTypes.func.isRequired,
    getCurrentSprint: PropTypes.func.isRequired,
    createSprint: PropTypes.func.isRequired,
    getTaskById: PropTypes.func.isRequired,
    isTeamMember: PropTypes.func.isRequired,
    spinner: PropTypes.object.isRequired
}
const mapStateToProps = (state) => ({
    auth: state.auth,
    sprint: state.sprint,
    spinner: state.spinner
});
export default connect(mapStateToProps, { getCurrentSprint, currentTeam, createSprint, getTaskById, isTeamMember })(withRouter(CurrentSprint));
