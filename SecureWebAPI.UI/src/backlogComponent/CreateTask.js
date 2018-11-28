import React from 'react';
import { Button } from 'reactstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createTask } from '../actions/backlogActions';
import { getEfforts, getPriorities } from '../actions/dictionaryActions';
import { toggleCreateTaskModal } from '../actions/modalActions';
import ModalComponent from '../CommonComponent/ModaComponent';
import $ from 'jquery';
window.jQuery = window.$ = $;
class CreateTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = { taskname: '', description: '', effort: -1, priority: -1, username: '' };
    }

    toggle = () => {
        this.props.toggleCreateTaskModal();
        const { priorities, efforts } = this.props.dics;
        if (priorities === null && efforts === null) {
            this.props.getEfforts();
            this.props.getPriorities();
        }
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        let newTask = {
            taskname: this.state.taskname,
            description: this.state.description,
            effortId: +this.state.effort,
            priorityId: +this.state.priority,
            username: this.state.username,
        }
        this.props.createTask(newTask);
        this.setState({ taskname: '', description: '', effort: -1, priority: -1, username: '' });
    }

    render() {
        const { showCreateTaskModal } = this.props.modal;
        const { priorities, efforts } = this.props.dics;
        let ddlPriorities, ddlEfforts = null;
        if (priorities != null && efforts != null) {
            ddlPriorities = priorities.priorities.map((priority) =>
                <option key={priority.priorityId} value={priority.priorityId}>{priority.priorityName}</option>
            );
            ddlEfforts = efforts.efforts.map((effort) =>
                <option key={effort.effortId} value={effort.effortId}>{effort.effortName}</option>
            );
        }

        const content = (<form onSubmit={this.handleSubmit}>
                <div className="row">
                    <div className="form-group col-md-12">
                        <label>Task name:</label>
                        <input name="taskname" type="text" value={this.state.taskname} onChange={this.handleChange} className="form-control" />
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md-12">
                        <label>Description</label>
                        <textarea rows="3" name="description" value={this.state.description} onChange={this.handleChange} className="form-control"></textarea>
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md-12">
                        <label>Effort:</label>
                        <select className="custom-select mr-sm-2" name="effort" onChange={this.handleChange} disabled={efforts === null} >
                            <option key={-1} value={-1}>Choose...</option>
                            {ddlEfforts}
                        </select>
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md-12">
                        <label>Priority:</label>
                        <select className="custom-select mr-sm-2" name="priority" onChange={this.handleChange} disabled={priorities === null} >
                            <option key={-1} value={-1}>Choose...</option>
                            {ddlPriorities}
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
            <div >
                <div className="container">
                    <Button color="success" style={{ marginTop: "-70px", float: "right" }} onClick={this.toggle} >Create task</Button>
                </div>
                <ModalComponent 
                    show={showCreateTaskModal}
                    header="New task for backlog"
                    content={content}
                    onCancelClick={this.props.toggleCreateTaskModal}
                    onSubmitClick={this.handleSubmit}
                />
            </div >
        );
    }
}

CreateTask.propTypes = {
    createTask: PropTypes.func.isRequired,
    toggleCreateTaskModal: PropTypes.func.isRequired,
    getEfforts: PropTypes.func.isRequired,
    getPriorities: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    backlog: state.backlog,
    dics: state.dics,
    modal: state.modal
});

export default connect(mapStateToProps, { createTask, toggleCreateTaskModal, getEfforts, getPriorities })(CreateTask);