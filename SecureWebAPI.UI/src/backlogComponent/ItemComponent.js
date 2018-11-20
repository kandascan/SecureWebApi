import React, { Component } from 'react'

export default class ItemComponent extends Component {

    onButtonClick = (id) => {
        const { onDeleteItem } = this.props;
        onDeleteItem(parseInt(id));
    }
    render() {
        const { index, value, id } = this.props;
        return (
            <li className="list-group-item d-flex justify-content-between align-items-center list-group-item-light">
                {value}
                <span className="badge badge-primary badge-pill">{index}</span>

                <div>
                    <button onClick={() => this.onButtonClick(id)} type="button" className="btn btn-outline-info btn-sm">
                        Delete
                    </button>
                </div>
            </li>
        )
    }
}