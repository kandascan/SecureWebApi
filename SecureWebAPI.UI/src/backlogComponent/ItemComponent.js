import React, { Component } from 'react'

export default class ItemComponent extends Component {

    onButtonClick = (v) => {
        const { onDeleteItem } = this.props;
        // console.log(v);
        onDeleteItem(v);
    }
    render() {
        const { index, value } = this.props;
        console.log(this.props)
        return (
            <li className="list-group-item d-flex justify-content-between align-items-center list-group-item-light">
                {value}
                <span className="badge badge-primary badge-pill">{index}</span>

                <div>
                    <button onClick={() => this.onButtonClick(value)} type="button" className="btn btn-outline-info btn-sm">
                        Delete
                    </button>
                </div>
            </li>
        )
    }
}