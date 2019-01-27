import React, { Component } from 'react';
import { sortableContainer, sortableElement, arrayMove, DragLayer } from '../react-sortable-multiple-hoc';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

const dragLayer = new DragLayer();

const SortableItem = sortableElement((props) => {
    return (<li style={{textAlign: "left"}} className="list-group-item list-group-item-light" onClick={props.onSelect}>
            {/* {props.item.ind}  */}
            <h6>{props.item.val.taskName} </h6>
            <div style={{textAlign: "right"}}>
            <span  className="badge badge-light">{props.item.val.userName === null ? "Not assigned" : props.item.val.userName}</span></div>
            <div style={{textAlign: "left"}}>
            <div><span style={{float: "right"}} className="badge badge-light">Effort: {props.item.val.effort}</span></div>
            <a href="#" className="badge badge-danger">DEL</a>
            <a href="#" className="badge badge-light">More</a>
            </div>
        </li>);
});

const SortableListItems = sortableContainer(({ items }) =>
    <ul className="list-group">
        {items.map((value, index) => (
          <SortableItem
              key={index}
              index={index}
              item={value}
          />
        ))}
    </ul>
);

const SortablePart = sortableElement(props =>
    <div className="col-sm">
        {/* <div><span>{props.item.name}</span></div> */}
        <SortableListItems
            {...props}
            items={props.item.items}
            dragLayer={dragLayer}
            distance={3}
            helperClass={'selected'}
            isMultiple={true}
            helperCollision={{ top: 1, bottom: 1 }}
        />
    </div>
);

const SortableListParts = sortableContainer(({ items, onSortItemsEnd }) =>
    <div className="row">
        {items.map((value, index) => (
            <SortablePart
                key={index}
                index={index}
                item={value}
                id={index}
                onMultipleSortEnd={onSortItemsEnd}
            />
        ))}
    </div>
);

class SortableComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            parts: []
        };
    }

    componentDidMount() {
        this.setState({
            parts: this.props.sprint.tasks
        })
    }
    
    onSortEnd = ({ oldIndex, newIndex }) => {
        this.setState({
            parts: arrayMove(this.state.parts, oldIndex, newIndex),
        });
    }
    onSortItemsEnd = ({ newListIndex, newIndex, items }) => {
        const parts = this.state.parts.slice();
        const itemsValue = [];

        items.forEach(item => {
            itemsValue.push(parts[item.listId].items[item.id]);
        });
        for (let i = items.length - 1; i >= 0; i--) {
            const item = items[i];

            parts[item.listId].items.splice(item.id, 1);
        }
        parts[newListIndex].items.splice(newIndex, 0, ...itemsValue);
        this.setState({
            parts: parts,
        });
        console.log(this.state)

    }
    render() {
        const parts = this.state.parts.map((value, index) => {
            return {
                name: value.name,
                items: value.items.map((val, ind) => {
                    return {
                        val: val,
                        ind: (index + 1) + '.' + (ind + 1),
                    };
                }),
            };
        });

        return (<div className="container">
        <SortableListParts
            items={parts}
            onSortEnd={this.onSortEnd}
            onSortItemsEnd={this.onSortItemsEnd}
            helperClass={'selected'}/>
    </div>);
    }
}

SortableComponent.propTypes = {
    sprint: PropTypes.object.isRequired
}

const mapStateToProps = (state) => ({
    sprint: state.sprint
});

export default connect(mapStateToProps, {})(SortableComponent);