import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { getBacklogItems, orderBacklogItems, removeTask, getTaskById } from '../actions/backlogActions';
import { currentTeam, getTeamById } from '../actions/teamActions';
import { isTeamMember } from '../actions/authActions';
import BacklogSortable from './BacklogSortable';
import CreateTask from './CreateTask';
import EditTask from './EditTask';
import Spinner from '../CommonComponent/Spinner';

class BacklogComponent extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
        if (this.props.match.params.teamid) {
            this.props.isTeamMember(this.props.match.params.teamid, this.props.history);
            this.props.getBacklogItems(this.props.match.params.teamid);
            this.props.getTeamById(this.props.match.params.teamid);
            //this.props.currentTeam(this.props.match.params.teamid);// to chyba jesli user jest w teamie
        }
    }

    sortBacklogItems = (sortdItems) => {
        this.props.orderBacklogItems(sortdItems);
    }

    removeBacklogTask = (id) => {
        this.props.removeTask(id);
    }

    getTaskById = (id) => {
        this.props.getTaskById(id);
    }

    render() {
        const { items } = this.props.backlog;
        const { showSpinner } = this.props.spinner;
        const { teamid } = this.props.match.params;
        const { team } = this .props.team;
        return (
            <div className="landing landing-background-backlog">
                <div className="dark-overlay text-light">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-2">
                                <EditTask teamid={teamid} onEditTask={this.getTaskById} />
                            </div>
                            <div className="col-md-8 text-center">
                                <h1 className="display-4 mb-4">Backlog: <small>{team.teamName}</small></h1>
                            </div>
                            <div className="col-md-2" style={{padding: "20px"}}>
                                <CreateTask teamid={teamid} />
                            </div>
                            <div className="container">{items.tasks.length > 0 ?
                                (<BacklogSortable
                                    items={items}
                                    sortBacklogItems={this.sortBacklogItems}
                                    removeBacklogTask={this.removeBacklogTask}
                                    getTaskById={this.getTaskById}
                                />) : (<div>
                                    <br />
                                    <h5 className="centerText">There is no items on the backlog yet, create first Task!</h5>
                                </div>)}
                                <div>{showSpinner ? (<Spinner />) : (null)}</div>
                            </div>
                        </div> </div> </div>

            </div>
        )
    }
}

BacklogComponent.propTypes = {
    auth: PropTypes.object.isRequired,
    backlog: PropTypes.object.isRequired,
    spinner: PropTypes.object.isRequired,
    getBacklogItems: PropTypes.func.isRequired,
    orderBacklogItems: PropTypes.func.isRequired,
    getTaskById: PropTypes.func.isRequired,
    currentTeam: PropTypes.func.isRequired,
    getTeamById: PropTypes.func.isRequired,
    isTeamMember: PropTypes.func.isRequired,
    team: PropTypes.object.isRequired
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    backlog: state.backlog,
    spinner: state.spinner,
    team: state.team
});

export default connect(mapStateToProps, { getBacklogItems, orderBacklogItems, removeTask, getTaskById, currentTeam, getTeamById, isTeamMember })(withRouter(BacklogComponent));
