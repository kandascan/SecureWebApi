import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getTaskById } from '../actions/backlogActions';
import { toggleEditTaskModal } from '../actions/modalActions';
import ModalComponent from '../CommonComponent/ModaComponent';

class ItemComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
         show: false
        };
      }

    onDeleteClick = (id) => {
        const { onDeleteItem } = this.props;
        onDeleteItem(parseInt(id));
    }

    getTaskById = (id) => {
        const { toggleEditTaskModal, getTaskById } = this.props;
        toggleEditTaskModal();
        getTaskById(parseInt(id));        
    }

    render() {
        const { index, value, id } = this.props;
        const { showEditTaskModal } = this.props.modal;
        const modalHeader = "Tytul modala";
        const modalContent = (<div className="row">
        <div className="form-group col-md-12">
            <h3>ModalBody</h3>
        </div>
    </div>)
    console.log(this.props.backlog.task)
        return (
            <div>
                <ModalComponent 
                show={showEditTaskModal} 
                header={modalHeader}
                content={modalContent}
                onCancelClick={this.props.toggleEditTaskModal} 
                onSubmitClick={this.props.toggleEditTaskModal}
                />
                
                <li className="list-group-item d-flex justify-content-between align-items-center list-group-item-light">
                {value}
                <span className="badge badge-primary badge-pill">{index}</span>
                <div>
                    <button type="button" onClick={() => this.getTaskById(id)} className="btn btn-outline-info btn-sm">Edit</button>{' '}
                    <button type="button" onClick={() => this.onDeleteClick(id)} className="btn btn-outline-danger btn-sm">Delete</button>
                </div>
            </li>
            </div>
            
        )
    }
}

ItemComponent.propTypes = {
    toggleEditTaskModal: PropTypes.func.isRequired,
    getTaskById: PropTypes.func.isRequired,
}

const mapStateToProps = (state) => ({
    backlog: state.backlog,
    modal: state.modal
});

export default connect(mapStateToProps, { toggleEditTaskModal, getTaskById })(ItemComponent);