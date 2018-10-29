import React, { Component } from 'react'

class BacklogTask extends Component {
    onButtonClick (v) {
        const { onDeleteItem } = this.props;
        onDeleteItem(v);
    }
    render() {
        const { value } = this.props;
        return (
            <li className="list-group-item noselect">{value}{' '}
                <button onClick={() => this.onButtonClick(value)} type="button" className="btn btn-outline-info btn-sm">
                    Delete
              </button>
            </li>
        )
    }
}

export default BacklogTask;

