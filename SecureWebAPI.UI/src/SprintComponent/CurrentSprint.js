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
import Moment from 'react-moment';
import classnames from 'classnames';

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

    onCreateSprint = (sprintId) => {
        const newSprint = {
            TeamId: this.props.match.params.teamid,
            SprintId: sprintId
        }
        this.props.createSprint(newSprint);
    }

    render() {
        const { sprint } = this.props;
        const { showSpinner } = this.props.spinner;
        let sprintPercent = 0;

        let remainingDaysToEndSprint = 0;
        if (sprint.sprint != null) {
            remainingDaysToEndSprint = Math.ceil((Date.parse(sprint.sprint.endDate) - Date.now()) / (1000 * 3600 * 24));
            sprintPercent = Math.round(100 - ((remainingDaysToEndSprint / 14) * 100));
            if(sprintPercent > 100 && remainingDaysToEndSprint < 0){ 
                sprintPercent = 100;
                remainingDaysToEndSprint = 0;
            }
        }

        let sprintView;
        if (sprint.sprintId === 0) {
            sprintView = (<div className="container">
                <div className="row">
                    <h3>There is no active sprint yet for team: {this.props.match.params.teamid}</h3>
                </div>
            </div>)
        } else {
            sprintView = (<div className="col-md-12 text-center">
                <div className="container">
                    <div className="row">
                        <div className="col-2">
                            <span style={{ position: "absolute", bottom: "0", left: "15px" }} className="badge badge-secondary">Sprint time: {sprintPercent}%</span>
                        </div>
                        <div className="col-8">
                            <h2>{sprint.sprint.sprintName}</h2>
                        </div>
                        <div className="col-2">
                            <span style={{ position: "absolute", bottom: "0", right: "16px" }} className="badge badge-secondary">{remainingDaysToEndSprint} {remainingDaysToEndSprint == 1 ? 'day' : 'days'} remaining</span>
                        </div>
                    </div>
                    <div className="progress">
                        {sprintPercent == 0 ? (<div
                            className="progress-bar bg-light"
                            role="progressbar"
                            style={{ width: `${10}%` }}
                            aria-valuenow="25"
                            aria-valuemin="0"
                            aria-valuemax="100">
                            <Moment style={{ textAlign: "left", paddingLeft: "10px", color: "black", fontWeight: "bold" }} format="D MMM YYYY" withTitle>
                                {sprint.sprint.startDate}
                            </Moment>
                        </div>) : (<div
                            className={classnames("progress-bar", {
                                'bg-success': sprintPercent <= 25,
                                'bg-info': sprintPercent > 25 && sprintPercent <= 50,
                                'bg-warning': sprintPercent > 50 && sprintPercent <= 75,
                                'bg-danger': sprintPercent > 75 && sprintPercent <= 100,
                            })}
                            role="progressbar"
                            style={{ width: `${sprintPercent}%` }}
                            aria-valuenow="25"
                            aria-valuemin="0"
                            aria-valuemax="100">
                            {sprintPercent < 100 ? (<Moment style={{ textAlign: "left", paddingLeft: "10px", fontWeight: "bold" }} format="D MMM YYYY" withTitle>
                                {sprint.sprint.startDate}
                            </Moment>) : (
                                    <div>
                                        <Moment style={{ float: "left", paddingLeft: "10px", fontWeight: "bold" }} format="D MMM YYYY" withTitle>
                                            {sprint.sprint.startDate}
                                        </Moment>
                                        <Moment style={{ float: "right", paddingRight: "10px", color: "black", fontWeight: "bold" }} format="D MMM YYYY" withTitle>
                                            {sprint.sprint.endDate}
                                        </Moment>
                                    </div>
                                )}
                        </div>)}

                        {sprintPercent == 100 ? (null) : (<div
                            className="progress-bar bg-light"
                            role="progressbar"
                            style={{ width: `${101 - sprintPercent}%` }}
                            aria-valuenow="25"
                            aria-valuemin="0"
                            aria-valuemax="100">
                            <Moment style={{ textAlign: "right", paddingRight: "10px", color: "black", fontWeight: "bold" }} format="D MMM YYYY" withTitle>
                                {sprint.sprint.endDate}
                            </Moment>
                        </div>)}

                    </div>
                </div>
                <hr />
                <SortableComponent />
                <EditTask teamid={sprint.teamId} />
            </div>)
        }
        return (
            <div className="landing landing-background-currentSprint">
                <div className="dark-overlay text-light">
                {sprint.sprintId === 0 || remainingDaysToEndSprint === 0 ? (
                    <div className="container">
                    <div className="row">
                        <div className="col-10"></div>
                        <div className="col-2" style={{paddingTop: "10px", marginBottom: "-20px"}}>
                            <button onClick={() => this.onCreateSprint(sprint.sprintId)} className="btn btn-info" >Create new Sprint</button>
                        </div>
                    </div>
                </div>
                ) : (null)}                    
                    {sprintView}
                    <div>{showSpinner ? (<Spinner />) : (null)}</div>
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
