import React from 'react';
import { Button } from 'reactstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getTaskById } from '../actions/backlogActions';
import { getEfforts, getPriorities } from '../actions/dictionaryActions';
import { toggleEditTaskModal } from '../actions/modalActions';
import ModalComponent from '../CommonComponent/ModaComponent';
import $ from 'jquery';
window.jQuery = window.$ = $;
class EditTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = { taskname: '', description: '', effort: -1, priority: -1, username: '', disabledEdit: true };
    }

    componentWillReceiveProps = (nextProps) => {
        this.setState({
            taskname: nextProps.backlog.task.task.taskname, 
            description: nextProps.backlog.task.task.description, 
            effort: nextProps.backlog.task.task.effortId, 
            priority: nextProps.backlog.task.task.priorityId, 
            username: nextProps.backlog.task.task.username
        })
    }

    componentDidMount = () =>{      
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
        this.props.toggleEditTaskModal();
        // let newTask = {
        //     taskname: this.state.taskname,
        //     description: this.state.description,
        //     effortId: +this.state.effort,
        //     priorityId: +this.state.priority,
        //     username: this.state.username,
        // }
        //this.props.createTask(newTask);
        //this.setState({ taskname: '', description: '', effort: -1, priority: -1, username: '' });
    }

    render() {
        const { showEditTaskModal } = this.props.modal;
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
                    <input name="taskname" type="text" value={this.state.taskname} onChange={this.handleChange} disabled={this.state.disabledEdit} className="form-control" />
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Description</label>
                    <textarea rows="3" name="description" value={this.state.description} onChange={this.handleChange} disabled={this.state.disabledEdit} className="form-control"></textarea>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Effort:</label>
                    <select className="custom-select mr-sm-2" name="effort" onChange={this.handleChange} disabled={efforts === null || this.state.disabledEdit} value={this.state.effort} >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlEfforts}
                    </select>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Priority:</label>
                    <select className="custom-select mr-sm-2" name="priority" onChange={this.handleChange} disabled={priorities === null || this.state.disabledEdit} value={this.state.priority} >
                        <option key={-1} value={-1}>Choose...</option>
                        {ddlPriorities}
                    </select>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-md-12">
                    <label>Username:</label>
                    <input name="username" type="text" value={this.state.username} onChange={this.handleChange} disabled={this.state.disabledEdit} className="form-control" />
                </div>
            </div>
    </form>);
        return (
                <ModalComponent 
                    show={showEditTaskModal}
                    header="Task details"
                    content={content}
                    onCancelClick={this.props.toggleEditTaskModal}
                    onSubmitClick={this.handleSubmit}
                />
        );
    }
}

EditTask.propTypes = {
    getTaskById: PropTypes.func.isRequired,
    getEfforts: PropTypes.func.isRequired,
    getPriorities: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    backlog: state.backlog,
    dics: state.dics,
    modal: state.modal
});

export default connect(mapStateToProps, { getTaskById, toggleEditTaskModal, getEfforts, getPriorities })(EditTask);