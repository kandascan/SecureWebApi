import React, { Component } from 'react';
import ModalComponent from '../CommonComponent/ModaComponent';

export default class ItemComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
         show: false
        };
      }

    toggleModal = (id) => {
        this.setState({
            show: !this.state.show
        })
    }

    onDeleteClick = (id) => {
        const { onDeleteItem } = this.props;
        onDeleteItem(parseInt(id));
    }
    render() {
        const { index, value, id } = this.props;
        const modalHeader = "Tytul modala";
        const modalContent = (<div className="row">
        <div className="form-group col-md-12">
            <h3>ModalBody</h3>
        </div>
    </div>)
        return (
            <div>
                <ModalComponent 
                show={this.state.show} 
                header={modalHeader}
                content={modalContent}
                onCancelClick={() => this.toggleModal(id)} 
                onSubmitClick={() => this.toggleModal(id)}
                />
                
                <li className="list-group-item d-flex justify-content-between align-items-center list-group-item-light">
                {value}
                <span className="badge badge-primary badge-pill">{index}</span>
                <div>
                    <button type="button" onClick={() => this.toggleModal(id)} className="btn btn-outline-info btn-sm">Edit</button>{' '}
                    <button type="button" onClick={() => this.onDeleteClick(id)} className="btn btn-outline-danger btn-sm">Delete</button>
                </div>
            </li>
            </div>
            
        )
    }
}