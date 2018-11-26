import React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createTask, toggleModal } from '../actions/backlogActions';
import { getEffortsAndPriorities } from '../actions/dictionaryActions';
import $ from 'jquery';
window.jQuery = window.$ = $;
class CreateTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = { taskname: '', description: '', effort: -1, priority: -1, username: '' };
    }

    toggle = () => {
        this.props.toggleModal();
        const { priorities, efforts } = this.props.dics;
        if(priorities === null && efforts === null){
            this.props.getEffortsAndPriorities();
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
        const { modal } = this.props.backlog;
        const { priorities, efforts } = this.props.dics;
        let ddlPriorities = null;
        if(priorities !== null){
            ddlPriorities = priorities.priorities.map((priority) => 
            <option key={priority.priorityId} value={priority.priorityId}>{priority.priorityName}</option>
            );
        }

        let ddlEfforts = null;
        if(efforts !== null){
            ddlEfforts = efforts.efforts.map((effort) => 
            <option key={effort.effortId} value={effort.effortId}>{effort.effortName}</option>
            );
        }
       
        return (
            <div >
                <div className="container">
                    <Button color="success" style={{ marginTop: "-70px", float: "right" }} onClick={this.toggle} >Create task</Button>
                </div>
                <Modal isOpen={modal}>
                    <form onSubmit={this.handleSubmit}>
                        <ModalHeader>New task for backlog</ModalHeader>
                        <ModalBody>
                            <div className="row">
                                <div className="form-group col-md-12">
                                    <label>Task name:</label>
                                    <input name="taskname" type="text" value={this.state.taskname} onChange={this.handleChange} className="form-control" />
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group col-md-12">
                                    <label>Description</label>
                                    <input name="description" type="text" value={this.state.description} onChange={this.handleChange} className="form-control" />
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
                                    <input name="username" type="text" value={this.username} onChange={this.handleChange} className="form-control" />
                                </div>
                            </div>
                        </ModalBody>
                        <ModalFooter>
                            <Button color="danger" onClick={this.toggle}>Cancel</Button>
                            <input type="submit" value="Submit" color="primary" className="btn btn-primary" />
                        </ModalFooter>
                    </form>
                </Modal>
            </div>
        );
    }
}

CreateTask.propTypes = {
    createTask: PropTypes.func.isRequired,
    toggleModal: PropTypes.func.isRequired,
    getEffortsAndPriorities: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    backlog: state.backlog,
    dics: state.dics
});

export default connect(mapStateToProps, { createTask, toggleModal, getEffortsAndPriorities })(CreateTask);