import React, { Component } from 'react';

class ItemComponent extends Component {
    onDeleteClick = (id) => {
        const { onDeleteItem } = this.props;
        onDeleteItem(parseInt(id));
    }

    onEditTaskClick = (id) => {
        const { onEditTask } = this.props;
        onEditTask(parseInt(id));        
    }

    render() {
        const { index, value, id } = this.props;
        return (
            <div>              
                <li className="list-group-item d-flex justify-content-between align-items-center list-group-item-light">
                {value}
                <span className="badge badge-primary badge-pill">{index}</span>
                <div>
                    <button type="button" onClick={() => this.onEditTaskClick(id)} className="btn btn-outline-info btn-sm">Edit</button>{' '}
                    <button type="button" onClick={() => this.onDeleteClick(id)} className="btn btn-outline-danger btn-sm">Delete</button>
                </div>
            </li>
            </div>            
        )
    }
}

export default ItemComponent;