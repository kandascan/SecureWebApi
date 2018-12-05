import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getBacklogItems, orderBacklogItems, removeTask, getTaskById } from '../actions/backlogActions';
import BacklogSortable from './BacklogSortable';
import CreateTask from './CreateTask';
import EditTask from './EditTask';
import Spinner from '../CommonComponent/Spinner';

class BacklogComponent extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
        if(this.props.match.params.teamid){
            this.props.getBacklogItems(this.props.match.params.teamid);
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
        return (
            <div className="landing landing-background-backlog">
                <div className="dark-overlay landing-inner text-light">
                    <CreateTask teamid={teamid} />
                    <EditTask teamid={teamid} onEditTask={this.getTaskById} />
                    <h1 className="centerText" style={{ marginTop: "-50px" }}>Main Backlog</h1>
                    <div className="container">{items.tasks.length > 0  ?
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
                </div>
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
    getTaskById: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    backlog: state.backlog,
    spinner: state.spinner
});

export default connect(mapStateToProps, { getBacklogItems, orderBacklogItems, removeTask, getTaskById })(BacklogComponent);
