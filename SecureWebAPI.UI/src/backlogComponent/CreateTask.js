import React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createTask, toggleModal } from '../actions/backlogActions';

class CreateTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = { taskname: '', description: '', effort: '', priority: '', username: '' };
    }

    toggle = () => {
        this.props.toggleModal();
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        let newTask = {
            taskname: this.state.taskname,
            description: this.state.description,
            effort: this.state.effort,
            priority: this.state.priority,
            username: this.state.username,
        }
        this.props.createTask(newTask);
        this.setState({ taskname: '', description: '', effort: '', priority: '', username: '' });
    }

    render() {
        const { modal } = this.props.backlog;
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
                                    <input name="effort" type="text" value={this.effort} onChange={this.handleChange} className="form-control" />
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group col-md-12">
                                    <label>Priority:</label>
                                    <input name="priority" type="text" value={this.priority} onChange={this.handleChange} className="form-control" />
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
                            <input type="submit" value="Submit" color="primary" className="btn btn-primary" />
                            <Button color="danger" onClick={this.toggle}>Cancel</Button>
                        </ModalFooter>
                    </form>
                </Modal>
            </div>

        );
    }
}

CreateTask.propTypes = {
    backlog: PropTypes.object.isRequired,
    createTask: PropTypes.func.isRequired,
    toggleModal: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    backlog: state.backlog
});

export default connect(mapStateToProps, { createTask, toggleModal })(CreateTask);