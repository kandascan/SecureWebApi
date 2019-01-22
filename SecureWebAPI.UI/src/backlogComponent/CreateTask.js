import React from 'react';
import { Button } from 'reactstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createTask } from '../actions/backlogActions';
import { getEfforts, getPriorities, getSprintsList } from '../actions/dictionaryActions';
import { toggleCreateTaskModal, clearErrorsModal } from '../actions/modalActions';
import ModalComponent from '../CommonComponent/ModaComponent';
import TextFieldGroup from '../CommonComponent/TextFieldGroup';
import classnames from 'classnames';

import $ from 'jquery';
window.jQuery = window.$ = $;

class CreateTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = { teamid: '', taskname: '', description: '', effort: -1, priority: -1, username: '', sprint: -1, errors: {} };
    }

    toggle = () => {
        this.setState({ taskname: '', description: '', effort: -1, priority: -1, username: '', sprint: -1, errors: {} });
        this.props.toggleCreateTaskModal();
        const { priorities, efforts, sprints } = this.props.dics;
        if (priorities === null && efforts === null) {
            this.props.getEfforts();
            this.props.getPriorities();
        }

        if(sprints === null){
            this.props.getSprintsList(this.props.teamid);
        }
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value, errors: {} });
    }

    handleCancel = () => {
        this.props.toggleCreateTaskModal();
        this.props.clearErrorsModal();
    }

    handleSubmit = (e) => {
        e.preventDefault();
        let newTask = {
            teamid: this.props.teamid,
            taskname: this.state.taskname,
            description: this.state.description,
            effortId: +this.state.effort,
            priorityId: +this.state.priority,
            username: this.state.username,
            sprint: this.state.sprint
        }
        this.props.createTask(newTask);
    }

    render() {
        const { errors } = this.props;
        const { showCreateTaskModal } = this.props.modal;
        const { priorities, efforts, sprints } = this.props.dics;
        let ddlPriorities, ddlEfforts, ddlTeamSprints = null;
        if (priorities != null && efforts != null) {
            ddlPriorities = priorities.priorities.map((priority) =>
                <option key={priority.priorityId} value={priority.priorityId}>{priority.priorityName}</option>
            );
            ddlEfforts = efforts.efforts.map((effort) =>
                <option key={effort.effortId} value={effort.effortId}>{effort.effortName}</option>
            );
        }

        if(sprints != null){
            ddlTeamSprints = sprints.sprintsList.map((sprint) =>
                <option key={sprint.sprintId} value={sprint.sprintId}>{sprint.sprintName}</option>
            );
        }

        const content = (<form onSubmit={this.handleSubmit}>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Task name:</label>
                    <TextFieldGroup
                        type="text"
                        name="taskname"
                        onChange={this.handleChange}
                        value={this.state.taskname}
                        placeholder=""
                        error={errors.taskname}
                    />
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Description</label>
                    <textarea
                        rows="3"
                        name="description"
                        value={this.state.description}
                        onChange={this.handleChange}
                        className={classnames("form-control", {
                            'is-invalid': errors.description
                        })} >
                    </textarea>
                    {errors.description && (<div className="invalid-feedback">{errors.description}</div>)}
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Effort:</label>
                    <select
                        className={classnames("custom-select mr-sm-2", {
                            'is-invalid': errors.effort
                        })}
                        name="effort"
                        onChange={this.handleChange}
                        disabled={efforts === null} >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlEfforts}
                    </select>
                    {errors.effort && (<div className="invalid-feedback">{errors.effort}</div>)}
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Priority:</label>
                    <select
                        className={classnames("custom-select mr-sm-2", {
                            'is-invalid': errors.priority
                        })}
                        name="priority"
                        onChange={this.handleChange}
                        disabled={priorities === null} >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlPriorities}
                    </select>
                    {errors.priority && (<div className="invalid-feedback">{errors.priority}</div>)}
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Sprint:</label>
                    <select
                        className={"custom-select mr-sm-2"}
                        name="sprint"
                        onChange={this.handleChange}
                        disabled={sprints === null ? true : false} 
                        >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlTeamSprints}
                    </select>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Username:</label>
                    <input name="username" type="text" value={this.state.username} onChange={this.handleChange} className="form-control" />
                </div>
            </div>
        </form>);
        return (
            <div>
                <Button color="success" style={{ float: "right" }} onClick={this.toggle} >Create task</Button>
                <ModalComponent
                    show={showCreateTaskModal}
                    header="New task for backlog"
                    content={content}
                    onCancelClick={this.handleCancel}
                    onSubmitClick={this.handleSubmit}
                />
            </div>
        );
    }
}

CreateTask.propTypes = {
    createTask: PropTypes.func.isRequired,
    toggleCreateTaskModal: PropTypes.func.isRequired,
    getEfforts: PropTypes.func.isRequired,
    getPriorities: PropTypes.func.isRequired,
    clearErrorsModal: PropTypes.func.isRequired,
    errors: PropTypes.object.isRequired,
    getSprintsList: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    backlog: state.backlog,
    dics: state.dics,
    modal: state.modal,
    errors: state.errors
});

export default connect(mapStateToProps, { createTask, toggleCreateTaskModal, getEfforts, getPriorities, clearErrorsModal, getSprintsList })(CreateTask);