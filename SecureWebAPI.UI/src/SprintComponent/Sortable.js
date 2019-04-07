import React, { Component } from 'react';
import { sortableContainer, sortableElement, arrayMove, DragLayer } from '../react-sortable-multiple-hoc';
import { onSortSprintTasks } from "../actions/sprintActions";
import { getTaskById, removeTask } from '../actions/backlogActions';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import SprintItem from './SprintItem';
import classnames from 'classnames';


const dragLayer = new DragLayer();

const SortableItem = sortableElement((props) => {
    return (<li style={{ textAlign: "left" }} className="list-group-item list-group-item-light" onClick={props.onSelect}>
        {/* {props.item.ind}  */}
        <h6>{props.item.val.taskName} </h6>
        <div style={{ textAlign: "right" }}>
            <span className={classnames("badge", {
                        'badge-light': props.item.val.userName !== null && props.item.val.userName !== props.userName,
                        'badge-warning': props.item.val.userName === null,
                        'badge-info': props.item.val.userName === props.userName,                        
                    })}>           
            
            {props.item.val.userName === null ? "Not assigned" : props.item.val.userName}</span></div>
        <div style={{ textAlign: "left" }}>
            <div><span style={{ float: "right" }} className="badge badge-light">Effort: {props.item.val.effort}</span></div>
            <a onClick={() => props.removeTask(props.item.val.taskId, props.teamId)} style={{color: "white"}} className="badge badge-danger">DEL</a>
            <a onClick={() => props.getTask(props.item.val.taskId, props.teamId)} style={{color: "black"}} className="badge badge-light">More</a>
        </div>
    </li>);
});

const SortableListItems = sortableContainer(({ items, getTask, removeTask, teamId, userName }) =>
    <ul className="list-group">
        {items.map((value, index) => (
            <SortableItem
                key={index}
                index={index}
                item={value}
                getTask={getTask}
                removeTask={removeTask}
                teamId={teamId}
                userName={userName}
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
            removeTask={props.removeTask}
            teamId={props.teamId}
            userName={props.userName}
        />
    </div>
);

const SortableListParts = sortableContainer(({ items, onSortItemsEnd, getTask, tasks, removeTask,teamId, userName }) =>
    <div>
        <div className="row">
            {tasks}
        </div>
        <div className="row">
            {items.map((value, index) => (
                <SortablePart
                    key={index}
                    index={index}
                    item={value}
                    id={index}
                    onMultipleSortEnd={onSortItemsEnd}
                    getTask={getTask}
                    removeTask={removeTask}
                    teamId={teamId}
                    userName={userName}
                />
            ))}
        </div>
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

    onClickGetTaskById = (id, teamId) => {
        this.props.getTaskById(id, teamId);
    }

    onClickRemoveTask = (id, teamId) => {
        this.props.removeTask(id, teamId);
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
        const { sprint } = this.props;
        const { auth } = this.props;

        let tasks = null;
        if (sprint != null && sprint.tasks != null) {
            if (sprint.tasks.length > 0) {
                tasks = sprint.tasks.map(t =>
                    <SprintItem key={t.columnId} task={t} />
                );
            }
        }
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
                removeTask={this.onClickRemoveTask}
                tasks={tasks}
                teamId={this.props.sprint.teamId}
                userName={auth.user.nameid}
            />
        </div>);
    }
}

SortableComponent.propTypes = {
    sprint: PropTypes.object.isRequired,
    onSortSprintTasks: PropTypes.func.isRequired,
    getTaskById: PropTypes.func.isRequired,
    removeTask: PropTypes.func.isRequired,
    auth: PropTypes.object.isRequired,
}

const mapStateToProps = (state) => ({
    sprint: state.sprint,
    auth: state.auth
});

export default connect(mapStateToProps, { onSortSprintTasks, getTaskById, removeTask })(SortableComponent);