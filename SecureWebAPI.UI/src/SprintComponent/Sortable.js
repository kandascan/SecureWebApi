import React, { Component } from 'react';
import { sortableContainer, sortableElement, arrayMove, DragLayer } from '../react-sortable-multiple-hoc';
import { onSortSprintTasks } from "../actions/sprintActions";
import { getTaskById } from '../actions/backlogActions';
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
            <a onClick={() => props.getTask(props.item.val.taskId)} className="badge badge-light">More</a>
            </div>
        </li>);
});

const SortableListItems = sortableContainer(({ items, getTask }) =>
    <ul className="list-group">
        {items.map((value, index) => (
          <SortableItem
              key={index}
              index={index}
              item={value}
              getTask={getTask}
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
            getTask={props.getTask}
        />
    </div>
);

const SortableListParts = sortableContainer(({ items, onSortItemsEnd, getTask }) =>
    <div className="row">
        {items.map((value, index) => (
            <SortablePart
                key={index}
                index={index}
                item={value}
                id={index}
                onMultipleSortEnd={onSortItemsEnd}
                getTask={getTask}
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

    componentWillReceiveProps = (nextProps) => {
        this.setState({
            parts: nextProps.sprint.tasks,
            sprintId: nextProps.sprint.sprintId,
            teamId: nextProps.sprint.teamId
        });
    }

    onClickGetTaskById = (id) => {
        this.props.getTaskById(id, this.state.teamId);
    }

    componentDidMount() {
        this.setState({
            parts: this.props.sprint.tasks,
            sprintId: this.props.sprint.sprintId,
            teamId: this.props.sprint.teamId
        });
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

        const sprintTasks = {
            TeamId: this.state.teamId,
            SprintId: this.state.sprintId,
            SprintBoardTasks: this.state.parts,
        }
        this.props.onSortSprintTasks(sprintTasks);

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
            helperClass={'selected'}
            getTask={this.onClickGetTaskById}
            />
    </div>);
    }
}

SortableComponent.propTypes = {
    sprint: PropTypes.object.isRequired,
    onSortSprintTasks: PropTypes.func.isRequired,
    getTaskById: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    sprint: state.sprint
});

export default connect(mapStateToProps, {onSortSprintTasks, getTaskById})(SortableComponent);