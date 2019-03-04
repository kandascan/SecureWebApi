import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getTaskById, updateTask } from '../actions/backlogActions';
import { getEfforts, getPriorities, getSprintsList } from '../actions/dictionaryActions';
import { getTeamUsers } from '../actions/userActions';
import { toggleEditTaskModal } from '../actions/modalActions';
import ModalComponent from '../CommonComponent/ModaComponent';
import $ from 'jquery';
window.jQuery = window.$ = $;

class EditTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = { taskId: 0, orderId: 0, taskname: '', description: '', effort: -1, priority: -1, userId: '', sprint: -1, toggleEdit: true };
    }

    componentWillReceiveProps = (nextProps) => {
        this.setState({
            taskId: nextProps.backlog.task.task.id,
            teamid: nextProps.backlog.task.task.teamid,
            taskname: nextProps.backlog.task.task.taskname, 
            description: nextProps.backlog.task.task.description, 
            effort: nextProps.backlog.task.task.effortId, 
            priority: nextProps.backlog.task.task.priorityId, 
            orderId: nextProps.backlog.task.task.orderId,
            sprint: nextProps.backlog.task.task.sprint,
            userId: nextProps.backlog.task.task.userId
        })
    }

    componentDidMount = () =>{      
        //const { teamUsers } = this.props.user;
        //if( teamUsers === null){
            this.props.getTeamUsers(this.props.teamid)
        //}
       // const { priorities, efforts, sprints } = this.props.dics;
        //if (priorities === null && efforts === null) {
            this.props.getEfforts();
            this.props.getPriorities();
       // }

       // if(sprints === null){
            this.props.getSprintsList(this.props.teamid);
       // }
    }

    onToggleCancel = () => {
        this.props.toggleEditTaskModal();
        this.setState({ toggleEdit: true });
    }

    onToggleEdit = () => {
        this.setState({ toggleEdit: !this.state.toggleEdit });
      }

    handleChange = (e) => {       
        this.setState({ [e.target.name]: e.target.value });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        let task = {
            id: this.state.taskId,
            teamid: this.props.teamid,
            taskname: this.state.taskname,
            description: this.state.description,
            effortId: +this.state.effort,
            priorityId: +this.state.priority,
            userId: this.state.userId,
            orderId: this.state.orderId,
            sprint: this.state.sprint
        }
        this.props.updateTask(task);
        this.setState({ toggleEdit: true });
    }

    render() {
        const { showEditTaskModal } = this.props.modal;
        const { priorities, efforts, sprints } = this.props.dics;
        const { teamUsers } = this.props.user;
        let ddlPriorities, ddlEfforts, ddlTeamSprints = null;
        if (priorities != null && efforts != null) {
            ddlPriorities = priorities.priorities.map((priority) =>
                <option key={priority.priorityId} value={priority.priorityId}>{priority.priorityName}</option>
            );
            ddlEfforts = efforts.efforts.map((effort) =>
                <option key={effort.effortId} value={effort.effortId}>{effort.effortName}</option>
            );
        }
        let ddlUsers = null;
        if (teamUsers != null) {
            ddlUsers = teamUsers.teamUsers.map((user) =>
                <option key={user.userId} value={user.userId}>{user.userName}</option>
            );
        }
debugger
        if(sprints != null){
            ddlTeamSprints = sprints.sprintsList.map((sprint) =>
                <option key={sprint.sprintId} value={sprint.sprintId}>{sprint.sprintName}</option>
            );
        }

        const content = (<form onSubmit={this.handleSubmit}>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Task name:</label>
                    <input name="taskname" type="text" value={this.state.taskname} onChange={this.handleChange} disabled={this.state.toggleEdit} className="form-control" />
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Description</label>
                    <textarea rows="3" name="description" value={this.state.description} onChange={this.handleChange} disabled={this.state.toggleEdit} className="form-control"></textarea>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Effort:</label>
                    <select className="custom-select mr-sm-2" name="effort" onChange={this.handleChange} disabled={efforts === null || this.state.toggleEdit} value={this.state.effort} >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlEfforts}
                    </select>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Priority:</label>
                    <select className="custom-select mr-sm-2" 
                    name="priority" onChange={this.handleChange} 
                    disabled={priorities === null || this.state.toggleEdit} 
                    value={this.state.priority} >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlPriorities}
                    </select>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Sprint:</label>
                    <select
                        className={"custom-select mr-sm-2"}
                        name="sprint"
                        onChange={this.handleChange}
                        disabled={sprints === null  || this.state.toggleEdit} 
                        value={this.state.sprint}>
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlTeamSprints}
                    </select>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Username:</label>
                    <select
                        className={"custom-select mr-sm-2"}
                        name="userId"
                        onChange={this.handleChange}
                        disabled={teamUsers === null || this.state.toggleEdit} 
                        value={this.state.userId}>
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlUsers}
                    </select>
                </div>
            </div>
    </form>);
    let toggleEdit = (
    <div><label className="switch">
    <input type="checkbox" checked={!this.state.toggleEdit} onChange={this.onToggleEdit} />
    <span className="slider"></span>
  </label></div>)
        return (
                <ModalComponent 
                    show={showEditTaskModal}
                    header="Task details"
                    content={content}
                    onCancelClick={this.onToggleCancel}
                    onSubmitClick={this.handleSubmit}
                    onEditSwitch={toggleEdit}
                />
        );
    }
}

EditTask.propTypes = {
    getTaskById: PropTypes.func.isRequired,
    updateTask: PropTypes.func.isRequired,
    getEfforts: PropTypes.func.isRequired,
    getPriorities: PropTypes.func.isRequired,
    user: PropTypes.object.isRequired,
    getSprintsList: PropTypes.func.isRequired,
    getTeamUsers: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    backlog: state.backlog,
    dics: state.dics,
    modal: state.modal,
    user: state.user
});

export default connect(mapStateToProps, { getTaskById, toggleEditTaskModal, getEfforts, getPriorities, updateTask, getSprintsList, getTeamUsers })(EditTask);